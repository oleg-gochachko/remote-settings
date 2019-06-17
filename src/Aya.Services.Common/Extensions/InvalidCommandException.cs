using System.Collections.Generic;
using System.Linq;

namespace RemoteSettingsProvider.Controllers
{
    public class InvalidCommandException: UnrecoverableServiceLayerException
    {
        public ICommand Command { get; }

        public ICollection<string> ValidationErrors { get; }

        public InvalidCommandException(ICommand command, IEnumerable<string> validationErrors)
        {
            Command = command;
            ValidationErrors = validationErrors.Safe().ToArray();
        }
    }
}