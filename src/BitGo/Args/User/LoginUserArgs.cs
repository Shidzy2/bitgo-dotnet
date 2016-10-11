using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class LoginUserArgs {

        [JsonProperty("email", Required = Required.Always)]
        public string Email { get; internal set; }

        [JsonProperty("password", Required = Required.Always)]
        public string Password { get; internal set; }

        [JsonProperty("otp", Required = Required.Always)]
        public string Otp { get; internal set; }

        [JsonProperty("extensible", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Extensible { get; internal set; }

    }
}