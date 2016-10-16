using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    /// <summary>
    /// BitGo Instant allows sending on-chain transactions which can be credited instantly by recipients, 
    /// due to a financial guarantee by BitGo against double-spending. Anyone can receive BitGo Instant transactions. 
    /// In order to send BitGo Instant transactions, you will need either a BitGo KRS wallet, or will need to arrange a collateral agreement with BitGo.
    /// </summary>
    public sealed class InstantService : ApiService, IInstantService {
        
        internal InstantService(BitGoClient client) : base(client, "instant") {

        }

        /// <summary>
        /// Use this method to receive instant guarantee message.
        /// </summary>
        /// <param name="instantId">
        /// The instant guarantee ID on BitGo.
        /// </param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// BitGo Instant is built on top of our wallet platform, as a guarantee by BitGo against double spends. 
        /// As a co-signer on a multi-sig wallet, BitGo will never double-spend an output. We back our promise with a 
        /// cryptographically signed guarantee on each transaction, enabling receivers to accept funds without the need for any block confirmations.
        /// </remarks>
        /// <returns>Returns the instant guarantee message (<see cref="InstantGuarantee"/>), including the amount and Transaction ID.</returns>
        public Task<InstantGuarantee> GetAsync(string instantId, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<InstantGuarantee>($"{_url}/{instantId}", true, cancellationToken);
        
    }
}