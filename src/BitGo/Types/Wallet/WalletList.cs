using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletList: PagedResult {

        [JsonProperty("wallets", Required = Required.Always)]
        public Wallet[] Wallets { get; internal set; }
    }
}