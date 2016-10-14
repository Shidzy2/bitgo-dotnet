using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicy {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("date", Required = Required.Default)]
        public DateTimeOffset Date { get; internal set; }

        [JsonProperty("rules", Required = Required.Default)]
        public WalletPolicyRule[] Rules { get; internal set; }

        [JsonProperty("version", Required = Required.Default)]
        public int Version { get; internal set; }
    }
}