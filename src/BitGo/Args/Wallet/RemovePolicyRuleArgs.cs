using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class RemovePolicyRuleArgs {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

    }
}