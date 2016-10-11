using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class BlockchainData
    {
        [JsonProperty("totalbc")]
        public decimal TotalBTC { get; set; }

        [JsonProperty("transactions")]
        public long Transactions { get; set; }
    }
}
