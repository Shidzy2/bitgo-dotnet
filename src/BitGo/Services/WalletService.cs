using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class WalletService : ApiService, IWalletService {
        
        internal WalletService(BitGoClient client) : base(client, "wallet") {

        }

        public Task<WalletList> GetListAsync(int skip = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<WalletList>($"{_url}?skip={skip}&limit={limit}", true, cancellationToken);

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