using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletAddressList: PagedResult {

        [JsonProperty("addresses", Required = Required.Always)]
        public WalletAddress[] Addresses { get; internal set; }
    }
}