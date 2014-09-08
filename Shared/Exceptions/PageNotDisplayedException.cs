using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
