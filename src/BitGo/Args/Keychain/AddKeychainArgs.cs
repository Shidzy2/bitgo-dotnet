using System;
using Newtonsoft.Json;

namespace BitGo.Args.Keychain {
    
    [JsonObject(MemberSerialization.OptIn)]
    internal class AddKeychainArgs {

        [JsonProperty("xpub", Required = Required.Always)]
        public string ExtendedPublicKey { get; internal set; }

        [JsonProperty("encryptedXprv", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptedExtendedPrivateKey { get; internal set; }

    }
}