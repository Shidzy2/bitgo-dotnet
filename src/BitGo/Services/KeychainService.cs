using System;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class KeychainService : ApiService, IKeychainService {
        
        internal KeychainService(BitGoClient client) : base(client, "keychain") {

        }

        public Task<KeychainList> GetListAsync(int skip = 0, int limit = 100, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<KeychainList>($"{_url}?skip={skip}&limit={limit}", true, cancellationToken);

        public Task<Keychain> GetAsync(string extendedPublicKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", null, cancellationToken);

        public Task<Keychain> AddAsync(string extendedPublicKey, string encryptedExtendedPrivateKey = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}", new AddKeychainArgs { ExtendedPublicKey = extendedPublicKey, EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        public async Task<Keychain> CreateAsync(string passphrase, string seedHex = null, CancellationToken cancellationToken = default(CancellationToken)) {
            var extKey = seedHex == null ? new ExtKey() : new ExtKey(seedHex);
            var extKeyStr = extKey.ToString(_client.Network);
            var keychain = await AddAsync(extKey.Neuter().ToString(_client.Network), _client.Encrypt(extKeyStr, passphrase), cancellationToken);
            keychain.ExtendedPrivateKey = extKeyStr;
            return keychain;
        }

        public Task<Keychain> UpdateAsync(string extendedPublicKey, string encryptedExtendedPrivateKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", new UpdateKeychainArgs { EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        public Task<Keychain> CreateBitGoAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/bitgo", null, cancellationToken);

        public Task<Keychain> CreateBackupAsync(string provider, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/backup", new CreateBackupKeychainArgs { Provider = provider }, cancellationToken);

        
    }
}