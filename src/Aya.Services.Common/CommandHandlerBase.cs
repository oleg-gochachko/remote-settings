using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public abstract class CommandHandlerBase<TCommand, TCommandResult, THandler>: ICommandHandler<TCommand, TCommandResult>
        where TCommand: class, ICommand, new()
        where TCommandResult: class, ICommandResult, new()
        where THandler: CommandHandlerBase<TCommand, TCommandResult, THandler>
    {
        protected ILogger<THandler> Logger { get; }

        protected CommandHandlerBase(ILogger<THandler> logger)
        {
            Logger = logger;
            // this should be injected for better testability
            //Logger = LoggingUtil.CreateLogger(GetType());
        }

        public virtual async Task<TCommandResult> ExecuteAsync(TCommand command)
        {
            Logger.LogDebug($"CommandHandler {GetType()} ExecuteAsync() started.");
            try
            {
                await EnsureCommandIsValidAsync(command);
                return await ExecuteBodyAsync(command);
            }
            catch (ServiceLayerException)
            {
                throw;
            }
            catch (Exception e)
            {                       
                throw new UnhandledServiceLayerException("", e)
                    .LogError(Logger);
            }
            finally
            {
                Logger.LogDebug($"CommandHandler {GetType()} ExecuteAsync() completed.");
            }
        }

        protected virtual async Task<ICollection<string>> ValidateCommandAsync(TCommand command)
        {
            await Task.CompletedTask;
            return new List<string>();
        }

        protected async Task EnsureCommandIsValidAsync(TCommand command)
        {
            var validationErrors = await ValidateCommandAsync(command);            
            if (validationErrors?.Count > 0)
            {                
                throw new InvalidCommandException(command, validationErrors)
                    .LogError(Logger);
            }
        }

        protected abstract Task<TCommandResult> ExecuteBodyAsync(TCommand command);

        protected virtual TCommandResult CreateCommandResult(TCommand command)
        {
            return new TCommandResult();
        }
    }
}