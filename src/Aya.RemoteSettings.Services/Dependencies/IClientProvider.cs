using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteSettingsProvider.Controllers
{
    public interface IClientProvider
    {
        Task<ICollection<ClientModel>> ProvideAsync();
    }
}