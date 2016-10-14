using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class SetPolicyRuleActionParamsArgs {
        [JsonProperty("otpType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OtpType { get; internal set; }

        [JsonProperty("phone", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; internal set; }

        [JsonProperty("duration", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int Duration { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleActionArgs {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("actionParams", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public SetPolicyRuleActionParamsArgs ActionParams { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleLimitConditionArgs {
        [JsonProperty("amount", Required = Required.Always)]
        public long Amount { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleWebhookConditionArgs {
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleAddBitcoinAddressWhitelistConditionArgs {
        [JsonProperty("add", Required = Required.Always)]
        public string Add { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleRemoveBitcoinAddressWhitelistConditionArgs {
        [JsonProperty("remove", Required = Required.Always)]
        public string Remove { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class SetPolicyRuleArgs<T> {
        
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; internal set; }

        [JsonProperty("condition", Required = Required.Default)]
        public T Condition { get; internal set; }

        [JsonProperty("action", Required = Required.Default)]
        public SetPolicyRuleActionArgs Action { get; internal set; }

    }
}