using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace BitGo.Types
{
    public class WalletTransaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("blockhash")]
        public string BlockHash { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("pending")]
        public bool IsPending { get; set; }

        [JsonProperty("instant")]
        public bool IsInstant { get; set; }

        [JsonProperty("instantId")]
        public string InstantId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("sequenceId")]
        public string SequenceId { get; set; }

        [JsonProperty("hex")]
        public string Hex { get; set; }

        [JsonProperty("entries")]
        public WalletTransactionEntry[] Entries { get; set; }

        [JsonProperty("outputs")]
        public WalletTransactionEntry[] Outputs { get; set; }


        private string _normalizedHash = null;
        [JsonIgnore]
        public string NormalizedHash
        {
            get
            {
                if (string.IsNullOrEmpty(_normalizedHash))
                {
                    var tx = new Transaction(Hex);
                    foreach (var input in tx.Inputs)
                    {
                        input.ScriptSig = new Script();
                    }
                    _normalizedHash = tx.GetHash().ToString();
                }
                return _normalizedHash;
            }
        }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("signedDate")]
        public DateTime SignedDate { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
