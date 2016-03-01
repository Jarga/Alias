using System;
using System.Runtime.Serialization;

namespace Aliases.Common.Shared.Exceptions
{
    [Serializable]
    public class PageValidationException : Exception
    {
        public PageValidationException() { }

        public PageValidationException(string message) : base(message) { }

        public PageValidationException(string message, Exception innerException) : base(message, innerException) { }

        protected PageValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
