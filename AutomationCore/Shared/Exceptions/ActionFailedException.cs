using System;
using System.Runtime.Serialization;

namespace AutomationCore.Shared.Exceptions
{
    public class ActionFailedException : Exception
    {
        public ActionFailedException(){}

        public ActionFailedException(string message) : base(message){}

        public ActionFailedException(string message, Exception innerException) : base(message, innerException){}

        protected ActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
