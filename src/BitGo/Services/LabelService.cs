using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class LabelService : ApiService, ILabelService {
        
        internal LabelService(BitGoClient client) : base(client, "labels") {

        }

        public Task<WalletAddressLabelList> GetListAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<WalletAddressLabelList>($"{_url}", true, cancellationToken);

        public Task<WalletAddressLabelList> GetListByWalletAsync(string walletId, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<WalletAddressLabelList>($"{_url}/{walletId}", true, cancellationToken);

        public Task<WalletAddressLabel> SetAsync(string walletId, string address, string label, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PutAsync<WalletAddressLabel>($"{_url}/{walletId}/{address}", new SetLabelArgs { Label = label }, cancellationToken);

        public Task<WalletAddressLabel> DeleteAsync(string walletId, string address, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.DeleteAsync<WalletAddressLabel>($"{_url}/{walletId}/{address}", cancellationToken);
        
    }
}