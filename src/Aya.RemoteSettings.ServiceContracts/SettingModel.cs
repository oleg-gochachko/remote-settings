using System;
using Newtonsoft.Json;

namespace RemoteSettingsProvider
{
    public class SettingModel
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("version", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set;}

        [JsonProperty("clientId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientId { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("actualFrom", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        //[JsonConverter(typeof(Startup.CustomDateTimeConverter))]
        public DateTime? ActualFrom { get; set; }

        [JsonProperty("actualTo", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]             
        public DateTime? ActualTo { get; set; }

        public bool IsNameEquals(string name)
        {
            return String.Equals(Name, name, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsVersionEquals(string version)
        {
            return String.Equals(Version, version, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsClientIdEquals(string clientId)
        {
            return String.Equals(ClientId, clientId, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}