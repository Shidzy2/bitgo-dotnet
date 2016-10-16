using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class WalletItemService : ApiService, IWalletItemService
    {

        private readonly IWalletService _walletService;
        private readonly string _id;

        internal WalletItemService(BitGoClient client, IWalletService walletService, string id) : base(client, $"wallet/{id}")
        {
            _walletService = walletService;
            _id = id;
        }

        public Task<Wallet> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _walletService.GetAsync(_id, cancellationToken);

        public Task<WalletTransactionList> GetTransactionListAsync(bool? compact = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransactionList>($"{_url}/tx{_client.ConvertToQueryString(new Dictionary<string, object>() { { "compact", compact }, { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<WalletTransaction> GetTransactionAsync(string hash, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransaction>($"{_url}/tx/{hash}", true, cancellationToken);

        public Task<WalletTransaction> GetTransactionBySequenceAsync(string sequenceId, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransaction>($"{_url}/tx/sequence/{sequenceId}", true, cancellationToken);

        public Task<TransactionResult> SendCoinsToAddressAsync(string address, long amount, string passphrase, string message = null, string sequenceId = null, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendCoinsToMultipleAddressesAsync(new Dictionary<string, long>() { { address, amount } }, passphrase, message, sequenceId, fee, feeRate, feeTxConfirmTarget, minConfirms, enforceMinConfirmsForChange, minUnspentSize, instant, otp, cancellationToken);
        }

        public async Task<TransactionResult> SendCoinsToMultipleAddressesAsync(Dictionary<string, long> recepients, string passphrase, string message = null, string sequenceId = null, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var unsignedTransaction = await CreateTransactionAsync(recepients, fee, feeRate, feeTxConfirmTarget, minConfirms, enforceMinConfirmsForChange, minUnspentSize, instant, cancellationToken);
            var userKeychain = await _client.Keychains.GetAsync(unsignedTransaction.WalletKeychains[0].ExtendedPublicKey);
            userKeychain.ExtendedPrivateKey = _client.Decrypt(userKeychain.EncryptedExtendedPrivateKey, passphrase);
            var signedTransactionHex = SignTransaction(unsignedTransaction.TransactionHex, unsignedTransaction.Unspents, userKeychain);
            return await _client.Transaction.SendAsync(signedTransactionHex, sequenceId, message, instant, otp, cancellationToken);
        }

        public async Task<WalletUnsignedTransaction> CreateTransactionAsync(Dictionary<string, long> recepients, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hexEncoder = new NBitcoin.DataEncoders.HexEncoder();
            var builder = new TransactionBuilder();
            builder.DustPrevention = true;
            var totalValue = recepients.Values.Sum();
            var unspents = await GetUnspentListAsync(instant, totalValue, null, null, minUnspentSize, cancellationToken);
            var changeAddress = await CreateAddressAsync(1, cancellationToken);
            builder.SetChange(_client.Network.CreateBitcoinAddress(changeAddress.Address));
            foreach (var unspent in unspents.Unspents)
            {
                builder.AddCoins(new ScriptCoin(new OutPoint(uint256.Parse(unspent.TransactionHash), unspent.TransactionOutputIndex), new TxOut(unspent.Value, new Script(hexEncoder.DecodeData(unspent.Script))), new Script(hexEncoder.DecodeData(unspent.RedeemScript))));
            }
            foreach (var recipient in recepients)
            {
                builder.Send(_client.Network.CreateBitcoinAddress(recipient.Key), recipient.Value);
            }
            var billingFee = await GetBillingFeeAsync(totalValue, instant ?? false, cancellationToken);
            if(billingFee > 0)
            {
                var billingAddress = await _client.Billing.GetAddressAsync(cancellationToken);
                billingFee = (long)Math.Min(billingFee, totalValue * 0.002);
                builder.Send(_client.Network.CreateBitcoinAddress(billingAddress), billingFee);
            }
            long finnalFee = 0;
            if (fee.HasValue)
            {
                builder.SendFees(fee.Value);
                finnalFee = fee.Value;
            }
            else if (feeRate.HasValue)
            {
                var estimatedFeeRate = new FeeRate(feeRate.Value);
                builder.SendEstimatedFees(estimatedFeeRate);
                finnalFee = builder.EstimateFees(estimatedFeeRate).Satoshi;
            }
            else
            {
                var estimateFee = await _client.Transaction.EstimateFeesAsync(feeTxConfirmTarget, cancellationToken);
                var estimatedFeeRate = new FeeRate(estimateFee.FeePerKb);
                builder.SendEstimatedFees(estimatedFeeRate);
                finnalFee = builder.EstimateFees(estimatedFeeRate).Satoshi;                
            }
            
            var transactionHex = builder.BuildTransaction(false).ToHex();
            return new WalletUnsignedTransaction
            {
                WalletId = _id,
                TransactionHex = transactionHex,
                Fee = finnalFee,
                ChangeAddress = new WalletUnsignedTransactionAddress { Address = changeAddress.Address, Path = changeAddress.Path },
                WalletKeychains = (await GetAsync()).Keychains.Keychains.Select(k => new WalletUnsignedTransactionKeychain { ExtendedPublicKey = k.ExtendedPublicKey, Path = k.Path }).ToArray(),
                Unspents = unspents.Unspents.Select(u => new WalletUnsignedTransactionUnspent { RedeemScript = u.RedeemScript, Script = u.Script, ChainPath = u.ChainPath, TransactionHash = u.TransactionHash, TransactionOutputIndex = u.TransactionOutputIndex, Value = u.Value }).ToArray()
            };
        }

        public string SignTransaction(string transactionHex, WalletUnsignedTransactionUnspent[] unspents, Keychain userKeychain)
        {
            var hexEncoder = new NBitcoin.DataEncoders.HexEncoder();
            var extKey = ExtKey.Parse(userKeychain.ExtendedPrivateKey);
            var builder = new TransactionBuilder().ContinueToBuild(Transaction.Parse(transactionHex));
            foreach (var unspent in unspents)
            {
                builder
                     .AddCoins(new ScriptCoin(new OutPoint(uint256.Parse(unspent.TransactionHash), unspent.TransactionOutputIndex), new TxOut(unspent.Value, new Script(hexEncoder.DecodeData(unspent.Script))), new Script(hexEncoder.DecodeData(unspent.RedeemScript))))
                     .AddKeys(extKey.Derive(KeyPath.Parse($"{userKeychain.Path}/0/0{unspent.ChainPath}")).PrivateKey);
            }
            return builder.BuildTransaction(true).ToHex();
        }

        public Task<WalletAddressList> GetAddressListAsync(int? chain = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddressList>($"{_url}/address{_client.ConvertToQueryString(new Dictionary<string, object>() { { "chain", chain }, { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<WalletAddress> GetAddressAsync(string address, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddress>($"{_url}/address/{address}", true, cancellationToken);

        public Task<WalletAddress> CreateAddressAsync(int chain = 0, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletAddress>($"{_url}/address/{chain}", null, cancellationToken);

        public Task<WalletUnspentList> GetUnspentListAsync(bool? instant = null, long? target = null, int? skip = null, int? limit = null, long? minSize = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletUnspentList>($"{_url}/unspents{_client.ConvertToQueryString(new Dictionary<string, object>() { { "instant", instant }, { "target", target }, { "skip", skip }, { "limit", limit }, { "minSize", minSize }, })}", true, cancellationToken);

        public Task<WalletPolicy> GetPolicyAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletPolicy>($"{_url}/policy", true, cancellationToken);
        public Task<WalletPolicyStatusList> GetPolicyStatusAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletPolicyStatusList>($"{_url}/policy/status", true, cancellationToken);

        private Task<WalletPolicy> SetPolicyRuleAsync<T>(T rule, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
            => _client.PutAsync<WalletPolicy>($"{_url}/policy/rule", rule, cancellationToken);

        public Task<WalletPolicy> SetPolicyRuleDailyLimitAsync(string id, string action, long amount, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken))
            => SetPolicyRuleAsync(new SetPolicyRuleArgs<SetPolicyRuleLimitConditionArgs>
            {
                Id = id,
                Type = "dailyLimit",
                Action = new SetPolicyRuleActionArgs
                {
                    Type = action,
                    ActionParams = actionParams == null ? null : new SetPolicyRuleActionParamsArgs
                    {
                        OtpType = actionParams.OtpType,
                        Phone = actionParams.Phone,
                        Duration = actionParams.Duration
                    }
                },
                Condition = new SetPolicyRuleLimitConditionArgs
                {
                    Amount = amount
                }
            }, cancellationToken);

        public Task<WalletPolicy> SetPolicyRuleTransactionLimitAsync(string id, string action, long amount, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken))
            => SetPolicyRuleAsync(new SetPolicyRuleArgs<SetPolicyRuleLimitConditionArgs>
            {
                Id = id,
                Type = "transactionLimit",
                Action = new SetPolicyRuleActionArgs
                {
                    Type = action,
                    ActionParams = actionParams == null ? null : new SetPolicyRuleActionParamsArgs
                    {
                        OtpType = actionParams.OtpType,
                        Phone = actionParams.Phone,
                        Duration = actionParams.Duration
                    }
                },
                Condition = new SetPolicyRuleLimitConditionArgs
                {
                    Amount = amount
                }
            }, cancellationToken);

        public Task<WalletPolicy> SetPolicyRuleWebhookAsync(string id, string action, string url, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken))
            => SetPolicyRuleAsync(new SetPolicyRuleArgs<SetPolicyRuleWebhookConditionArgs>
            {
                Id = id,
                Type = "webhook",
                Action = new SetPolicyRuleActionArgs
                {
                    Type = action,
                    ActionParams = actionParams == null ? null : new SetPolicyRuleActionParamsArgs
                    {
                        OtpType = actionParams.OtpType,
                        Phone = actionParams.Phone,
                        Duration = actionParams.Duration
                    }
                },
                Condition = new SetPolicyRuleWebhookConditionArgs
                {
                    Url = url
                }
            }, cancellationToken);

        public Task<WalletPolicy> SetPolicyRuleAddBitcoinAddressWhitelistAsync(string id, string action, string address, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken))
            => SetPolicyRuleAsync(new SetPolicyRuleArgs<SetPolicyRuleAddBitcoinAddressWhitelistConditionArgs>
            {
                Id = id,
                Type = "bitcoinAddressWhitelist",
                Action = new SetPolicyRuleActionArgs
                {
                    Type = action,
                    ActionParams = actionParams == null ? null : new SetPolicyRuleActionParamsArgs
                    {
                        OtpType = actionParams.OtpType,
                        Phone = actionParams.Phone,
                        Duration = actionParams.Duration
                    }
                },
                Condition = new SetPolicyRuleAddBitcoinAddressWhitelistConditionArgs
                {
                    Add = address
                }
            }, cancellationToken);

        public Task<WalletPolicy> SetPolicyRuleRemoveBitcoinAddressWhitelistAsync(string id, string action, string address, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken))
            => SetPolicyRuleAsync(new SetPolicyRuleArgs<SetPolicyRuleRemoveBitcoinAddressWhitelistConditionArgs>
            {
                Id = id,
                Type = "bitcoinAddressWhitelist",
                Action = new SetPolicyRuleActionArgs
                {
                    Type = action,
                    ActionParams = actionParams == null ? null : new SetPolicyRuleActionParamsArgs
                    {
                        OtpType = actionParams.OtpType,
                        Phone = actionParams.Phone,
                        Duration = actionParams.Duration
                    }
                },
                Condition = new SetPolicyRuleRemoveBitcoinAddressWhitelistConditionArgs
                {
                    Remove = address
                }
            }, cancellationToken);

        public Task<WalletPolicy> RemovePolicyRuleAsync(string ruleId, CancellationToken cancellationToken = default(CancellationToken))
            => _client.DeleteAsync<WalletPolicy>($"{_url}/policy/rule", new RemovePolicyRuleArgs { Id = ruleId }, cancellationToken);

        public Task<WalletWebhook[]> GetWebhookListAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletWebhook[]>($"{_url}/webhooks", true, cancellationToken);

        public Task<WalletWebhook> AddWebhookAsync(string type, string url, int? numConfirmations = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletWebhook>($"{_url}/webhooks", new AddWalletWebhookArgs { Type = type, Url = url, NumConfirmations = numConfirmations }, cancellationToken);

        public Task RemoveWebookAsync(string type, string url, CancellationToken cancellationToken = default(CancellationToken))
            => _client.DeleteAsync<object>($"{_url}/webhooks", new RemoveWalletWebhookArgs { Type = type, Url = url }, cancellationToken);

        public async Task<long> GetBillingFeeAsync(long amount, bool instant = false, CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.GetAsync<BillingFee>($"{_url}/billing/fee{_client.ConvertToQueryString(new Dictionary<string, object>() { { "amount", amount }, { "instant", instant } })}", true, cancellationToken)).Fee;

        public Task<WalletFreeze> FreezeAsync(TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletFreeze>($"{_url}/freeze", new FreezeWalletArgs { Duration = (int?)duration?.TotalSeconds }, cancellationToken);

    }
}