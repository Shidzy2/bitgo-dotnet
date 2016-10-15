using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace BitGo.Helpers.Sjcl
{
    internal class SjclEncryptor
    {
        private readonly byte[] _key;

        private readonly byte[] _plainText;

        private readonly byte[] _salt;

        private readonly byte[] _iv;

        private readonly byte[] _adata;

        private readonly byte[] _ct;

        private readonly int _v = 1;

        private readonly int _iter = 10000;

        private readonly int _ks = 256;

        private readonly int _ts = 64;
        

        private readonly string _mode = "ccm";

        private readonly string _cipher = "aes";

        public SjclEncryptor(string plainText, string password)
        {
            var random = RandomNumberGenerator.Create();
            _plainText = Encoding.UTF8.GetBytes(plainText);
            _adata = new byte[0];
            _iv = new byte[12];
            _salt = new byte[8];
            random.GetBytes(_iv);
            random.GetBytes(_salt);
            _key = GenKeyBytes(password, _salt, _ks, _iter);

            var nonSecretPayload = new byte[] { };

            var cipher = new CcmBlockCipher(new AesFastEngine());
            var parameters = new CcmParameters(
                new KeyParameter(_key), _ts, _iv, nonSecretPayload);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(_plainText.Length)];
            var len = cipher.ProcessBytes(_plainText, 0, plainText.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);
            _ct = cipherText;
        }

        private static byte[] GenKeyBytes(string aPW, byte[] salt, int ks, int iter)
        {
            byte[] pw = Encoding.UTF8.GetBytes(aPW);
            PBKDF2HMACSHA256 k = new PBKDF2HMACSHA256(pw, salt, iter);
            return k.GetBytes(ks/8);
        }

        public string EncryptedText
        {
            get
            {
                return JsonConvert.SerializeObject(new SjclJson {
                    IV = Convert.ToBase64String(_iv),
                    V = _v,
                    Iter = _iter,
                    KS = _ks,
                    TS = _ts,
                    Mode = _mode,
                    AData = _adata.Length == 0 ? string.Empty : Convert.ToBase64String(_adata),
                    Cipher = _cipher,
                    Salt = Convert.ToBase64String(_salt),
                    CT = Convert.ToBase64String(_ct),
                });
            }
        }

        private class SjclJson
        {
            [JsonProperty("iv")]
            public string IV { get; internal set; }

            [JsonProperty("v")]
            public int V { get; internal set; }

            [JsonProperty("iter")]
            public int Iter { get; internal set; }

            [JsonProperty("ks")]
            public int KS { get; internal set; }

            [JsonProperty("ts")]
            public int TS { get; internal set; }

            [JsonProperty("mode")]
            public string Mode { get; internal set; }

            [JsonProperty("adata")]
            public string AData { get; internal set; }

            [JsonProperty("cipher")]
            public string Cipher { get; internal set; }

            [JsonProperty("salt")]
            public string Salt { get; internal set; }

            [JsonProperty("ct")]
            public string CT { get; internal set; }
        }
    }
}