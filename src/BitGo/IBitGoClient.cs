using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BitGo.Services;

namespace BitGo
{
    /// <summary>
    /// A client interface to use the BitGo API
    /// </summary>
    public interface IBitGoClient
    {
        IWalletService Wallets { get; }

        IKeychainService Keychains { get; } 

        IUserService User { get; } 
    }
}