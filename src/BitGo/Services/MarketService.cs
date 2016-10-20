using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class MarketService : ApiService {
        
        internal MarketService(BitGoClient client) : base(client, "market") {

        }

        public Task<MarketData> GetAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<MarketData>($"{_url}/latest", false, cancellationToken);
        
    }
}
