using System;
using System.Runtime.Serialization;

namespace SupplementStore.Infrastructure {

    [Serializable]
    public class MissingEntityException : Exception {

        public MissingEntityException() {
        }

        public MissingEntityException(string message) : base(message) {
        }

        public MissingEntityException(string message, Exception innerException) : base(message, innerException) {
        }

        protected MissingEntityException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}