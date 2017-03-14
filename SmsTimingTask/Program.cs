using log4net;
using log4net.Config;
using System;
using System.Configuration;
using SmsTimingTask.Services;
using SmsTimingTask.Services.Impl;

namespace SmsTimingTask
{
    /// <summary>
    /// This class is the main entry point of the application.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    class Program
    {
        /// <summary>
        /// Represents the connection string name of a the database.
        /// </summary>
        private const string ConnectionStringName = "DefaultConnectionString";

        /// <summary>
        /// Represents the output path string name.
        /// </summary>
        private const string OutputFolderStringName = "OutputFolder";

        /// <summary>
        /// Represents the database connection string.
        /// </summary>
        private static readonly string connectionString = ConfigurationManager.
               ConnectionStrings[ConnectionStringName].ConnectionString;

        /// <summary>
        /// Represents file output path the CSV files
        /// </summary>
        private static readonly string outputPath =
            ConfigurationManager.AppSettings[OutputFolderStringName];

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        private static readonly ILog logger;

        /// <summary>
        /// Initializes the <see cref="Program"/> class.
        /// </summary>
        static Program()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(Program));
        }

        /// <summary>
        /// Print application help
        /// </summary>
        static void PrintHelp()
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("Enter the column name to group by!!!");
            Console.WriteLine("===================================================");
        }

        /// <summary>
        /// Main entry point of the program
        /// </summary>
        /// <param name="args">command line arguments</param>
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintHelp();
                return;
            }

            try
            {
                string field = args[0];
                //string field = "gender";
                IProcess process = new DataProccess(logger, connectionString, outputPath);
                process.Summary(field.ToUpper());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
