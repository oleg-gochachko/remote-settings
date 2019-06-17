using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public class SettingGetByNameCommandHandler : CommandHandlerBase<SettingGetByNameCommand,
        SettingGetByNameCommandResult, SettingGetByNameCommandHandler>
    {
        private ISettingProvider SettingProvider { get; }

        public SettingGetByNameCommandHandler(ISettingProvider settingProvider, ILogger<SettingGetByNameCommandHandler> logger) : base(logger)
        {
            SettingProvider = settingProvider;
        }

        protected override async Task<SettingGetByNameCommandResult> ExecuteBodyAsync(SettingGetByNameCommand command)
        {
            var commandResult = CreateCommandResult(command);
            var settingCollection = await SettingProvider.ProvideAsync();
            

            var nameScope = settingCollection.Where(x => x.IsNameEquals(command.SettingName) && IsActualDate(x)).OrderBy(ActualDateOrder).ToArray();

            var value =
                nameScope.FirstOrDefault(x => x.IsVersionEquals(command.Version) && x.IsClientIdEquals(command.ClientId)) ??
                nameScope.FirstOrDefault(x => x.IsVersionEquals(command.Version) && x.ClientId == null) ??
                nameScope.FirstOrDefault(x => x.Version == null && x.IsClientIdEquals(command.ClientId)) ??
                nameScope.FirstOrDefault(x => x.Version == null && x.ClientId == null);

            commandResult.Setting = value;
            return commandResult;
        }

        private static bool IsActualDate(SettingModel setting)
        {
            var now = DateTime.Now;

            var result = (setting.ActualFrom == null || setting.ActualFrom <= now)
                         && (setting.ActualTo == null || setting.ActualTo >= now);
            return result;
        }

        private static int ActualDateOrder(SettingModel setting)
        {
            var now = DateTime.Now;
            if (setting.ActualFrom <= now && setting.ActualTo >= now) return 0;
            if (setting.ActualFrom == null && setting.ActualTo >= now) return 1;
            if (setting.ActualFrom <= now && setting.ActualTo == null) return 2;
            if (setting.ActualFrom == null && setting.ActualTo == null) return 3;            
            return 4; 
        }
    }
}