using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class MarketData
    {
        [JsonProperty("latest")]
        public MarketData Latest { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, CurrencyData> Currencies { get; set; }
        
        [JsonProperty("blockchain")]
        public BlockchainData Blockchain { get; set; }

        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }
        
    }
}
