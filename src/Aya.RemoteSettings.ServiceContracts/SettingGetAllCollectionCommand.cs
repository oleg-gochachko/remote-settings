namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetAllCollectionCommand : CommandBase
    {
        public string ClientId { get; set; }        

        public string Version { get; set; }
    }
}