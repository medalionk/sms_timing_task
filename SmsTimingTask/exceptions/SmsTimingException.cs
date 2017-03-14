using System;
using System.Runtime.Serialization;

namespace SmsTimingTask.Exceptions
{
    /// <summary>
    /// This is base exception for other custom exceptions and 
    /// this exception extends ApplicationException.
    /// </summary>
    ///
    /// <threadsafety>
    /// This class is not thread safe.
    /// </threadsafety>
    ///
    /// <author>Bilal Abdullah</author>
    [Serializable]
    public class SmsTimingException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "SmsTimingException" /> class.
        /// </summary>
        public SmsTimingException()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "SmsTimingException" /> class
        /// with a specified error message.
        ///</summary>
        ///<param name = "message">
        ///  The message that describes the error.
        ///</param>
        public SmsTimingException(string message)
            : base(message)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "SmsTimingException" /> class
        /// with a specified error message and a reference to the inner 
        /// exception that is the cause of this exception.
        ///</summary>
        ///
        ///<param name = "message">
        /// The error message that explains the reason for the exception.
        ///</param>
        ///<param name = "innerException">
        /// The exception that is the cause of the current exception 
        /// or a null reference if no inner exception is specified.
        ///</param>
        public SmsTimingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "SmsTimingException" /> 
        /// class with serialized data.
        ///</summary>
        ///
        ///<param name = "info">
        /// The <see cref = "SerializationInfo" /> that holds the 
        /// serialized object data about the exception being thrown.
        ///</param>
        ///<param name = "context">
        /// The <see cref = "StreamingContext" /> that contains contextual 
        /// information about the source or destination.
        ///</param>
        protected SmsTimingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
