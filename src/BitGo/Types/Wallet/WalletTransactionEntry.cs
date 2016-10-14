using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class WalletTransactionEntry
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }

        [JsonProperty("isMine")]
        public bool IsMine { get; internal set; }

        [JsonProperty("chain")]
        public int Chain { get; internal set; }

        [JsonProperty("chainIndex")]
        public long ChainIndex { get; internal set; }

        [JsonProperty("value")]
        public long Value { get; internal set; }

        [JsonProperty("vout")]
        public int VOut { get; internal set; }
    }
}
