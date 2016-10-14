using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class PendingApprovalList {

        [JsonProperty("pendingApprovals", Required = Required.Always)]
        public PendingApproval[] PendingApprovals { get; internal set; }
    }
}