using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class Wallet {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("label", Required = Required.Always)]
        public string Label { get; set; }

        [JsonProperty("isActive", Required = Required.Default)]
        public bool IsActive { get; set; }

        [JsonProperty("type", Required = Required.Default)]
        public string Type { get; set; }

        [JsonProperty("private", Required = Required.Default)]
        public KeychainList Keychains { get; set; }

        [JsonProperty("permissions", Required = Required.Default)]
        public string Permissions { get; set; }

        [JsonProperty("spendingAccount", Required = Required.Default)]
        public bool IsSpendingAccount { get; set; }

        [JsonProperty("confirmedBalance", Required = Required.Default)]
        public long ConfirmedBalance { get; set; }

        [JsonProperty("balance", Required = Required.Default)]
        public long Balance { get; set; }
    }
}