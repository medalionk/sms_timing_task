using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmsTimingTask.Services.Impl;
using SmsTimingTask.Exceptions;

namespace SmsTimingTask.Tests.Services.Impl
{
    /// <summary>
    /// This class contains parameterized unit tests for
    /// <see cref="DataProccess"/> class.
    /// </summary>
    ///
    /// <author>Bilal Abdullah</author>
    [TestClass]
    public class DataProccessTest : BaseServiceTest
    {
        /// <summary> </summary>
        private DataProccess process;

        /// <summary>
        /// Sets up the environment before executing each test in this class.
        /// </summary>
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            process = new DataProccess(logger, connectionString, outputFolder);     
        }

        /// <summary>
        /// Test creation of <see cref = "DataProccess" />  with logger null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataProccess_NullLogger_ThrowException()
        {
            var process = new DataProccess(null, connectionString, outputFolder);            
        }

        /// <summary>
        /// Test creation of <see cref = "DataProccess" />  
        /// with connection string null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void DataProccess_NullConnectionString_ThrowException()
        {
            var process = new DataProccess(logger, null, outputFolder);
        }

        /// <summary>
        /// Test creation of <see cref = "DataProccess" />  
        /// with connection string empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void DataProccess_EmptyConnectionString_ThrowException()
        {
            var process = new DataProccess(logger, string.Empty, outputFolder);
        }

        /// <summary>
        /// Test creation of <see cref = "DataProccess" />  
        /// with valid params
        /// </summary>
        [TestMethod]
        public void DataProccess_ValidParams_CreateNewObject()
        {
            var process = new DataProccess(logger, connectionString, outputFolder);
            Assert.IsNotNull(process, "DataProccess object is expected to be not null.");           
        }

        /// <summary>
        /// Test <see cref = "DataProccess.Summary(string)" />  
        /// with null param
        /// <summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Summary_NullParam_ThrowException()
        {            
            process.Summary(null);            
        }

        /// <summary>
        /// Test <see cref = "DataProccess.Summary(string)" />  
        /// with empty param string
        /// <summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Summary_EmptyParam_ThrowException()
        {
            process.Summary(string.Empty);
        }

        /// <summary>
        /// Test <see cref = "DataProccess.Summary(string)" />  
        /// with invalid param string
        /// <summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Summary_InvalidParam_ThrowException()
        {
            string field = "field";            
            process.Summary(field);
        }

        /// <summary>
        /// Test <see cref = "DataProccess.Summary(string)" />  
        /// with valid field param
        /// <summary>
        [TestMethod]
        public void Summary_ValidParam_WriteSummaryToFile()
        {
            var expectedFilename = "Test_Result_Task_1.csv";
            var field = "GENDER";
            var actualFilename = process.Summary(field);

            ValidateTwoFilesResult(actualFilename, expectedFilename);
        }
    }
}
