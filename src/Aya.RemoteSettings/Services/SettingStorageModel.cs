using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace RemoteSettingsProvider
{
    public class SettingStorageModel
    {   
        [JsonProperty("settings")]
        public ICollection<SettingModel> SettingCollection { get; set; }
    }
}