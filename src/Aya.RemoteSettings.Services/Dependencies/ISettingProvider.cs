using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteSettingsProvider.Controllers
{
    public interface ISettingProvider
    {
        Task<ICollection<SettingModel>> ProvideAsync();
    }
}