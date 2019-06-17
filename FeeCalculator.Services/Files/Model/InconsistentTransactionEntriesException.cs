using System;
using System.Runtime.Serialization;

namespace FeeCalculator.Services.Files
{
    [Serializable]
    internal class InconsistentTransactionEntriesException : Exception
    {
        public string LineData { get; private set; }

        public InconsistentTransactionEntriesException()
        {
        }

        public InconsistentTransactionEntriesException(string message) : base(message)
        {
        }

        public InconsistentTransactionEntriesException(string message, string lineData) : this(message)
        {
            LineData = lineData;
        }

        public InconsistentTransactionEntriesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InconsistentTransactionEntriesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}