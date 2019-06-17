using System.Threading.Tasks;

namespace RemoteSettingsProvider.Controllers
{
    public interface ICommandHandler<in TCommand, TCommandResult>
        where TCommand : class, ICommand, new()
        where TCommandResult : class, ICommandResult, new()
    {
        Task<TCommandResult> ExecuteAsync(TCommand command);
    }
}