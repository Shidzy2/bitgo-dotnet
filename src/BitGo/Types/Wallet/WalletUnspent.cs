using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class WalletUnspent
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("blockHeight")]
        public int BlockHeight { get; set; }

        [JsonProperty("chainPath")]
        public string ChainPath { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("tx_hash")]
        public string TransactionHash { get; set; }

        [JsonProperty("tx_output_n")]
        public int TransactionOutputIndex { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("wallet")]
        public string WalletId { get; set; }

        [JsonProperty("instant")]
        public bool IsInstant { get; set; }
    }
}
