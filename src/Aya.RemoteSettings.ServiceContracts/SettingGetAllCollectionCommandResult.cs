using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetAllCollectionCommandResult: CommandResultBase
    {
        public SettingGetAllCollectionCommandResult()
        {
            SettingCollection = new List<SettingModel>();
        }

        [JsonProperty("settings")]
        public ICollection<SettingModel> SettingCollection { get; set; }
    }
}