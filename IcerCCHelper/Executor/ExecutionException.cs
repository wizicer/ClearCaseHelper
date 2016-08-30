namespace IcerDesign.CCHelper
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ExecutionException : Exception
    {
        public ExecutionException()
        {
        }

        public ExecutionException(string message)
            : base(message)
        {
        }

        public ExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ExecutionException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}