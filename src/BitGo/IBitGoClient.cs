using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Services;

namespace BitGo
{
    /// <summary>
    /// A client interface to use the BitGo API
    /// </summary>
    public interface IBitGoClient
    {
        IWalletService Wallets { get; }

        IWalletAddressService WalletAddresses { get; }

        IKeychainService Keychains { get; } 

        IUserService User { get; } 

        IBillingService Billing { get; }

        IInstantService Instant { get; }

        ITransactionService Transaction { get; }

        ILabelService Labels { get; }

        IMarketService Market { get; }

        IWebhookService Webhooks { get; }

        IPendingApprovalService PendingApprovals { get; }
    }
}