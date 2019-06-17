using System;
using System.Runtime.Serialization;

namespace RemoteSettingsProvider.Controllers
{
    public class UnrecoverableServiceLayerException : ServiceLayerException
    {
        public UnrecoverableServiceLayerException()
        {
        }

        protected UnrecoverableServiceLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnrecoverableServiceLayerException(string message) : base(message)
        {
        }

        public UnrecoverableServiceLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}