using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class TransactionService : ApiService, ITransactionService {
        
        internal TransactionService(BitGoClient client) : base(client, "tx") {

        }

        public Task<TransactionFeeEstimate> EstimateFeesAsync(int numBlocks = 2, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<TransactionFeeEstimate>($"{_url}/fee?numBlocks={numBlocks}", false, cancellationToken);

        public Task<TransactionResult> SendAsync(string hex, string sequenceId = null, string message = null, bool? instant = null, string otp = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<TransactionResult>($"{_url}/send", new SendTransactionArgs { Hex = hex, SequenceId = sequenceId, Message = message, Instant = instant, Otp = otp }, cancellationToken);
    }
}