using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RemoteSettingsProvider.Controllers
{
    public static class CommandHandlerExtensions
    {
        public static IServiceCollection AddCommandHandler<TCommand, TCommandResult, TCommandHandler>(this IServiceCollection serviceCollection)
            where TCommand : class, ICommand, new()
            where TCommandResult : class, ICommandResult, new()
            where TCommandHandler: class, ICommandHandler<TCommand, TCommandResult>
        {
            return serviceCollection.AddScoped<ICommandHandler<TCommand, TCommandResult>, TCommandHandler>();
        }        
    }
}