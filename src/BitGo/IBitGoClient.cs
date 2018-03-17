using System;
using BitGo.Services;

namespace BitGo
{
    /// <summary>
    /// A client interface to use the BitGo API
    /// </summary>
    public interface IBitGoClient
    {
        WalletService Wallets { get; }

        WalletAddressService WalletAddresses { get; }

        KeychainService Keychains { get; } 

        UserService User { get; } 

        BillingService Billing { get; }

        InstantService Instant { get; }

        TransactionService Transactions { get; }

        LabelService Labels { get; }

        MarketService Market { get; }

        WebhookService Webhooks { get; }

        PendingApprovalService PendingApprovals { get; }
    }
}