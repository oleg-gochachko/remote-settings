using System;
using System.Threading.Tasks;

namespace RemoteSettingsProvider.Controllers
{
    public static class CommandHandlerExtensions
    {
        public static Task<TCommandResult> ExecuteAsync<TCommand, TCommandResult>(this ICommandHandler<TCommand, TCommandResult> commandHandler, Action<TCommand> setupCommand = null) 
            where TCommand : class, ICommand, new() 
            where TCommandResult : class, ICommandResult, new()
        {
            var command = new TCommand();   
            setupCommand?.Invoke(command);            
            return commandHandler.ExecuteAsync(command);
        }   
    }
}