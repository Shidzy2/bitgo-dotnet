using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class PrivateKeychain: Keychain {

        [JsonIgnore]
        public string ExtendedPrivateKey { get; internal set; }
    }
}