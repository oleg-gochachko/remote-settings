using System;
using System.Runtime.Serialization;

namespace RemoteSettingsProvider.Controllers
{
    public class RecoverableServiceLayerException : ServiceLayerException
    {
        public RecoverableServiceLayerException()
        {
        }

        protected RecoverableServiceLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RecoverableServiceLayerException(string message) : base(message)
        {
        }

        public RecoverableServiceLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}