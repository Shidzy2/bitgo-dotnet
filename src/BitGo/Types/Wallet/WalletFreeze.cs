using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletFreeze
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; internal set; }

        [JsonProperty("expires")]
        public DateTimeOffset Expires { get; internal set; }
    }
}
