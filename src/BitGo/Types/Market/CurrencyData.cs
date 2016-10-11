using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGo.Types
{
    public class CurrencyData
    {
        [JsonProperty("24h_avg")]
        public decimal Average24 { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("last")]
        public decimal Last { get; set; }

        [JsonProperty("lastHourHigh")]
        public decimal LastHourHigh { get; set; }

        [JsonProperty("lastHourLow")]
        public decimal LastHourLow { get; set; }

        [JsonProperty("monthlyHigh")]
        public decimal MonthlyHigh { get; set; }

        [JsonProperty("monthlyLow")]
        public decimal MonthlyLow { get; set; }

        [JsonProperty("prevDayHigh")]
        public decimal PreviousDayHigh { get; set; }

        [JsonProperty("prevDayLow")]
        public decimal PreviousDayLow { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("total_vol")]
        public decimal TotalVolume { get; set; }
    }
}
