using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class TransactionFeeEstimate
    {
        [JsonProperty("confidence")]
        public decimal Confidence { get; set; }

        [JsonProperty("feePerKb")]
        public long FeePerKb { get; set; }

        [JsonProperty("multiplier")]
        public decimal Multiplier { get; set; }

        [JsonProperty("numBlocks")]
        public int NumberOfBlocks { get; set; }

        [JsonProperty("feeByBlockTarget")]
        public Dictionary<string, long> FeeByBlockTarget { get; set; }
    }
}
