using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Exceptions
{
    public class ActionTimeoutException : Exception
    {
        public ActionTimeoutException(){}

        public ActionTimeoutException(string message, Exception innerException) : base(message, innerException){}

        public ActionTimeoutException(string message) : base(message){}

        protected ActionTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
