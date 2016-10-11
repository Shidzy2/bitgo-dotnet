using System;
using Newtonsoft.Json;

namespace BitGo.Types {
    
    [JsonObject(MemberSerialization.OptIn)]
    public class UserResult {
        [JsonProperty("access_token", Required = Required.Default)]
        public string AccessToken { get; internal set; }

        [JsonProperty("expires_in", Required = Required.Default)]
        public int ExpiresIn { get; internal set; }

        [JsonProperty("token_type", Required = Required.Default)]
        public string TokenType { get; internal set; }

        [JsonProperty("user", Required = Required.Always)]
        public User User { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class UserDetails {
        [JsonProperty("first", Required = Required.Default)]
        public string First { get; internal set; }

        [JsonProperty("last", Required = Required.Default)]
        public string Last { get; internal set; }

        [JsonProperty("full", Required = Required.Default)]
        public string Full { get; internal set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class User {

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        [JsonProperty("isActive", Required = Required.Default)]
        public bool IsActive { get; internal set; }

        [JsonProperty("name", Required = Required.Default)]
        public UserDetails Name { get; internal set; }

        [JsonProperty("username", Required = Required.Default)]
        public string Username { get; internal set; }

    }
}