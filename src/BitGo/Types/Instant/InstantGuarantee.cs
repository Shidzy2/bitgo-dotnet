using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class InstantGuarantee
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("guarantee")]
        public string Guarantee { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("createTime")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("normalizedHash")]
        public string NormalizedHash { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        public bool IsValid()
        {
            return new BitcoinPubKeyAddress("1BitGo3gxRZ6mQSEH52dvCKSUgVCAH4Rja", Network.Main).VerifyMessage(Guarantee, Convert.ToBase64String(new NBitcoin.DataEncoders.HexEncoder().DecodeData(Signature)));
        }
    }
}
