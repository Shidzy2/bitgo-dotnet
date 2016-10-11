using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;
using System.Security.Cryptography;
using System.Text;

namespace BitGo.Services
{
    public sealed class UserService : ApiService, IUserService {
        
        internal UserService(BitGoClient client) : base(client, "user") {

        }

        public async Task<User> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.GetAsync<UserResult>($"{_url}/me", true, cancellationToken))?.User;
        public async Task<UserSession> GetSessionAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (await _client.GetAsync<UserSessionResult>($"{_url}/session", true, cancellationToken))?.Session;

        public Task LockAsync(CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PostAsync<object>($"{_url}/lock", null, cancellationToken);

        public Task<UserResult> LoginAsync(string email, string password, string otp, bool? extensible = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<UserResult>($"{_url}/login", new LoginUserArgs { Email = email, Password = CalculateHmac(email, password), Otp = otp, Extensible = extensible }, cancellationToken);
        
        private string CalculateHmac(string email, string password) {
            Console.WriteLine(BitConverter.ToString(new HMACSHA256(Encoding.UTF8.GetBytes(email)).ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-","").ToLower());
            return BitConverter.ToString(new HMACSHA256(Encoding.UTF8.GetBytes(email)).ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-","").ToLower();
        }

        public Task LogoutAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<object>($"{_url}/logout", true, cancellationToken);

        public Task SendOtpAsync(bool? forceSms = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<object>($"{_url}/sendotp", new SendOtpArgs { ForceSms = forceSms }, cancellationToken);

        public Task UnlockAsync(string otp, TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<object>($"{_url}/sendotp", new UnlockUserArgs { Otp = otp, Duration = (int?)duration?.TotalSeconds }, cancellationToken);

        // public Task<KeychainList> GetListAsync(int? skip = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.GetAsync<KeychainList>($"{_url}{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "skip", skip }, { "limit", limit } })}", true, cancellationToken);

        // public Task<Keychain> GetAsync(string extendedPublicKey, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", null, cancellationToken);

        // public Task<Keychain> AddAsync(string extendedPublicKey, string encryptedExtendedPrivateKey = null, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Keychain>($"{_url}", new AddKeychainArgs { ExtendedPublicKey = extendedPublicKey, EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        // public async Task<Keychain> CreateAsync(string passphrase, string seedHex = null, CancellationToken cancellationToken = default(CancellationToken)) {
        //     var extKey = seedHex == null ? new ExtKey() : new ExtKey(seedHex);
        //     var extKeyStr = extKey.ToString(_client.Network);
        //     var keychain = await AddAsync(extKey.Neuter().ToString(_client.Network), _client.Encrypt(extKeyStr, passphrase), cancellationToken);
        //     keychain.ExtendedPrivateKey = extKeyStr;
        //     return keychain;
        // }

        // public Task<Keychain> UpdateAsync(string extendedPublicKey, string encryptedExtendedPrivateKey, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Keychain>($"{_url}/{extendedPublicKey}", new UpdateKeychainArgs { EncryptedExtendedPrivateKey = encryptedExtendedPrivateKey }, cancellationToken);

        // public Task<Keychain> CreateBitGoAsync(CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Keychain>($"{_url}/bitgo", null, cancellationToken);

        // public Task<Keychain> CreateBackupAsync(string provider, CancellationToken cancellationToken = default(CancellationToken)) 
        //     => _client.PostAsync<Keychain>($"{_url}/backup", new CreateBackupKeychainArgs { Provider = provider }, cancellationToken);


    }
}