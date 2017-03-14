using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmsTimingTask.Tests.Services
{
    /// <summary>
    /// Represents base class for unit tests
    /// unit tests for <see cref="DatabaseService"/> class.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    [TestClass]
    public class BaseServiceTest : TestHelper
    {
        /// <summary>
        /// Sets up the environment before executing each test in this class.
        /// </summary>
        [TestInitialize]
        public virtual void SetUp()
        {
            ClearOutputFolder();
        }

        /// <summary>
        /// Clean up the environment after executing each test in this class.
        /// </summary>
        [TestCleanup]
        public virtual void CleanUp()
        {
            ClearOutputFolder();
        }
    }
}
