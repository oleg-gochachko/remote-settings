using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetAllCollectionCommandHandler : CommandHandlerBase<SettingGetAllCollectionCommand,
        SettingGetAllCollectionCommandResult, SettingGetAllCollectionCommandHandler>
    {
        private ISettingProvider SettingProvider { get; }
        private ICommandHandler<SettingGetByNameCommand, SettingGetByNameCommandResult> GetByName { get; }

        public SettingGetAllCollectionCommandHandler(ISettingProvider settingProvider, ICommandHandler<SettingGetByNameCommand, SettingGetByNameCommandResult> getByName, ILogger<SettingGetAllCollectionCommandHandler> logger) : base(logger)
        {
            SettingProvider = settingProvider;
            GetByName = getByName;
        }

        protected override async Task<SettingGetAllCollectionCommandResult> ExecuteBodyAsync(SettingGetAllCollectionCommand command)
        {
            var commandResult = CreateCommandResult(command);

            var settingCollection = await SettingProvider.ProvideAsync();

            var nameCollection = settingCollection.Select(x => x.Name).Distinct().ToArray();

            foreach (var name in nameCollection)
            {
                var settingModel = await GetSettingAsync(name, command.ClientId, command.Version);                
                commandResult.SettingCollection.Add(settingModel);
            }
            

            return commandResult;
        }

        private async Task<SettingModel> GetSettingAsync(string settingName, string clientId, string version)
        {
            var result = await GetByName.ExecuteAsync(command =>
            {
                command.ClientId = clientId;
                command.SettingName = settingName;  
                command.Version = version;
            });
            return result.Setting;
        }        
    }
}