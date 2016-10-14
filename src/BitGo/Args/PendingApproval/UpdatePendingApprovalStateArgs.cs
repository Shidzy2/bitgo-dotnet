using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class UpdatePendingApprovalStateArgs {

        [JsonProperty("state", Required = Required.Always)]
        public string State { get; internal set; }

    }
}