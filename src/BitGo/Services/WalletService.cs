using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class WalletService : ApiService, IWalletService {
        
        public IWalletItemService this[string id]
        {
            get 
            {
                return new WalletItemService(_client, this, id);
            }
        }

        internal WalletService(BitGoClient client) : base(client, "wallet") {

        }

        public Task<WalletList> GetListAsync(int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<WalletList>($"{_url}{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<Wallet> GetAsync(string id, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<Wallet>($"{_url}/{id}", true, cancellationToken);

        public Task<Wallet> AddAsync(string label, string[] extendedPublicKeys, string enterprise = null, bool? disableTransactionNotifications = null, int m = 2, int n = 3,  CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Wallet>($"{_url}", new AddWalletArgs { 
                                                                    Label = label,
                                                                    Keychains = extendedPublicKeys.Select(ek => 
                                                                        new AddWalletKeychainArgs { 
                                                                            ExtendedPublicKey = ek  
                                                                        }).ToArray(),
                                                                    M = m,
                                                                    N = n,
                                                                    Enterprise = enterprise,
                                                                    DisableTransactionNotifications = disableTransactionNotifications
                                                                }, cancellationToken);

        public async Task<Wallet> CreateAsync(string label, string passphrase, string backupExtendedPublicKey = null, string backupProvider = null, string enterprise = null, bool? disableTransactionNotifications = null, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }
    }
}