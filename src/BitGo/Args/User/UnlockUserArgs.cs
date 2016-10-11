using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class UnlockUserArgs {

        [JsonProperty("otp", Required = Required.Always)]
        public string Otp { get; internal set; }

        [JsonProperty("duration", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Duration { get; internal set; }

    }
}