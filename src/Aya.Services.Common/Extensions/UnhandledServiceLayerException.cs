using System;

namespace RemoteSettingsProvider.Controllers
{
    public class UnhandledServiceLayerException : UnrecoverableServiceLayerException
    {
        public UnhandledServiceLayerException()
        {
        }

        public UnhandledServiceLayerException(string message) : base(message)
        {
        }

        public UnhandledServiceLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}