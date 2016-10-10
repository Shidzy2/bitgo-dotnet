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
        public string Account { get; set; }

        [JsonProperty("isMine")]
        public bool IsMine { get; set; }

        [JsonProperty("chain")]
        public int Chain { get; set; }

        [JsonProperty("chainIndex")]
        public long ChainIndex { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("vout")]
        public int VOut { get; set; }
    }
}
