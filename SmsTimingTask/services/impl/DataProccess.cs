using FirebirdSql.Data.FirebirdClient;
using log4net;
using SmsTimingTask.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmsTimingTask.Services.Impl
{
    /// <summary>
    /// The main class for data processing.
    /// This class extends the BaseProcess class
    /// </summary>
    ///
    /// <threadsafety>
    /// This class is mutable and it is not thread safe.
    /// </threadsafety>
    /// 
    /// <author>Bilal Abdullah</author>
    public class DataProccess : BaseProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "DataProccess" /> class.
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="outputPath">The output folder path</param>
        /// <param name="logger">The ILog logger object</param>
        public DataProccess(ILog logger, string connectionString, string outputPath = null) 
            : base(logger, connectionString, outputPath) { }

        /// <summary>
        /// Check if field is a valid column name
        /// </summary>
        /// <param name="field">Field name to check</param>
        /// 
        /// <exception cref="ArgumentException">
        /// If <paramref name="field"/> <c>is not a valid column name</c>.
        /// </exception>
        private void CheckField(ref string field)
        {
            string normalizedColName = string.Empty;
            string fieldCopy = field;

            logger.LoggingWrapper(() =>
            {

                Helper.CheckNotNullOrEmpty(fieldCopy, nameof(field));
                normalizedColName = fieldCopy.StartsWith("F_CU_") ? fieldCopy : "F_CU_" + fieldCopy;

                bool isValidColumn = Helper.Columns.ContainsKey(normalizedColName);

                if (!isValidColumn)
                {
                    throw new ArgumentException(
                        string.Format("The field '{0}' is not a valid column name.", fieldCopy),
                        nameof(field));
                }
            }, field);

            field = normalizedColName;
        }

        /// <summary>
        /// Get the summary list Number fields grouped by field
        /// </summary>
        /// <param name="field">Requested field</param>
        /// <returns>The summary list</returns>
        /// 
        /// <exception cref="DataAccessException">
        /// If error accessing database
        /// </exception>
        private IList<IList<string>> GetSummary(string field)
        {
            // Summary to return
            var summary = new List<IList<string>>();

            logger.LoggingWrapper(() =>
            {
                // Get columns names that are numbers except field
                var columns = Helper.Columns
                        .Where(a => a.Value.Equals(true) && !a.Key.Equals(field))
                                                 .Select(dict => dict.Key)
                                                 .ToList();

                summary.Add(columns);

                // Build the summary clause
                columns = columns.Select(s => "SUM(" + s + "),").ToList();
                string commandStr = string.Join(" ", columns);
                commandStr = commandStr.Remove(commandStr.Length - 1);
                commandStr = "SELECT " + commandStr + " FROM T_CUSTOMER GROUP BY " + field;

                using (var service = new FirebirdDatabaseService(logger, connectionString))
                {
                    try
                    {
                        var fbConnection = service.CreateConnection();
                        var fbCommand = new FbCommand(commandStr, fbConnection);
                        var reader = fbCommand.ExecuteReader();

                        // Get all number field summary values
                        while (reader.Read())
                        {
                            var row = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader[i].ToString());
                            }

                            summary.Add(row);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new DataAccessException("FieldSummary", ex);
                    }
                }
            }, field);
            
            return summary;
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
        public override string Summary(string field)
        {
            string filename = string.Empty;
            
            logger.LoggingWrapper(()=>
            {
                try
                {
                    filename = Helper.GenerateFileName("Task1", "csv");
                    if (!string.IsNullOrWhiteSpace(outputPath))
                    {
                        Helper.createDirectory(outputPath);
                        filename = Path.Combine(outputPath, filename);
                    }

                    CheckField(ref field);
                    var summary = GetSummary(field);
                    Helper.WriteCsv(summary, filename, logger);
                }
                catch (ArgumentException)
                {                    
                    throw;
                }
            }, field, connectionString);

            return filename;
        }

        /// <summary>
        /// Perform Clustering of customers(Use K-Means)
        /// </summary>
        /// <param name="fields">Clustering fields</param>
        /// <returns>summary csv filename</returns>
        public override string Cluster(string[] fields)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Regular update of clusters 
        /// </summary>
        public override void UpdateCluster()
        {
            throw new NotImplementedException();
        }
    }
}
