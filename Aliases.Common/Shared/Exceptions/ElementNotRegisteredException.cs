using System;
using System.Runtime.Serialization;

namespace Aliases.Common.Shared.Exceptions
{
    [Serializable]
    public class ElementNotRegisteredException : Exception
    {
        public ElementNotRegisteredException(){}

        public ElementNotRegisteredException(string message) : base(message){}

        protected ElementNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context){}

        public ElementNotRegisteredException(string message, Exception innerException) : base(message, innerException){}
    }
}
