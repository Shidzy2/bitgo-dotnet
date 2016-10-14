using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class PendingApproval {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("createDate", Required = Required.Default)]
        public DateTimeOffset CreateDate { get; internal set; }

        [JsonProperty("creator", Required = Required.Default)]
        public string Creator { get; internal set; }

        [JsonProperty("enterprise", Required = Required.Default)]
        public string Enterprise { get; internal set; }

        [JsonProperty("state", Required = Required.Default)]
        public string State { get; internal set; }
    }
}