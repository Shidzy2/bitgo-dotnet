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
    public sealed class WalletService : ApiService
    {

        public WalletItemService this[string id]
        {
            get
            {
                return new WalletItemService(_client, this, id);
            }
        }

        internal WalletService(BitGoClient client) : base(client, "wallet")
        {

        }

        public Task<WalletList> GetListAsync(int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<WalletList>($"{_url}{_client.ConvertToQueryString(new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<Wallet> GetAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<Wallet>($"{_url}/{id}", true, cancellationToken);

        public Task<Wallet> AddAsync(string label, string[] extendedPublicKeys, string enterprise = null, bool? disableTransactionNotifications = null, int m = 2, int n = 3, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<Wallet>($"{_url}", new AddWalletArgs
            {
                Label = label,
                Keychains = extendedPublicKeys.Select(ek =>
                    new AddWalletKeychainArgs
                    {
                        ExtendedPublicKey = ek
                    }).ToArray(),
                M = m,
                N = n,
                Enterprise = enterprise,
                DisableTransactionNotifications = disableTransactionNotifications
            }, cancellationToken);

        public async Task<Wallet> CreateAsync(string label, string passphrase, string backupExtendedPublicKey = null, string backupProvider = null, string enterprise = null, bool? disableTransactionNotifications = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(backupExtendedPublicKey) && string.IsNullOrEmpty(backupProvider))
            {
                throw new ArgumentException("one of backupExtendedPublicKey or backupProvider must be specified");
            }
            if (!string.IsNullOrEmpty(backupExtendedPublicKey) && !string.IsNullOrEmpty(backupProvider))
            {
                throw new ArgumentException("please specify only one of backupExtendedPublicKey or backupProvider");
            }
            if (string.IsNullOrEmpty(passphrase))
            {
                throw new ArgumentException("passphrase cannot be null");
            }
            if (string.IsNullOrEmpty(label))
            {
                throw new ArgumentException("label cannot be empty");
            }
            var keychains = new List<Task<Keychain>>();
            keychains.Add(_client.Keychains.CreateAsync(passphrase, null, cancellationToken));
            keychains.Add(_client.Keychains.CreateBitGoAsync(cancellationToken));
            if (!string.IsNullOrEmpty(backupExtendedPublicKey))
            {
                keychains.Add(_client.Keychains.AddAsync(backupExtendedPublicKey, null, cancellationToken));
            }
            if (!string.IsNullOrEmpty(backupProvider))
            {
                keychains.Add(_client.Keychains.CreateBackupAsync(backupProvider, cancellationToken));
            }

            return await AddAsync(label, (await Task.WhenAll(keychains.ToArray())).Select(k => k.ExtendedPublicKey).ToArray(), enterprise, disableTransactionNotifications, 2, 3, cancellationToken);
        }
    }
}