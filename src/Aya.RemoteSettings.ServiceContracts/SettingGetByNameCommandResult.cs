using Newtonsoft.Json;

namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetByNameCommandResult: CommandResultBase
    {
        [JsonProperty("setting")]
        public SettingModel Setting { get; set; }
    }
}