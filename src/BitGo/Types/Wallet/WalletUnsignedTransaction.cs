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
    public class WalletUnsignedTransactionUnspent
    {
        [JsonProperty("chainPath")]
        public string ChainPath { get; set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("transactionOutputIndex")]
        public int TransactionOutputIndex { get; set; }

        [JsonProperty("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletUnsignedTransactionAddress
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletUnsignedTransactionKeychain
    {
        [JsonProperty("xpub")]
        public string ExtendedPublicKey { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class WalletUnsignedTransaction
    {
        [JsonProperty("walletId")]
        public string WalletId { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("transactionHex")]
        public string TransactionHex { get; set; }

        [JsonProperty("changeAddress")]
        public WalletUnsignedTransactionAddress ChangeAddress { get; set; }

        [JsonProperty("walletKeychains")]
        public WalletUnsignedTransactionKeychain[] WalletKeychains { get; set; }

        [JsonProperty("unspents")]
        public WalletUnsignedTransactionUnspent[] Unspents { get; set; }

    }
}