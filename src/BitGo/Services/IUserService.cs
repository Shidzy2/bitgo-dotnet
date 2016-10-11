using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Types;

namespace BitGo.Services
{
    /// <summary>
    /// A Keychain service interface to use the BitGo API
    /// </summary>
    public interface IUserService
    {
        Task<User> GetAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<UserResult> LoginAsync(string email, string password, string otp, bool? extensible = null, CancellationToken cancellationToken = default(CancellationToken));

        Task LogoutAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<UserSession> GetSessionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task SendOtpAsync(bool? forceSms = null, CancellationToken cancellationToken = default(CancellationToken));

        Task UnlockAsync(string otp, TimeSpan? duration = null, CancellationToken cancellationToken = default(CancellationToken));

        Task LockAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}