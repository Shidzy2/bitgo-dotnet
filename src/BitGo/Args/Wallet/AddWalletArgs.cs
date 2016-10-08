using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class AddWalletArgs {

        [JsonProperty("label", Required = Required.Always)]
        public string Label { get; internal set; }

        [JsonProperty("m", Required = Required.Always)]
        public int M { get; internal set; }

        [JsonProperty("n", Required = Required.Always)]
        public int N { get; internal set; }

        [JsonProperty("keychains", Required = Required.Always)]
        public AddWalletKeychainArgs[] Keychains { get; internal set; }

        [JsonProperty("enterprise", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Enterprise { get; internal set; }

        [JsonProperty("disableTransactionNotifications", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableTransactionNotifications { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class AddWalletKeychainArgs {
        [JsonProperty("xpub", Required = Required.Always)]
        public string ExtendedPublicKey { get; internal set; }
    }
}