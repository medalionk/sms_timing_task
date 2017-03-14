using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;

namespace SmsTimingTask.Tests
{
    /// <summary>
    /// This class provides common methods used in unit tests.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    public class TestHelper
    {
        /// <summary>
        /// Represents the connection string name of a the database.
        /// </summary>
        private const string connectionStringName = "DefaultConnectionString";

        /// <summary>
        /// Represents the output path string name.
        /// </summary>
        private const string outputFolderStringName = "OutputFolder";

        /// <summary>
        /// Represents the output path string name.
        /// </summary>
        private const string actualResultPathStringName = "ActualResultFolder";

        /// <summary>
        /// Represents the path to the files with test results in CSV format.
        /// </summary>
        internal static readonly string actualResultPath = ConfigurationManager.
               AppSettings[actualResultPathStringName];

        /// <summary>
        /// Represents the database connection string.
        /// </summary>
        internal static readonly string connectionString = ConfigurationManager.
               ConnectionStrings[connectionStringName].ConnectionString;

        /// <summary>
        /// Represents file output path the CSV files
        /// </summary>
        internal static readonly string outputFolder =
            ConfigurationManager.AppSettings[outputFolderStringName];

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        internal readonly static ILog logger;

        /// <summary>
        /// Initializes the <see cref="TestHelper"/> class.
        /// </summary>
        static TestHelper()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(TestHelper)); 
        }

        /// <summary>
        /// Clears test CSV files in the folder.
        /// </summary>
        internal static void ClearOutputFolder()
        {
            var directory = new DirectoryInfo(outputFolder);
            if (directory.Exists)
            {
                foreach (var file in directory.GetFiles())
                {
                    file.Delete();
                }
            }
            else
            {
                directory.Create();
            }
        }

        /// <summary>
        /// Compares the actual test result file with the expected 
        /// test result file in CSV file.
        /// </summary>
        /// <param name="actualfile">The actual result file name.</param>
        /// <param name="expectedfile">The expected result file name.</param>
        internal static void ValidateTwoFilesResult(string actualfile, string expectedfile)
        {
            expectedfile = Path.Combine(actualResultPath, $"{expectedfile}");
            var expected = File.ReadAllText(expectedfile);
            var actual = File.ReadAllText(actualfile);
            var message = string.Format("Mismatch in {0} and {1} results in file.", 
                actualfile, expectedfile);

            Assert.AreEqual(expected, actual, message);
        }

        /// <summary>
        /// Compares the actual string test result with the 
        /// expected test result in CSV file.
        /// </summary>
        /// <param name="actual">The actual result.</param>
        /// <param name="expectedfile">The expected result file name.</param>
        internal static void ValidateFileResult(string actual, string expectedfile)
        {
            var expectedFilePath = Path.Combine(actualResultPath, $"{expectedfile}.csv");
            var expected = File.ReadAllText(expectedFilePath);

            Assert.AreEqual(expected, actual, "Mismatch in actual and expected results in file.");
        }
    }
}
