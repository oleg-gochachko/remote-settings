using System;
using Microsoft.Extensions.Logging;

namespace RemoteSettingsProvider.Controllers
{
    public static class ExceptionExtensions
    {
        public static TException LogError<TException>(this TException exception, ILogger logger, string message = null)
            where TException: Exception
        {
            message = $"Exception {typeof(TException)} occured.";
            logger.LogError(exception, message);
            return exception;
        }
    }
}