using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteSettingsProvider.Controllers;

namespace RemoteSettingsProvider
{
    internal class ClientProvider : IClientProvider
    {
        private CachedJsonFileDataProvider<ClientStorageModel> JsonProvider { get; }

        public ClientProvider(CachedJsonFileDataProvider<ClientStorageModel> jsonProvider)
        {
            JsonProvider = jsonProvider;
        }

        public async Task<ICollection<ClientModel>> ProvideAsync()
        {
            var json = await JsonProvider.GetValueAsync();
            return json.ClientCollection;
        }
    }
}