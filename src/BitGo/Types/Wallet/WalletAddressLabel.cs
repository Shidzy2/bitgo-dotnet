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
        [JsonProperty("walletId", NullValueHandling = NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
