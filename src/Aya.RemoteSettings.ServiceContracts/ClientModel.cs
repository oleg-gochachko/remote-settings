using Newtonsoft.Json;

namespace RemoteSettingsProvider
{
    public class ClientModel
    {
        [JsonProperty("clientId")]
        public string Id { get; set; }

        [JsonProperty("allowed-origins")]
        public string[] AllowedOriginCollection { get; set; }
    }
}