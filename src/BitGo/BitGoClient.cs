using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using NBitcoin;
using BitGo.Helpers.Sjcl;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using BitGo.Exceptions;
using BitGo.Services;

namespace BitGo
{
    public enum BitGoNetwork
    {
        Main,
        Test
    }

    /// <summary>
    /// A client to use the BitGo API
    /// </summary>
    public class BitGoClient : IBitGoClient
    {
        private const string Version = "0.0.1";
        private const string MainBaseUrl = "https://www.bitgo.com/api/v1/";
        private const string TestBaseUrl = "https://test.bitgo.com/api/v1/";
        private readonly Uri _baseUrl;
        private SecureString _token;
        private readonly BitGoNetwork _network;

        private readonly IKeychainService _keychainService;

        private readonly IWalletService _walletService;

        private readonly IWalletAddressService _walletAddressService;

        private readonly IUserService _userService;

        private readonly IBillingService _billingService;

        private readonly IInstantService _instantService;

        private readonly IMarketService _marketService;

        private readonly ITransactionService _transactionService;

        private readonly ILabelService _labelService;

        private readonly IWebhookService _webhookService;

        private readonly IPendingApprovalService _pendingApprovalService;


        internal Network Network
        {
            get
            {
                return _network == BitGoNetwork.Main ? Network.Main : Network.TestNet;
            }
        }

        public IKeychainService Keychains
        {
            get
            {
                return _keychainService;
            }
        }

        public IWalletService Wallets
        {
            get
            {
                return _walletService;
            }
        }

        public IWalletAddressService WalletAddresses
        {
            get
            {
                return _walletAddressService;
            }
        }

        public IUserService User
        {
            get
            {
                return _userService;
            }
        }

        public IBillingService Billing 
        {
            get
            {
                return _billingService;
            }
        }

        public IInstantService Instant 
        {
            get
            {
                return _instantService;
            }
        }

        public ITransactionService Transaction 
        {
            get
            {
                return _transactionService;
            }
        }

        public ILabelService Labels 
        {
            get
            {
                return _labelService;
            }
        }

        public IMarketService Market 
        {
            get
            {
                return _marketService;
            }
        }

        public IWebhookService Webhooks 
        {
            get
            {
                return _webhookService;
            }
        }

        public IPendingApprovalService PendingApprovals
        {
            get
            {
                return _pendingApprovalService;
            }
        }

        public BitGoClient(string token = null) : this(BitGoNetwork.Main, token)
        {

        }

        public BitGoClient(BitGoNetwork network, string token = null)
        {
            _network = network;
            _baseUrl = new Uri(network == BitGoNetwork.Main ? MainBaseUrl : TestBaseUrl);
            if (!string.IsNullOrEmpty(token))
            {
                _token = ConvertToSecureString(token);
            }
            _keychainService = new KeychainService(this);
            _walletService = new WalletService(this);
            _walletAddressService = new WalletAddressService(this);
            _userService = new UserService(this);
            _labelService = new LabelService(this);
            _marketService = new MarketService(this);
            _transactionService = new TransactionService(this);
            _instantService = new InstantService(this);
            _billingService = new BillingService(this);
            _webhookService = new WebhookService(this);
            _pendingApprovalService = new PendingApprovalService(this);
        }

        public void SetAccessToken(string token)
        {
            _token = ConvertToSecureString(token);
        }

        public string Decrypt(string input, string password)
        {
            return new SjclDecryptor(input, password).Plaintext;
        }

        public string Encrypt(string input, string password)
        {
            return new SjclEncryptor(input, password).EncryptedText;
        }


        internal HttpClient GetHttpClient(bool authenticated = true)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            var client = new HttpClient(handler);
            client.BaseAddress = _baseUrl;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BitGoDotNet", Version));
            if (authenticated && _token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConvertToString(_token));
            }
            return client;
        }

        internal string ConvertToQueryString(Dictionary<string, object> nvc)
        {
            var array = nvc
                .Where(keyValue => keyValue.Value != null)
                .Select(keyValue => new KeyValuePair<string, string>(keyValue.Key, ConvertValueToString(keyValue.Value)))
                .Select(keyValue => $"{WebUtility.UrlEncode(keyValue.Key)}={WebUtility.UrlEncode(keyValue.Value)}")
                .ToArray();
            return array.Any() ? "?" + string.Join("&", array) : string.Empty;
        }

        private string ConvertValueToString(object value)
        {
            if (value is bool || value is bool?)
            {
                return ((bool)value) ? "true" : "false";
            }
            if (value is string)
            {
                return (string)value;
            }
            return value?.ToString();
        }

        internal async Task<T> GetAsync<T>(string url, bool authenticated = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = GetHttpClient(authenticated))
            {
                var response = await client.GetAsync($"{url}", cancellationToken);
                Console.WriteLine(response.RequestMessage.RequestUri);
                string content = await response.Content.ReadAsStringAsync();

                try
                {
                    response.EnsureSuccessStatusCode();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T);
                    }
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedException(content, ex);
                        case HttpStatusCode.NotFound:
                            throw new NotFoundException(content, ex);
                        default:
                            throw new Exception(content, ex);
                    }
                }
            }
        }

        internal async Task<T> PostAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PostAsync($"{url}",  new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T);
                    }
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedException(content, ex);
                        case HttpStatusCode.NotFound:
                            throw new NotFoundException(content, ex);
                        default:
                            throw new Exception(content, ex);
                    }
                }
            }
        }

        internal async Task<T> PutAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PutAsync($"{url}",  new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T);
                    }
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedException(content, ex);
                        case HttpStatusCode.NotFound:
                            throw new NotFoundException(content, ex);
                        default:
                            throw new Exception(content, ex);
                    }
                }
            }
        }

        internal async Task<T> DeleteAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            using (var client = GetHttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}");
                var data = JsonConvert.SerializeObject(obj ?? new object());
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request, cancellationToken);
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T);
                    }
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedException(content, ex);
                        case HttpStatusCode.NotFound:
                            throw new NotFoundException(content, ex);
                        default:
                            throw new Exception(content, ex);
                    }
                }
            }
        }

        internal SecureString ConvertToSecureString(string str)
        {
            var secureStr = new SecureString();
            if (str.Length > 0)
            {
                foreach (var c in str.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        internal string ConvertToString(SecureString secStr)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
#if NETSTANDARD1_6
                unmanagedString = SecureStringMarshal.SecureStringToGlobalAllocUnicode(secStr);
#else
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secStr);
#endif
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}