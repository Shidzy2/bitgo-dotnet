using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class AddWebhookArgs {

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; internal set; }

        [JsonProperty("coin", Required = Required.Always)]
        public string Coin { get; internal set; }

    }
}