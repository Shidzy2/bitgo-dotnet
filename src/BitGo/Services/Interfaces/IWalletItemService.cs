using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Wallet item service interface to use the BitGo API
    /// </summary>
    public interface IWalletItemService
    {
        Task<Wallet> GetAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletTransactionList> GetTransactionListAsync(bool? compact = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletTransaction> GetTransactionAsync(string hash, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletTransaction> GetTransactionBySequenceAsync(string sequenceId, CancellationToken cancellationToken = default(CancellationToken));

        Task<TransactionResult> SendCoinsToAddressAsync(string address, long amount, string passphrase, string message = null, string sequenceId = null, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<TransactionResult> SendCoinsToMultipleAddressesAsync(Dictionary<string, long> recepients, string passphrase, string message = null, string sequenceId = null, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletUnsignedTransaction> CreateTransactionAsync(Dictionary<string, long> recepients, long? fee = null, long? feeRate = null, int feeTxConfirmTarget = 2, int minConfirms = 1, bool enforceMinConfirmsForChange = true, long minUnspentSize = 5460, bool? instant = null, CancellationToken cancellationToken = default(CancellationToken));

        string SignTransaction(string transactionHex, WalletUnsignedTransactionUnspent[] unspents, Keychain userKeychain);

        Task<WalletAddressList> GetAddressListAsync(int? chain = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddress> GetAddressAsync(string address, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddress> CreateAddressAsync(int chain = 0, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletUnspentList> GetUnspentListAsync(bool? instant = null, long? target = null, int? skip = null, int? limit = null, long? minSize = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<long> GetBillingFeeAsync(long amount, bool instant = false, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> GetPolicyAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicyStatusList> GetPolicyStatusAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> SetPolicyRuleDailyLimitAsync(string id, string action, long amount, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> SetPolicyRuleTransactionLimitAsync(string id, string action, long amount, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> SetPolicyRuleWebhookAsync(string id, string action, string url, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> SetPolicyRuleAddBitcoinAddressWhitelistAsync(string id, string action, string address, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> SetPolicyRuleRemoveBitcoinAddressWhitelistAsync(string id, string action, string address, WalletPolicyRuleActionParams actionParams = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletPolicy> RemovePolicyRuleAsync(string ruleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletWebhook[]> GetWebhookListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletWebhook> AddWebhookAsync(string type, string url, int? numConfirmations = null, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveWebookAsync(string type, string url, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletFreeze> FreezeAsync(TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}