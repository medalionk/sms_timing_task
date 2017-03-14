using System;
using System.Runtime.Serialization;

namespace SmsTimingTask.Exceptions
{
    /// <summary>
    /// This exception is thrown if there is error writing to CSV file
    /// This exception extends SmsTimingException.
    /// </summary>
    ///
    /// <threadsafety>
    /// This class isn't thread safe.
    /// </threadsafety>
    ///
    /// <author>Bilal Abdullah</author>
    [Serializable]
    class CsvWriteException : SmsTimingException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "CsvWriteException" /> class.
        /// </summary>
        public CsvWriteException()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "CsvWriteException" /> class
        /// with a specified error message.
        ///</summary>
        ///
        ///<param name = "message">
        ///  The message that describes the error.
        ///</param>
        public CsvWriteException(string message)
            : base(message)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "CsvWriteException" /> class
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
        public CsvWriteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref = "CsvWriteException" /> 
        /// class with serialized data.
        ///</summary>
        ///
        ///<param name = "info">
        /// The <see cref = "SerializationInfo" /> that holds the serialized 
        /// object data about the exception being thrown.
        ///</param>
        ///<param name = "context">
        /// The <see cref = "StreamingContext" /> that contains contextual 
        /// information about the source or destination.
        ///</param>
        protected CsvWriteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

