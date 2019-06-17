using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public class ClientHasAccessCommandHandler : CommandHandlerBase<ClientHasAccessCommand, ClientHasAccessCommandResult
        , ClientHasAccessCommandHandler>
    {
        private IClientProvider ClientProvider { get; }        

        public ClientHasAccessCommandHandler(IClientProvider clientProvider, ILogger<ClientHasAccessCommandHandler> logger) : base(logger)
        {
            ClientProvider = clientProvider;            
        }

        protected override async Task<ClientHasAccessCommandResult> ExecuteBodyAsync(ClientHasAccessCommand command)
        {
            var commandResult = CreateCommandResult(command);

            var clientCollection = await ClientProvider.ProvideAsync();

            var client = clientCollection.FirstOrDefault(c => c.Id == command.ClientId);
            if (client != null)
            {
                // full access check :)
                if (client.AllowedOriginCollection.Contains("*"))
                {
                    commandResult.HasAccess = true;                    
                }
                else
                    // per IP check
                if (client.AllowedOriginCollection.Contains(command.RemoteAddress))
                {
                    commandResult.HasAccess = true;                    
                }
            }
            else
            {
                Logger.LogWarning(
                    $"Client: \"{command.ClientId}\" has no access from following origin: \"{command.RemoteAddress}\"");
                commandResult.HasAccess = false;
            }

            return commandResult;
        }
    }
}