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
    public sealed class UserService : ApiService {
        
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
            return BitConverter.ToString(new HMACSHA256(Encoding.UTF8.GetBytes(email)).ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-","").ToLower();
        }

        public Task LogoutAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<object>($"{_url}/logout", true, cancellationToken);

        public Task SendOtpAsync(bool? forceSms = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<object>($"{_url}/sendotp", new SendOtpArgs { ForceSms = forceSms }, cancellationToken);

        public Task UnlockAsync(string otp, TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<object>($"{_url}/unlock", new UnlockUserArgs { Otp = otp, Duration = (int?)duration?.TotalSeconds }, cancellationToken);

    }
}