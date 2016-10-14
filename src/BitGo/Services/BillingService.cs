using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class BillingService : ApiService, IBillingService {
        
        internal BillingService(BitGoClient client) : base(client, "billing") {

        }

        public async Task<string> GetAddressAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => (await _client.PostAsync<BillingAddress>($"{_url}/address", null, cancellationToken)).Address;
        
    }
}