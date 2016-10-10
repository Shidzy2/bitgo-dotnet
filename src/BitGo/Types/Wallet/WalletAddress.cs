using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{

    public class WalletAddress
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("chain")]
        public int Chain { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; set; }

        [JsonProperty("received")]
        public long Received { get; set; }

        [JsonProperty("sent")]
        public long Sent { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("txCount")]
        public int TxCount { get; set; }
    }
}
