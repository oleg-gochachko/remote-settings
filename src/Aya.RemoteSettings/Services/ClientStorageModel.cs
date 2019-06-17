using Newtonsoft.Json;

namespace RemoteSettingsProvider
{
    public class ClientStorageModel
    {
        public ClientStorageModel()
        {
        }

        [JsonProperty("clients")]        
        public ClientModel[] ClientCollection { get; set; }
    }
}