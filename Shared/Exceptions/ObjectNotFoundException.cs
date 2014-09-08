using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Exceptions
{
    class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(){}

        public ObjectNotFoundException(string message) : base(message){}

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException){}

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context){}
    }
}
