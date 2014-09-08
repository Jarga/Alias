using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Exceptions
{
    class ElementNotRegisteredException : Exception
    {
        public ElementNotRegisteredException(){}

        public ElementNotRegisteredException(string message) : base(message){}

        protected ElementNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context){}

        public ElementNotRegisteredException(string message, Exception innerException) : base(message, innerException){}
    }
}
