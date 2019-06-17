using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteSettingsProvider.Controllers;

namespace RemoteSettingsProvider
{
    internal class SettingProvider : ISettingProvider
    {
        private CachedJsonFileDataProvider<SettingStorageModel> JsonProvider { get; }

        public SettingProvider(CachedJsonFileDataProvider<SettingStorageModel> jsonProvider)
        {
            JsonProvider = jsonProvider;
        }

        public async Task<ICollection<SettingModel>> ProvideAsync()
        {
            var json = await JsonProvider.GetValueAsync();
            return json.SettingCollection;
        }
    }
}