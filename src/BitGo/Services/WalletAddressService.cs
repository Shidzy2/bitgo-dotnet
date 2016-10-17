using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class WalletAddressService : ApiService {
        
        internal WalletAddressService(BitGoClient client) : base(client, "walletaddress") {

        }

        public Task<WalletAddress> GetAsync(string address, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<WalletAddress>($"{_url}/{address}", true, cancellationToken);
        
    }
}