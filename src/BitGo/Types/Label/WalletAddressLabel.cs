using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class WalletAddressLabel
    {
        [JsonProperty("walletId")]
        public string WalletId { get; internal set; }

        [JsonProperty("address")]
        public string Address { get; internal set; }

        [JsonProperty("label")]
        public string Label { get; internal set; }
    }
}
