using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletUnspentList: PagedResult {

        [JsonProperty("pendingTransactions", Required = Required.Default)]
        public bool PendingTransactions { get; internal set; }

        [JsonProperty("unspents", Required = Required.Always)]
        public WalletUnspent[] Unspents { get; internal set; }
    }
}