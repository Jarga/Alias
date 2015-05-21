using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Exceptions
{
    public class ActionFailedException : Exception
    {
        public ActionFailedException(){}

        public ActionFailedException(string message) : base(message){}

        public ActionFailedException(string message, Exception innerException) : base(message, innerException){}

        protected ActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
