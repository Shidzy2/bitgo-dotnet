using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class Keychain {

        [JsonProperty("xpub", Required = Required.Always)]
        public string ExtendedPublicKey { get; set; }

        [JsonProperty("encryptedXprv", Required = Required.Default)]
        public string EncryptedExtendedPrivateKey { get; internal set; }

        [JsonProperty("path", Required = Required.Default)]
        public string Path { get; set; }

        [JsonProperty("isBitGo", Required = Required.Default)]
        public bool IsBitGo { get; internal set; }
        
        [JsonIgnore]
        public string ExtendedPrivateKey { get; set; }
    }
}