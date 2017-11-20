using System;
using System.Runtime.Serialization;

namespace LambAndLentil.UI.Controllers
{
    [Serializable]
    internal class NotToBeImplementedException : Exception
    {
        public override string Message { get; } = "This operation is not to be implemented.";

        public NotToBeImplementedException()
        {
        }

        public NotToBeImplementedException(string message) : base(message)
        {
        }

        public NotToBeImplementedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotToBeImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}