using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class KeychainList: PagedResult {

        [JsonProperty("keychains", Required = Required.Always)]
        public Keychain[] Keychains { get; internal set; }
    }
}