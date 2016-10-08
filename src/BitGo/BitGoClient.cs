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
        private const string MainBaseUrl = "https://www.bitgo.com/api/v1";
        private const string TestBaseUrl = "https://test.bitgo.com/api/v1";
        private readonly Uri _baseUrl;
        private readonly SecureString _token;
        private readonly BitGoNetwork _network;

        private readonly KeychainService _keychainService;

        internal Network Network
        {
            get
            {
                return _network == BitGoNetwork.Main ? Network.Main : Network.TestNet;
            }
        }

        public KeychainService Keychains
        {
            get
            {
                return _keychainService;
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
        }



        internal string Decrypt(string input, string password)
        {
            SjclDecryptor sd = new SjclDecryptor(input, password);
            return sd.Plaintext;
        }


        internal HttpClient GetHttpClient(bool authenticated = true)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BitGoDotNet", Version));
            if (authenticated)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConvertToString(_token));
            }
            return client;
        }

        internal async Task<T> GetAsync<T>(string url, bool authenticated = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = GetHttpClient(authenticated))
            {
                var response = await client.GetAsync($"{_baseUrl}/{url}", cancellationToken);
                string content = await response.Content.ReadAsStringAsync();

                try
                {
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    throw new Exception(content, ex);
                }
            }
        }

        internal async Task<T> PostAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PostAsync($"{_baseUrl}/{url}",
                            new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    throw new Exception(content, ex);
                }
            }
        }

        internal async Task<T> PutAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PutAsync($"{_baseUrl}/{url}",
                            new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception ex)
                {
                    throw new Exception(content, ex);
                }
            }
        }

        static SecureString ConvertToSecureString(string str)
        {
            var secureStr = new SecureString();
            if (str.Length > 0)
            {
                foreach (var c in str.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        static string ConvertToString(SecureString secStr)
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