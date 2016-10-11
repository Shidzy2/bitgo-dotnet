using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class SendOtpArgs {

        [JsonProperty("forceSMS", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? ForceSms { get; internal set; }

    }
}