using System;
using Newtonsoft.Json;

namespace BitGo.Args {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class SendTransactionArgs {

        [JsonProperty("hex", Required = Required.Always)]
        public string Hex { get; internal set; }

        [JsonProperty("sequenceId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SequenceId { get; internal set; }

        [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; internal set; }

        [JsonProperty("instant", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Instant { get; internal set; }

        [JsonProperty("otp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Otp { get; internal set; }

        

    }
}