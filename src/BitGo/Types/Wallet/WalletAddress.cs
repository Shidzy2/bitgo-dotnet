using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletAddress
    {
        [JsonProperty("address")]
        public string Address { get; internal set; }

        [JsonProperty("chain")]
        public int Chain { get; internal set; }

        [JsonProperty("index")]
        public int Index { get; internal set; }

        [JsonProperty("path")]
        public string Path { get; internal set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }

        [JsonProperty("received")]
        public long Received { get; internal set; }

        [JsonProperty("sent")]
        public long Sent { get; internal set; }

        [JsonProperty("balance")]
        public long Balance { get; internal set; }

        [JsonProperty("txCount")]
        public int TxCount { get; internal set; }

        [JsonProperty("wallet")]
        public string Wallet { get; internal set; }
    }
}
