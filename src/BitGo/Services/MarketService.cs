using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class MarketService : ApiService, IMarketService {
        
        internal MarketService(BitGoClient client) : base(client, "instant") {

        }

        public Task<MarketData> GetAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<MarketData>($"{_url}/latest", true, cancellationToken);
        
    }
}