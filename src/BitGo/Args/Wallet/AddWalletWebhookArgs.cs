using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class AddWalletWebhookArgs {

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; internal set; }

        [JsonProperty("numConfirmations", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? NumConfirmations { get; internal set; }

    }
}