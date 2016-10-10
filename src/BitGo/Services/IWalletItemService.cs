using System;
using System.IO;
using System.Net;
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

        Task<WalletTransactionList> GetTransactionListAsync(bool compact = true, int skip = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletTransaction> GetTransactionAsync(string hash, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletTransaction> GetTransactionBySequenceAsync(string sequenceId, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddressList> GetAddressListAsync(int? chain = null, int skip = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddress> GetAddressAsync(string address, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddress> CreateAddressAsync(int chain = 0, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletUnspentList> GetUnspentListAsync(bool? instant = null, long? target = null, int skip = 0, int limit = 25, long? minSize = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletFreeze> FreezeAsync(TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}