using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmsTimingTask.Services;
using SmsTimingTask.Exceptions;

namespace SmsTimingTask.Tests.Services
{
    /// <summary>
    /// This class contains parameterized unit tests for
    /// <see cref="FirebirdDatabaseServiceTest"/> class.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    [TestClass]
    public class FirebirdDatabaseServiceTest : BaseServiceTest
    {
        /// <summary>
        /// 
        /// </summary>
        private FirebirdDatabaseService service;
        
        /// <summary>
        /// Sets up the environment before executing each test in this class.
        /// </summary>
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            service = new FirebirdDatabaseService(logger, connectionString);
        }

        /// <summary>
        /// Test creation of <see cref = "FirebirdDatabaseService" />  
        /// with logger null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FirebirdDatabaseService_NullLogger_ThrowException()
        {
            var process = new FirebirdDatabaseService(null, connectionString);
        }

        /// <summary>
        /// Test creation of <see cref = "FirebirdDatabaseService" />  
        /// with null connection string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void FirebirdDatabaseService_NullConnectionString_ThrowException()
        {
            var process = new FirebirdDatabaseService(logger, null);
        }

        /// <summary>
        /// Test creation of <see cref = "FirebirdDatabaseService" />  
        /// with empty connection string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void FirebirdDatabaseService_EmptyConnectionString_ThrowException()
        {
            var process = new FirebirdDatabaseService(logger, string.Empty);
        }

        /// <summary>
        /// Test creation of <see cref = "FirebirdDatabaseService" />  
        /// with valid params
        /// </summary>
        [TestMethod]
        public void FirebirdDatabaseService_ValidParams_CreateNewObject()
        {
            var process = new FirebirdDatabaseService(logger, connectionString);
            Assert.IsNotNull(process, 
                "FirebirdDatabaseService object is expected to be not null.");
        }

        /// <summary>
        /// Test <see cref = "FirebirdDatabaseService.CreateConnection()" />  
        /// should return created connection object.
        /// <summary>
        [TestMethod]
        public void CreateConnection_Call_ReturnValid()
        {
            var connection = service.CreateConnection();
            Assert.IsNotNull(connection,
                "Connectiont is not expected to be null.");
        }
    }
}
