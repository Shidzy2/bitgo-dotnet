using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletUnspent
    {
        [JsonProperty("address")]
        public string Address { get; internal set; }

        [JsonProperty("blockHeight")]
        public int BlockHeight { get; internal set; }

        [JsonProperty("chainPath")]
        public string ChainPath { get; internal set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; internal set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; internal set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }

        [JsonProperty("script")]
        public string Script { get; internal set; }

        [JsonProperty("tx_hash")]
        public string TransactionHash { get; internal set; }

        [JsonProperty("tx_output_n")]
        public int TransactionOutputIndex { get; internal set; }

        [JsonProperty("value")]
        public long Value { get; internal set; }

        [JsonProperty("wallet")]
        public string WalletId { get; internal set; }

        [JsonProperty("instant")]
        public bool IsInstant { get; internal set; }
    }
}
