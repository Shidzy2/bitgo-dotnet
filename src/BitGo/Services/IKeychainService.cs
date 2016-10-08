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
    public interface IKeychainService
    {
        Task<KeychainList> GetListAsync(int skip = 0, int limit = 100, CancellationToken cancellationToken = default(CancellationToken));

        Task<Keychain> GetAsync(string extendedPublicKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<Keychain> AddAsync(string extendedPublicKey, string encryptedExtendedPrivateKey = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Keychain> CreateAsync(string passphrase, string seedHex = null, CancellationToken cancellationToken = default(CancellationToken))
    }
}