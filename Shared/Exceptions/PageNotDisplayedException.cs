using System;
using System.Runtime.Serialization;

namespace TestAutomation.Shared.Exceptions
{
    class PageNotDisplayedException : Exception
    {
        public PageNotDisplayedException(){}

        public PageNotDisplayedException(string message) : base(message){}

        public PageNotDisplayedException(string message, Exception innerException) : base(message, innerException){}

        protected PageNotDisplayedException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
