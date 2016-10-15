using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace BitGo.Helpers.Sjcl
{
    internal class SjclManaged
    {
        private readonly int _v = 1;

        private readonly int _iter = 10000;

        private readonly int _ks = 256;

        private readonly int _ts = 64;
        
        private readonly string _mode = "ccm";

        private readonly string _cipher = "aes";

        public SjclManaged()
        {
            
        }

        private byte[] GenKeyBytes(string aPW, byte[] salt, int ks, int iter)
        {
            byte[] pw = Encoding.UTF8.GetBytes(aPW);
            PBKDF2HMACSHA256 k = new PBKDF2HMACSHA256(pw, salt, iter);
            return k.GetBytes(ks/8);
        }

        public string Decrypt(string encryptedText, string password) {
            var json = JsonConvert.DeserializeObject<SjclJson>(encryptedText);
            var v = json.V;
            var adata = Convert.FromBase64String(json.AData);
            var iv = Convert.FromBase64String(json.IV);
            var salt = Convert.FromBase64String(json.Salt);
            var ks = json.KS;
            var ts = json.TS;
            var iter = json.Iter;
            var ct = Convert.FromBase64String(json.CT);
            // var cipher = json.Cipher;
            // var mode = json.Mode;
            var key = GenKeyBytes(password, salt, ks, iter);

            var nonSecretPayload = new byte[] { };

            var cipher = new CcmBlockCipher(new AesFastEngine());
            var parameters = new CcmParameters(
                new KeyParameter(key), _ts, iv.Take(13).ToArray(), nonSecretPayload);
            cipher.Init(false, parameters);

            var plainText = new byte[cipher.GetOutputSize(ct.Length)];
            var len = cipher.ProcessBytes(ct, 0, ct.Length, plainText, 0);
            cipher.DoFinal(plainText, len);
            return Encoding.UTF8.GetString(plainText);
        }

        public string Encrypt(string plainText, string password) {
            var random = RandomNumberGenerator.Create();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var adata = new byte[0];
            var iv = new byte[16];
            var salt = new byte[8];
            random.GetBytes(iv);
            random.GetBytes(salt);
            var key = GenKeyBytes(password, salt, _ks, _iter);

            var nonSecretPayload = new byte[] { };

            var cipher = new CcmBlockCipher(new AesFastEngine());
            var parameters = new CcmParameters(
                new KeyParameter(key), _ts, iv.Take(13).ToArray(), nonSecretPayload);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(plainTextBytes.Length)];
            var len = cipher.ProcessBytes(plainTextBytes, 0, plainTextBytes.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);
            return JsonConvert.SerializeObject(new SjclJson {
                    IV = Convert.ToBase64String(iv),
                    V = _v,
                    Iter = _iter,
                    KS = _ks,
                    TS = _ts,
                    Mode = _mode,
                    AData = Convert.ToBase64String(adata),
                    Cipher = _cipher,
                    Salt = Convert.ToBase64String(salt),
                    CT = Convert.ToBase64String(cipherText),
            });
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