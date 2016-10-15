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

        private readonly RandomNumberGenerator _random;

        public SjclManaged()
        {
            _random = RandomNumberGenerator.Create();
        }

        private byte[] GenKeyBytes(string aPW, byte[] salt, int ks, int iter)
        {
            byte[] pw = Encoding.UTF8.GetBytes(aPW);
            var k = new PBKDF2HMACSHA256(pw, salt, iter);
            return k.GetBytes(ks / 8);
        }

        public string Decrypt(string json, string password)
        {
            var jsonObj = JsonConvert.DeserializeObject<SjclJson>(json);
            var v = jsonObj.V;
            var adata = Convert.FromBase64String(jsonObj.AData);
            var iv = Convert.FromBase64String(jsonObj.IV);
            var salt = Convert.FromBase64String(jsonObj.Salt);
            var ks = jsonObj.KS;
            var ts = jsonObj.TS;
            var iter = jsonObj.Iter;
            var ct = Convert.FromBase64String(jsonObj.CT);
            // var cipher = json.Cipher;
            // var mode = json.Mode;
            var key = GenKeyBytes(password, salt, ks, iter);

            var nonSecretPayload = new byte[] { };

            var cipher = new CcmBlockCipher(new AesFastEngine());
            var parameters = new CcmParameters(
                new KeyParameter(key), ts, iv.Take(13).ToArray(), nonSecretPayload);
            cipher.Init(false, parameters);

            var plainText = new byte[cipher.GetOutputSize(ct.Length)];
            var len = cipher.ProcessBytes(ct, 0, ct.Length, plainText, 0);
            cipher.DoFinal(plainText, len);
            return Encoding.UTF8.GetString(plainText);
        }

        public string Encrypt(string plainText, string password)
        {

            var v = 1;
            var iter = 10000;
            var ks = 256;
            var ts = 64;
            // var mode = "ccm";
            // var cipher = "aes";

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var adata = new byte[0];
            var iv = new byte[16];
            var salt = new byte[8];
            _random.GetBytes(iv);
            _random.GetBytes(salt);
            var key = GenKeyBytes(password, salt, ks, iter);

            var nonSecretPayload = new byte[] { };

            var cipher = new CcmBlockCipher(new AesFastEngine());
            var parameters = new CcmParameters(
                new KeyParameter(key), ts, iv.Take(13).ToArray(), nonSecretPayload);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(plainTextBytes.Length)];
            var len = cipher.ProcessBytes(plainTextBytes, 0, plainTextBytes.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);
            return JsonConvert.SerializeObject(new SjclJson
            {
                IV = Convert.ToBase64String(iv),
                V = v,
                Iter = iter,
                KS = ks,
                TS = ts,
                Mode = "ccm",
                Cipher = "aes",
                AData = Convert.ToBase64String(adata),
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