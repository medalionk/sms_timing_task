using log4net;

namespace SmsTimingTask.Services.Impl
{
    /// <summary>
    /// The base class for all data processing services.
    /// This class implements the IProcess interface
    /// </summary>
    ///
    /// <threadsafety>
    /// This class is mutable and it is not thread safe.
    /// </threadsafety>
    /// 
    /// <author>Bilal Abdullah</author>
    public abstract class BaseProcess : IProcess
    {
        /// <summary>
        /// The logger object.
        /// </summary>
        internal readonly ILog logger;

        /// <summary>
        ///The database connection string.
        /// </summary>
        internal readonly string connectionString;

        /// <summary>
        ///The database connection string.
        /// </summary>
        internal readonly string outputPath;

        /// <summary>
        /// Initializes a new instance of the <see cref = "BaseProcess" /> class.
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="outputPath">The output folder path</param>
        /// <param name="logger">The ILog logger object</param>
        public BaseProcess(ILog logger, string connectionString, string outputPath)
        {
            Helper.CheckNotNull(logger, nameof(logger));
            Helper.CheckConfiguration(connectionString, nameof(connectionString));
            this.logger = logger;
            this.connectionString = connectionString;           
            this.outputPath = outputPath;
        }

        /// <summary>
        /// Get the summary of all Number fields grouped by requested field
        /// </summary>
        /// <param name="field">The requested field</param>
        /// <returns>summary csv filename</returns>
        /// 
        /// <exception cref="ArgumentException">
        /// If <paramref name="field"/> <c>is not a valid column name</c>.
        /// </exception>
        /// <exception cref="DataAccessException">
        /// If error accessing database
        /// </exception>
        public abstract string Summary(string field);

        /// <summary>
        /// Perform Clustering of customers(Use K-Means)
        /// </summary>
        /// <param name="fields">Clustering fields</param>
        /// <returns>summary csv filename</returns>
        public abstract string Cluster(string[] fields);

        /// <summary>
        /// Regular update of clusters 
        /// </summary>
        public abstract void UpdateCluster();
    }
}
