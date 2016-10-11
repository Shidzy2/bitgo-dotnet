using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class TransactionResult
    {
        [JsonProperty("tx")]
        public string Hex { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("instant")]
        public bool Instant { get; set; }

        [JsonProperty("instantId")]
        public string InstantId { get; set; }

    }
}
