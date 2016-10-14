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
    public interface IPendingApprovalService
    {
        Task<PendingApprovalList> GetListAsync(string walletId = null, string enterprise = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<PendingApproval> UpdateAsync(string id, string state, CancellationToken cancellationToken = default(CancellationToken));
    }
}