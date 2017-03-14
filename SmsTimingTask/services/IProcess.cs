namespace SmsTimingTask.Services
{
    /// <summary>
    /// Base interface for all data processing logic.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    interface IProcess
    {
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
        string Summary(string field);

        /// <summary>
        /// Perform Clustering of customers(Use K-Means)
        /// </summary>
        /// <param name="fields">Clustering fields</param>
        /// <returns>summary csv filename</returns>
        string Cluster(string[] fields);

        /// <summary>
        /// Regular update of clusters 
        /// </summary>
        void UpdateCluster();
    }
}
