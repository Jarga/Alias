using System;
using System.Runtime.Serialization;

namespace Automation.Common.Shared.Exceptions
{
    [Serializable]
    public class ActionTimeoutException : Exception
    {
        public ActionTimeoutException(){}

        public ActionTimeoutException(string message, Exception innerException) : base(message, innerException){}

        public ActionTimeoutException(string message) : base(message){}

        protected ActionTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
