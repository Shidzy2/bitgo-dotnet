using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    /// <summary>
    /// This object represents an instant guarantee message.
    /// </summary>
    public class InstantGuarantee
    {
        /// <summary>
        /// The instant guarantee ID on BitGo
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// The message by BitGo to guarantee the instant transaction
        /// </summary>
        [JsonProperty("guarantee")]
        public string Guarantee { get; internal set; }

        /// <summary>
        /// Amount in Satoshis of the instant guarantee
        /// </summary>
        [JsonProperty("amount")]
        public long Amount { get; internal set; }

        /// <summary>
        /// The time at which the transaction was created
        /// </summary>
        [JsonProperty("createTime")]
        public DateTimeOffset CreateTime { get; internal set; }

        /// <summary>
        /// The hash of the guaranteed transaction
        /// </summary>
        [JsonProperty("transactionId")]
        public string TransactionId { get; internal set; }

        /// <summary>
        /// The hash of the guaranteed transaction without signatures
        /// </summary>
        [JsonProperty("normalizedHash")]
        public string NormalizedHash { get; internal set; }

        /// <summary>
        /// Cryptographically signed guarantee, to provide an audit record in cases of a dispute
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; internal set; }

        /// <summary>
        /// The state of a transaction as monitored by BitGo (you do not need to take any action on this)
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }

        /// <summary>
        /// if the signature is valid, you may accept the transaction instantly without the need for any block information. 
        /// You can save the guarantee &amp; signature locally to provide an audit record in case of a dispute.
        /// </summary>
        public bool IsValid()
        {
            return new BitcoinPubKeyAddress("1BitGo3gxRZ6mQSEH52dvCKSUgVCAH4Rja", Network.Main).VerifyMessage(Guarantee, Convert.ToBase64String(new NBitcoin.DataEncoders.HexEncoder().DecodeData(Signature)));
        }
    }
}
