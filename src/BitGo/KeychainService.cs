using System;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types.Keychain;
using BitGo.Args.Keychain;

namespace BitGo
{
    public sealed class KeychainService : ApiService {
        
        internal KeychainService(BitGoClient client) : base(client, "keychain") {

        }

        public Task<KeychainList> GetListAsync(int skip = 0, int limit = 100, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<KeychainList>($"{_url}?skip={skip}&limit={limit}", true, cancellationToken);

        public Task<Keychain> GetAsync(string extendedPublicKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", null, cancellationToken);

        public Task<Keychain> AddAsync(string extendedPublicKey, string encryptedExtendedPrivateKey = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}", new AddKeychainArgs { ExtendedPublicKey = extendedPublicKey, EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        public Task<Keychain> CreateBitGoAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/bitgo", null, cancellationToken);

        public Task<Keychain> CreateBackupAsync(string provider, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/backup", new CreateBackupKeychainArgs { Provider = provider }, cancellationToken);

        public Task<Keychain> UpdateAsync(string extendedPublicKey, string encryptedExtendedPrivateKey, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", new UpdateKeychainArgs { EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);
    }
}