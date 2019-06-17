namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetByNameCommand : CommandBase
    {
        public string ClientId { get; set; }

        public string SettingName { get; set; }

        public string Version { get; set; }
    }
}