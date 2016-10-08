using System;
using Newtonsoft.Json;

namespace BitGo.Types {

    public abstract class PagedResult {

        [JsonProperty("start", Required = Required.Default)]
        public int Start { get; internal set; }

        [JsonProperty("count", Required = Required.Default)]
        public int Count { get; internal set; }

        [JsonProperty("total", Required = Required.Default)]
        public int Total { get; internal set; }

        [JsonProperty("limit", Required = Required.Default)]
        public int Limit { get; internal set; }
    }
}