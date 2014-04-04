using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CsvMapper.Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvMapper.IntegrationTests.Infrastructure.Data
{
    /// <summary>
    /// Summary description for XMLMappingRepository_ReturnMappingsShould
    /// </summary>
    [TestClass]
    public class XMLMappingRepository_ReturnMappingsShould
    {
        public XMLMappingRepository_ReturnMappingsShould()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReturnMappings_IsNotNull()
        {
            var mappingRespository = new XmlMappingRepository();
            var maps = mappingRespository.ReturnMappings("2 year old patient list immunisation targets.xml");
            Assert.IsNotNull(maps);
        }
    }
}
