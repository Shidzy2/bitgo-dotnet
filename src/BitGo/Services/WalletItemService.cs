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
    public sealed class WalletItemService : ApiService, IWalletItemService {
        
        private readonly IWalletService _walletService;
        private readonly string _id;

        internal WalletItemService(BitGoClient client, IWalletService walletService, string id) : base(client, "wallet/{id}") {
            _walletService = walletService;
            _id = id;
        }

        public Task<Wallet> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _walletService.GetAsync(_id, cancellationToken);

        public Task<WalletTransactionList> GetTransactionListAsync(bool? compact = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransactionList>($"{_url}/tx{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "compact", compact }, { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<WalletTransaction> GetTransactionAsync(string hash, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransaction>($"{_url}/tx/{hash}", true, cancellationToken);

        public Task<WalletTransaction> GetTransactionBySequenceAsync(string sequenceId, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletTransaction>($"{_url}/tx/sequence/{sequenceId}", true, cancellationToken);

        public Task<WalletAddressList> GetAddressListAsync(int? chain = null, int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddressList>($"{_url}/addresses{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "chain", chain }, { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<WalletAddress> GetAddressAsync(string address, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletAddress>($"{_url}/addresses/{address}", true, cancellationToken);

        public Task<WalletAddress> CreateAddressAsync(int chain = 0, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletAddress>($"{_url}/addresses/{chain}", null, cancellationToken);

        public Task<WalletUnspentList> GetUnspentListAsync(bool? instant = null, long? target = null, int? skip = null, int? limit = null, long? minSize = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletUnspentList>($"{_url}/unspents{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "instant", instant }, { "target", target }, { "skip", skip }, { "limit", limit }, { "minSize", minSize }, })}", true, cancellationToken);

        public async Task<long> GetBillingFeeAsync(long amount, bool instant = false, CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.GetAsync<BillingFee>($"{_url}/billing/fee{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "amount", amount }, { "instant", instant } })}", true, cancellationToken)).Fee;

        public Task<WalletFreeze> FreezeAsync(TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<WalletFreeze>($"{_url}/freeze", new FreezeWalletArgs { Duration = (int?)duration?.TotalSeconds }, cancellationToken);



        // public Task<WalletList> GetListAsync(int skip = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.GetAsync<WalletList>($"{_url}?skip={skip}&limit={limit}", true, cancellationToken);

        // public Task<Wallet> GetAsync(string id, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.GetAsync<Wallet>($"{_url}/{id}", true, cancellationToken);

        // public Task<Wallet> AddAsync(string label, string[] extendedPublicKeys, string enterprise = null, bool? disableTransactionNotifications = null, int m = 2, int n = 3,  CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Wallet>($"{_url}", new AddWalletArgs { 
        //                                                             Label = label,
        //                                                             Keychains = extendedPublicKeys.Select(ek => 
        //                                                                 new AddWalletKeychainArgs { 
        //                                                                     ExtendedPublicKey = ek  
        //                                                                 }).ToArray(),
        //                                                             M = m,
        //                                                             N = n,
        //                                                             Enterprise = enterprise,
        //                                                             DisableTransactionNotifications = disableTransactionNotifications
        //                                                         }, cancellationToken);

        // public async Task<Wallet> CreateAsync(string label, string passphrase, string backupExtendedPublicKey = null, string backupProvider = null, string enterprise = null, bool? disableTransactionNotifications = null, CancellationToken cancellationToken = default(CancellationToken)) {
        //     throw new NotImplementedException();
        // }
    }
}