using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class UserSessionResult {
        [JsonProperty("session", Required = Required.Always)]
        public UserSession Session { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class UserSessionUnlock {
        [JsonProperty("time", Required = Required.Default)]
        public DateTimeOffset Time { get; internal set; }

        [JsonProperty("expires", Required = Required.Default)]
        public DateTimeOffset Expires { get; internal set; }

        [JsonProperty("txCount", Required = Required.Default)]
        public int TransacitonCount { get; internal set; }

        [JsonProperty("txValue", Required = Required.Default)]
        public long TransacitonValue { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class UserSession {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("client", Required = Required.Always)]
        public string Client { get; internal set; }

        [JsonProperty("user", Required = Required.Default)]
        public string User { get; internal set; }

        [JsonProperty("scope", Required = Required.Default)]
        public string[] Scope { get; internal set; }

        [JsonProperty("created", Required = Required.Default)]
        public DateTimeOffset Created { get; internal set; }

        [JsonProperty("expires", Required = Required.Default)]
        public DateTimeOffset Expires { get; internal set; }
        
        [JsonProperty("origin", Required = Required.Default)]
        public string Origin { get; internal set; }

        [JsonProperty("unlock", Required = Required.Default)]
        public UserSessionUnlock Unlock { get; internal set; }

        [JsonProperty("ip", Required = Required.Default)]
        public string Ip { get; internal set; }

        [JsonProperty("isExtensible", Required = Required.Default)]
        public bool IsExtensible { get; internal set; }

        
    }
}