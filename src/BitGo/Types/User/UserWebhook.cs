using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class UserWebhook
    {
        [JsonProperty("type")]
        public string Type { get;  internal set; }

        [JsonProperty("coin")]
        public string Coin { get; internal set; }

        [JsonProperty("url")]
        public string Url { get;  internal set; }

    }
}