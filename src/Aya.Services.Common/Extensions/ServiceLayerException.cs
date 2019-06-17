using System;
using System.Runtime.Serialization;

namespace RemoteSettingsProvider.Controllers
{
    public class ServiceLayerException: Exception
    {
        public ServiceLayerException()
        {
        }

        protected ServiceLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ServiceLayerException(string message) : base(message)
        {
        }

        public ServiceLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}