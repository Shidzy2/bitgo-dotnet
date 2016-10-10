using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class FreezeWalletArgs {

        [JsonProperty("duration", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Duration { get; internal set; }

    }
}