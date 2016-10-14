using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class WebhookNotification
    {
        [JsonProperty("type")]
        public string Type { get;  set; }

        [JsonProperty("walletId")]
        public string WalletId { get; set; }

        [JsonProperty("hash")]
        public string Hash { get;  set; }

        [JsonProperty("pendingApprovalId")]
        public string PendingApprovalId { get;  set; }

        [JsonProperty("state")]
        public string State { get;  set; }

    }
}