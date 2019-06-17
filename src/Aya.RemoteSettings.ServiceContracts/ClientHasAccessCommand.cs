using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public class ClientHasAccessCommand : CommandBase
    {
        public string ClientId { get; set; }

        public string RemoteAddress { get; set; }
    }
}