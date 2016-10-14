using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletWebhook
    {
        [JsonProperty("walletId")]
        public string WalletId { get; internal set; }

        [JsonProperty("type")]
        public string Type { get;  internal set; }

        [JsonProperty("url")]
        public string Url { get;  internal set; }

        [JsonProperty("numConfirmations")]
        public int? NumConfirmations { get;  internal set; }
    }
}