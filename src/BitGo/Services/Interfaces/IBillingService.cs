using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Instant service interface to use the BitGo API
    /// </summary>
    public interface IBillingService
    {
        Task<string> GetAddressAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}