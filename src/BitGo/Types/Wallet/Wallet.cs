using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class Wallet {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("label", Required = Required.Always)]
        public string Label { get; internal set; }

        [JsonProperty("isActive", Required = Required.Default)]
        public bool IsActive { get; internal set; }

        [JsonProperty("type", Required = Required.Default)]
        public string Type { get; internal set; }

        [JsonProperty("private", Required = Required.Default)]
        public KeychainList Keychains { get; internal set; }

        [JsonProperty("permissions", Required = Required.Default)]
        public string Permissions { get; internal set; }

        [JsonProperty("spendingAccount", Required = Required.Default)]
        public bool IsSpendingAccount { get; internal set; }

        [JsonProperty("confirmedBalance", Required = Required.Default)]
        public long ConfirmedBalance { get; internal set; }

        [JsonProperty("balance", Required = Required.Default)]
        public long Balance { get; internal set; }
    }
}