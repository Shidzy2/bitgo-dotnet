using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyRuleActionParams {
        [JsonProperty("otpType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OtpType { get; set; }

        [JsonProperty("phone", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("duration", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int Duration { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyRuleAction {
        // “deny”, “getApproval”, “getOTP”
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("actionParams", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public WalletPolicyRuleActionParams ActionParams { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyRuleCondition {
        [JsonProperty("amount", Required = Required.Default)]
        public long Amount { get; internal set; }

        [JsonProperty("addresses", Required = Required.Default)]
        public string[] Addresses { get; internal set; }

        [JsonProperty("url", Required = Required.Default)]
        public string Url { get; internal set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class WalletPolicyRule {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        //“transactionLimit”, “dailyLimit”, “bitcoinAddressWhitelist”, “webhook”
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("action", Required = Required.Default)]
        public WalletPolicyRuleAction Action { get; internal set; }

        [JsonProperty("condition", Required = Required.Default)]
        public WalletPolicyRuleCondition Condition { get; internal set; }
    }
}