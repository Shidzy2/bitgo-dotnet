using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Market service interface to use the BitGo API
    /// </summary>
    public interface IMarketService
    {
        Task<MarketData> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}