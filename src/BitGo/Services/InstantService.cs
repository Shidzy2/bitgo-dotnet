using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class InstantService : ApiService, IInstantService {
        
        internal InstantService(BitGoClient client) : base(client, "instant") {

        }

        public Task<InstantGuarantee> GetAsync(string instantId, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<InstantGuarantee>($"{_url}/{instantId}", true, cancellationToken);
        
    }
}