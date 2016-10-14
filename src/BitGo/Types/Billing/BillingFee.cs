using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BitGo.Types
{
    internal class BillingFee
    {
        [JsonProperty("fee")]
        public long Fee { get; internal set; }
    }
}
