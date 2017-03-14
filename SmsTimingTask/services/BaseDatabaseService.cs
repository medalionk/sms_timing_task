using FirebirdSql.Data.FirebirdClient;
using log4net;
using System;

namespace SmsTimingTask.Services
{
    /// <summary>
    /// This is the base class for database access.
    /// </summary>
    ///
    /// <threadsafety>
    /// This class is mutable and it is not thread safe.
    /// </threadsafety>
    ///
    /// <author>Bilal Abdullah</author>
    public abstract class BaseDatabaseService : IDisposable
    {
        #region Database Connection

        /// <summary>
        ///The database connection string.
        /// </summary>
        internal readonly string connectionString;

        /// <summary>
        /// The logger object.
        /// </summary>
        internal readonly ILog logger;

        /// <summary>
        /// The database connection object.
        /// </summary>
        internal FbConnection connection;

        /// <summary>
        /// Initializes a new instance of <see cref="BaseDatabaseService"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="logger">The logger object</param> 
        /// <exception cref="ArgumentNullException">
        /// If logger is null
        /// </exception>
        /// <exception cref="ConfigurationException">
        /// If connection string is empty or null
        /// </exception>
        public BaseDatabaseService(ILog logger, string connectionString)
        {
            Helper.CheckNotNull(logger, nameof(logger));
            this.logger = logger;
            this.connectionString = connectionString;
            CheckConfiguration();
        }

        /// <summary>
        /// Checks the configuration properties.
        /// </summary>
        ///
        /// <exception cref="ConfigurationException">
        /// If any of required fields have invalid values.
        /// </exception>
        public virtual void CheckConfiguration()
        {
            Helper.CheckConfiguration(connectionString, nameof(connectionString));
        }

        /// <summary>
        /// Creates the database connection.
        /// </summary>
        /// <returns>The created <see cref="FbConnection"/></returns>
        /// 
        /// <remarks>
        /// The internal exception may be thrown directly.
        /// </remarks>
        abstract public FbConnection CreateConnection();

        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
