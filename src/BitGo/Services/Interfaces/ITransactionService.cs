using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Transaction service interface to use the BitGo API
    /// </summary>
    public interface ITransactionService
    {
        Task<TransactionResult> SendAsync(string hex, string sequenceId = null, string message = null, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<TransactionFeeEstimate> EstimateFeesAsync(int numBlocks = 2, CancellationToken cancellationToken = default(CancellationToken));
    }
}