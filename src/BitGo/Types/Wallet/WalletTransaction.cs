using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletTransaction
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("blockhash")]
        public string BlockHash { get; internal set; }

        [JsonProperty("height")]
        public int Height { get; internal set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; internal set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; internal set; }

        [JsonProperty("fee")]
        public long Fee { get; internal set; }

        [JsonProperty("pending")]
        public bool IsPending { get; internal set; }

        [JsonProperty("instant")]
        public bool IsInstant { get; internal set; }

        [JsonProperty("instantId")]
        public string InstantId { get; internal set; }

        [JsonProperty("comment")]
        public string Comment { get; internal set; }

        [JsonProperty("sequenceId")]
        public string SequenceId { get; internal set; }

        [JsonProperty("hex")]
        public string Hex { get; internal set; }

        [JsonProperty("entries")]
        public WalletTransactionEntry[] Entries { get; internal set; }

        [JsonProperty("outputs")]
        public WalletTransactionEntry[] Outputs { get; internal set; }


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
        public long Amount { get; internal set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; internal set; }

        [JsonProperty("creator")]
        public string Creator { get; internal set; }

        [JsonProperty("signedDate")]
        public DateTimeOffset SignedDate { get; internal set; }

        [JsonProperty("size")]
        public long Size { get; internal set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; internal set; }

        [JsonProperty("state")]
        public string State { get; internal set; }
    }
}
