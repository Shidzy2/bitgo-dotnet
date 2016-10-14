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

        public Task<WalletAddressList> GetAddressListAsync(int? chain = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddressList>($"{_url}/addresses{_client.ConvertToQueryString(new Dictionary<string, object>() { { "chain", chain }, { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<WalletAddress> GetAddressAsync(string address, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddress>($"{_url}/addresses/{address}", true, cancellationToken);

        public Task<WalletAddress> CreateAddressAsync(int chain = 0, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletAddress>($"{_url}/addresses/{chain}", null, cancellationToken);

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