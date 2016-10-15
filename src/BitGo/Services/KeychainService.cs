using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class KeychainService : ApiService, IKeychainService {
        
        internal KeychainService(BitGoClient client) : base(client, "keychain") {

        }

        public Task<KeychainList> GetListAsync(int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<KeychainList>($"{_url}{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        public Task<Keychain> GetAsync(string extendedPublicKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", null, cancellationToken);

        public Task<Keychain> AddAsync(string extendedPublicKey, string encryptedExtendedPrivateKey = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}", new AddKeychainArgs { ExtendedPublicKey = extendedPublicKey, EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        public Task<Keychain> CreateAsync(string passphrase, string seedHex = null, CancellationToken cancellationToken = default(CancellationToken)) {
            var extKey = (seedHex == null ? new ExtKey() : new ExtKey(seedHex));
            return AddAsync(extKey.Neuter().ToString(_client.Network), _client.Encrypt(extKey.ToString(_client.Network), passphrase), cancellationToken);
        }

        public Task<Keychain> UpdateAsync(string extendedPublicKey, string encryptedExtendedPrivateKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", new UpdateKeychainArgs { EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        public Task<Keychain> CreateBitGoAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/bitgo", null, cancellationToken);

        public Task<Keychain> CreateBackupAsync(string provider, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/backup", new CreateBackupKeychainArgs { Provider = provider }, cancellationToken);

        
    }
}