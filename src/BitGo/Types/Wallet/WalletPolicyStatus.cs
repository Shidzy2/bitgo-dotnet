using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyStatusList {

        [JsonProperty("statusResults", Required = Required.Always)]
        public WalletPolicyStatus[] Statuses { get; internal set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyRuleStatus {
        [JsonProperty("remaining", Required = Required.Default)]
        public long Remaining { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyStatus {

        [JsonProperty("policy", Required = Required.Default)]
        public string Policy { get; internal set; }

        [JsonProperty("ruleId", Required = Required.Default)]
        public string RuleId { get; internal set; }

        [JsonProperty("status", Required = Required.Default)]
        public WalletPolicyRuleStatus Status { get; internal set; }
    }

}