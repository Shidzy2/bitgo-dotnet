using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Label service interface to use the BitGo API
    /// </summary>
    public interface ILabelService
    {
        Task<WalletAddressLabelList> GetListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddressLabelList> GetListByWalletAsync(string walletId, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddressLabel> SetAsync(string walletId, string address, string label, CancellationToken cancellationToken = default(CancellationToken));

        Task<WalletAddressLabel> DeleteAsync(string walletId, string address, CancellationToken cancellationToken = default(CancellationToken));
    }
}