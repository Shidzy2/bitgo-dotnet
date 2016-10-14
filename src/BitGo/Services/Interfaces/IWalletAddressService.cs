using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Wallet address service interface to use the BitGo API
    /// </summary>
    public interface IWalletAddressService
    {
        Task<WalletAddress> GetAsync(string address, CancellationToken cancellationToken = default(CancellationToken));
    }
}