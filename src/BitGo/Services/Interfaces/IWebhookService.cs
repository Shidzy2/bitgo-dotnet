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
    public interface IWebhookService
    {
        Task<UserWebhook[]> GetListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<UserWebhook> AddAsync(string type, string url, string coin = "bitcoin", CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveAsync(string type, string url, CancellationToken cancellationToken = default(CancellationToken));
    }
}