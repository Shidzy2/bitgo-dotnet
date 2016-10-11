using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletAddressLabelList: PagedResult {

        [JsonProperty("labels", Required = Required.Always)]
        public WalletAddressLabel[] Labels { get; internal set; }
    }
}