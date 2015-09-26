using System;
using System.Runtime.Serialization;

namespace Automation.Common.Shared.Exceptions
{
    [Serializable]
    public class InvalidSearchPropertyException : Exception
    {
        public InvalidSearchPropertyException(){}

        public InvalidSearchPropertyException(string message) : base(message){}

        protected InvalidSearchPropertyException(SerializationInfo info, StreamingContext context) : base(info, context){}

        public InvalidSearchPropertyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
