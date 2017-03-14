using CsvHelper;
using log4net;
using Newtonsoft.Json;
using SmsTimingTask.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmsTimingTask
{
    /// <summary>
    /// This class is the helper class for this project.
    /// </summary>
    ///
    /// <author>
    /// Bilal Abdullah
    /// </author>
    internal static class Helper
    {

        #region Parameters
        /// <summary>
        /// The column names of the T_CUSTOMER table and
        /// flag to indicate if the field is numeric
        /// </summary>
        private static readonly Dictionary<string, bool> columns 
            = new Dictionary<string, bool>()
        {
            {"F_CU_FIRSTNAME", false },
            {"F_CU_LASTNAME", false },
            {"F_CU_EMAIL", false },
            {"F_CU_MOBILE", false },
            {"F_CU_ACCEPT_EMAIL", false },
            {"F_CU_ACCEPT_PHONE", false },
            {"F_CU_ACCEPT_FACEBOOK", false },
            {"F_CU_AGE", true },
            {"F_CU_GENDER", false },
            {"F_CU_ZIP", false },
            {"F_CU_COUNTRY", false },
            {"F_CU_CITY", false },
            {"F_CU_CREATED", false },
            {"F_CU_BIRTHDATE", false },
            {"F_CU_PRODUCT_GROUP", false },
            {"F_CU_RACES", true },
            {"F_CU_MONEYSPENT", true },
            {"F_CU_LAST_VISIT", false },
            {"F_CU_LAST_VISIT_MONTHS_AGO", true },
            {"F_CU_FIRST_VISIT_MONTHS_AGO", true },
            {"F_CU_CREATED_YEAR", true },
            {"F_CU_CREATED_QUARTER", false },
            {"F_CU_CREATED_MONTH", false },
            {"F_CU_CREATED_WEEKDAY", true },
            {"F_CU_HAS_EMAIL", false },
            {"F_CU_HAS_PHONE", false },
            {"F_CU_HAS_FACEBOOK", false },
            {"F_CU_DAYS_RACED", true },
            {"F_CU_CREATED_WEEK", true },
            {"F_CU_DISTANCE", true },
            {"F_CU_CUSTOMER_ID", false },
            {"F_CU_CREATED_TIME_OF_DAY", false },
            {"F_CU_FRIENDS_COUNT", true }
        };

        /// <summary>
        /// Columns accessor
        /// </summary>
        public static Dictionary<string, bool> Columns
        {
            get { return columns; }
        }

        #endregion

        /// <summary>
        /// Create a new directory with given path
        /// do nothing if directory is already existing
        /// </summary>
        /// <param name="path">directory path</param>
        public static void createDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Generate a unique filename
        /// </summary>
        /// <param name="context">The filename context</param>
        /// <param name="extension">The file extension</param>
        /// <returns>The new file name</returns>
        public static string GenerateFileName(string context, string extension)
        {
            return context + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") 
                + "." + extension;          
        }

        #region CSV Writer

        /// <summary>
        /// Write text to CSV file
        /// </summary>
        /// 
        /// <param name="data">Data to write to file</param>
        /// <param name="filename">The csv filename</param>
        /// <param name="logger">The logging object</param>
        /// 
        /// <exception cref="CsvWriteException">
        /// If there error writing data to file
        /// </exception>
        public static void WriteCsv(IList<IList<string>> data, string filename, ILog logger)
        {
            logger.LoggingWrapper(() => {
                try
                {
                    using (var writer = File.CreateText(filename))
                    {
                        if (data == null || data.Count == 0)
                        {
                            return;
                        }

                        using (var csv = new CsvWriter(writer))
                        {
                            int length = data.ElementAt(0).Count;
                            for (int i = 0; i < length; i++)
                            {
                                for (int j = 0; j < data.Count; j++)
                                {
                                    var value = data.ElementAt(j).ElementAt(i);
                                    csv.WriteField(value);
                                }
                                csv.NextRecord();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new CsvWriteException("WriteCsv", ex);
                }
            }, data, filename);
        }

        #endregion

        #region Check 
        /// <summary>
        /// <para>
        /// Checks whether the given object is null.
        /// </para>
        /// </summary>
        ///
        /// <param name="value">
        /// The object to check.
        /// </param>
        /// <param name="parameterName">
        /// The actual parameter name of the argument being checked.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If object is null.
        /// </exception>
        internal static void CheckNotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                    parameterName, string.Format("The parameter '{0}' cannot be null.", parameterName));
            }
        }

        /// <summary>
        /// <para>
        /// Checks whether the given string is null or empty.
        /// </para>
        /// </summary>
        ///
        /// <param name="value">
        /// The object to check.
        /// </param>
        /// <param name="parameterName">
        /// The actual parameter name of the argument being checked.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If the string is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the given string is empty string.
        /// </exception>
        internal static void CheckNotNullOrEmpty(string value, string parameterName)
        {
            CheckNotNull(value, parameterName);
            if (value.Trim().Length == 0)
            {
                throw new ArgumentException(
                    string.Format("The parameter '{0}' cannot be empty.", parameterName), parameterName);
            }
        }

        /// <summary>
        /// <para>
        /// Checks whether the given configuration value is null or empty.
        /// </para>
        /// </summary>
        /// <param name="value">
        /// The configuration value to check.
        /// </param>
        /// <param name="name">
        /// The actual property name.
        /// </param>
        /// 
        /// <exception cref="ConfigurationException">
        /// If value is null or empty string.
        /// </exception>
        internal static void CheckConfiguration(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationException(string.Format(
                   "Instance property '{0}' wasn't configured properly.", name));
            }
        }

        #endregion

        #region Logger
        /// <summary>
        /// This is a helper method to provide logging wrapper.
        /// </summary>
        ///
        /// <remarks>
        /// Any exception will throw to caller directly.
        /// </remarks>
        ///
        /// <param name="call">
        /// The delegation to execute.
        /// </param>
        /// <param name="parameters">
        /// The parameters in the delegation method.
        /// </param>
        internal static void LoggingWrapper(this ILog logger, Action call, params object[] parameters)
        {
            var callingMethod = new StackTrace().GetFrame(1).GetMethod();
            var start = LogMethodEntry(logger, callingMethod, parameters);

            try
            {
                call();
            }
            catch (Exception e)
            {
                LogException(logger, callingMethod, e);
                throw;
            }
           
           LogMethodExit(logger, callingMethod, start);
        }

        /// <summary>
        /// This is a helper method to provide logging wrapper.
        /// </summary>
        ///
        /// <typeparam name="T">
        /// The return type.
        /// </typeparam>
        ///
        /// <remarks>
        /// Any exception will throw to caller directly.
        /// </remarks>
        ///
        /// <param name="call">
        /// The delegation to execute.
        /// </param>
        /// <param name="parameters">
        /// The parameters in the delegation method.
        /// </param>
        ///
        /// <returns>
        /// The value returned by <paramref name="call"/>.
        /// </returns>
        internal static T LoggingWrapper<T>(this ILog logger, Func<T> call, params object[] parameters)
        {
            var callingMethod = new StackTrace().GetFrame(1).GetMethod();
            var start = LogMethodEntry(logger, callingMethod, parameters);

            T ret;
            try
            {
                ret = call();
            }
            catch (Exception e)
            {
                LogException(logger, callingMethod, e);
                throw;
            }

            return LogReturnValue(logger, callingMethod, ret, start);
        }

        /// <summary>
        /// Logs a method exit at verbose level.
        /// </summary>
        ///
        /// <param name="method">
        /// The method where the logging occurs.
        /// </param>
        /// <param name="start">
        /// The method start date time.
        /// </param>
        private static void LogMethodExit(ILog logger, MethodBase method, DateTime start)
        {
            LogDebug(logger, string.Format("Exiting method {0}.{1}. Execution time: {2}ms",
                method.DeclaringType, method.Name, (DateTime.Now - start).TotalMilliseconds));
        }

        /// <summary>
        /// Logs the return value and method exit on verbose level.
        /// </summary>
        ///
        /// <typeparam name="R">
        /// The method return type.
        /// </typeparam>
        ///
        /// <param name="method">
        /// The method where the logging occurs.
        /// </param>
        /// <param name="returnValue">
        /// The return value to log.
        /// </param>
        /// <param name="start">
        /// The method start date time.
        /// </param>
        private static R LogReturnValue<R>(ILog logger, MethodBase method, R returnValue, DateTime start)
        {
            LogDebug(logger, string.Format("Exiting method {0}.{1}. Execution time: {2}ms. Return value: {3}",
                method.DeclaringType, method.Name, (DateTime.Now - start).TotalMilliseconds,
                GetObjectDescription(returnValue)));
            return returnValue;
        }

        /// <summary>
        /// Logs a method entry at verbose level.
        /// </summary>
        ///
        /// <param name="method">
        /// The method where the logging occurs.
        /// </param>
        /// <param name="parameters">
        /// The method parameters.
        /// </param>
        ///
        /// <returns>
        /// The method start date time.
        /// </returns>
        /// <remarks>The internal exception may be thrown directly.</remarks>
        private static DateTime LogMethodEntry(ILog logger, MethodBase method, params object[] parameters)
        {
            var logFormat = new StringBuilder();
            var pis = method.GetParameters();
            var methodName = string.Format("{0}.{1}", method.DeclaringType, method.Name);
            
            logFormat.AppendFormat("Entering method {0}", methodName);
            if (parameters.Length > 0 && pis.Length >= parameters.Length)
            {
                logFormat.AppendLine().Append("Argument Values:");
                for (int i = 0; i < parameters.Length; i++)
                {
                    logFormat.Append("\t").Append(pis[i].Name).Append(": ");
                    logFormat.Append(GetObjectDescription(parameters[i]));
                }
            }

            LogDebug(logger, logFormat.ToString());

            return DateTime.Now;
        }

        /// <summary>
        /// Logs the given exception on ERROR level.
        /// </summary>
        ///
        /// <param name="method">
        /// The method where the logging occurs.
        /// </param>
        /// <param name="ex">
        /// The exception to be logged.
        /// </param>
        private static void LogException(ILog logger, MethodBase method, Exception ex)
        {
            LogError(logger, string.Format("Error in method {0}.{1}.\n\nDetails:\n{2}",
                method.DeclaringType, method.Name, ex.ToString()));
        }

        /// <summary>
        /// Logs the given message on verbose level.
        /// </summary>
        ///
        /// <param name="message">
        /// The message to log.
        /// </param>
        private static void LogDebug(ILog logger, string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Logs the given message on ERROR level.
        /// </summary>
        ///
        /// <param name="message">
        /// The message to log.
        /// </param>
        private static void LogError(ILog logger, string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Gets JSON description of the object.
        /// </summary>
        ///
        /// <param name="obj">The object to describe.</param>
        /// <returns>The JSON description of the object.</returns>
        internal static string GetObjectDescription(object obj)
        {
            try
            {
                var stream = obj as Stream;
                if (stream != null)
                {
                    return stream.Length.ToString();
                }

                return JsonConvert.SerializeObject(obj, SerializerSettings);
            }
            catch
            {
                return "[Can't express this value]";
            }
        }

        /// <summary>
        /// Represents the JSON serializer settings.
        /// </summary>
        private static readonly JsonSerializerSettings SerializerSettings = 
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "MM/dd/yyyy HH:mm:ss",
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        #endregion
    }
}
