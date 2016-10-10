using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletTransactionList: PagedResult {

        [JsonProperty("transactions", Required = Required.Always)]
        public WalletTransaction[] Transactions { get; internal set; }
    }
}