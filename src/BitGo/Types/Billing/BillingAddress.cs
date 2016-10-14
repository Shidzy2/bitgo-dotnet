using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    internal class BillingAddress
    {
        [JsonProperty("address")]
        public string Address { get; internal set; }
    }
}
