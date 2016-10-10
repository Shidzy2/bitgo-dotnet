using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Wallet service interface to use the BitGo API
    /// </summary>
    public interface IWalletService
    {
        Task<WalletList> GetListAsync(int skip = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken));

        Task<Wallet> GetAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<Wallet> AddAsync(string label, string[] extendedPublicKeys, string enterprise = null, bool? disableTransactionNotifications = null, int m = 2, int n = 3,  CancellationToken cancellationToken = default(CancellationToken));

        Task<Wallet> CreateAsync(string label, string passphrase, string backupExtendedPublicKey = null, string backupProvider = null, string enterprise = null, bool? disableTransactionNotifications = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}