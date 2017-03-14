using FirebirdSql.Data.FirebirdClient;
using log4net;
using SmsTimingTask.Exceptions;
using System;

namespace SmsTimingTask.Services
{
    /// <summary>
    /// This class implements the Firebird database connection services.
    /// This class extends BaseDatabaseService.
    /// </summary>
    ///
    /// <threadsafety>
    /// This class is mutable and it is not thread safe.
    /// </threadsafety>
    ///
    /// <author>Bilal Abdullah</author>
    class FirebirdDatabaseService : BaseDatabaseService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FirebirdDatabaseService"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="logger">The logger object</param> 
        /// <exception cref="ArgumentNullException">
        /// If logger is null
        /// </exception>
        /// <exception cref="ConfigurationException">
        /// If connection string is empty or null
        /// </exception>
        public FirebirdDatabaseService(ILog logger, string connectionString) 
            : base(logger, connectionString) { }

        /// <summary>
        /// Create a connection to the Firebird database
        /// </summary>
        /// <returns>The created <see cref="FbConnection"/></returns>
        /// 
        /// <exception cref="ConnectionException">
        /// If a connection to the database could not be established.
        /// </exception>
        public override FbConnection CreateConnection()
        {
            logger.LoggingWrapper(() =>
            {
                // Build the connection string
                var connectionString = base.connectionString +
                "Dialect=3;" +
                "Charset=NONE;" +
                "Role=;" +
                "Connection lifetime=15;" +
                "Pooling=true;" +
                "MinPoolSize=0;" +
                "MaxPoolSize=50;" +
                "Packet Size=8192;" +
                "ServerType=0";

                try
                {
                    connection = new FbConnection(connectionString);
                    connection.Open();
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    throw new ConnectionException(ex.Message, ex.InnerException);
                }
            });

            return connection;
        }
    }
}
