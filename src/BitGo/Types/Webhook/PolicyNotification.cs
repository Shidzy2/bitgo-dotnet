using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PolicyNotificationOutput
    {
        [JsonProperty("outputAddress")]
        public string OutputAddress { get; set; }

        [JsonProperty("outputWallet")]
        public string OutputWallet { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PolicyNotification
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("walletId")]
        public string WalletId { get; set; }

        [JsonProperty("ruleId")]
        public string RuleId { get;  set; }

        [JsonProperty("outputs")]
        public PolicyNotificationOutput[] Outputs { get;  set; }

        [JsonProperty("spendAmount")]
        public long SpendAmount { get;  set; }

        [JsonProperty("approvalCount")]
        public int AapprovalCount { get;  set; }

        [JsonProperty("unsignedRawTx")]
        public string UnsignedRawTransaction { get;  set; }

        [JsonProperty("sequenceId")]
        public string SequenceId { get;  set; }

    }
}