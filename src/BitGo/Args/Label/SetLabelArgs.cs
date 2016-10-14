using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class SetLabelArgs {

        [JsonProperty("label", Required = Required.Always)]
        public string Label { get; internal set; }

    }
}