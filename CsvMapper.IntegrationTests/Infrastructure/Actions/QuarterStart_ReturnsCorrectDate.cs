using System;
using System.Collections.Generic;
using System.Linq;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;
using CsvMapper.Infrastructure.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvMapper.IntegrationTests.Infrastructure.Actions
{
    [TestClass]
    public class QuarterStart_ReturnsCorrectDate
    {
        private ModelMap _map;

        [TestInitialize]
        public void TestInitialise()
        {
             _map = new ModelMap
            {
                ConnectTo = 0,
                DataType = "DateTime",
                Destination = "LIST_DATE",
                Id = 1,
                Processes = (IEnumerable<ModelProcess>)new List<ModelProcess>
            {
                new ModelProcess
                {
                    Where = string.Empty,
                    Make = string.Empty,
                    ProcessAction = "QuarterStart"
                }
            },
                Source = string.Empty
            };
        }

        [TestMethod]
        public void QuarterStartReturnsCorrectDataForQuarter4()
        {
            var factory = new AutoFactory();
            IActionable process = factory.CreateInstance(_map.Processes.First().ProcessAction.ToLower());
            string testDate = "01/12/2014";
            string result = process.Transform(_map,_map.Processes.First(), testDate);
            Assert.AreEqual(result, "01/07/2014");
        }

        [TestMethod]
        public void QuarterStartReturnsCorrectDataForQuarter1()
        {
            var factory = new AutoFactory();
            IActionable process = factory.CreateInstance(_map.Processes.First().ProcessAction.ToLower());
            string testDate = "15/05/2014";
            string result = process.Transform(_map, _map.Processes.First(), testDate);
            Assert.AreEqual(result, "01/01/2014");
        }

        [TestMethod]
        public void QuarterStartReturnsCorrectDataForQuarter2()
        {
            var factory = new AutoFactory();
            IActionable process = factory.CreateInstance(_map.Processes.First().ProcessAction.ToLower());
            string testDate = "01/09/2014";
            string result = process.Transform(_map, _map.Processes.First(), testDate);
            Assert.AreEqual(result, "01/04/2014");
        }

        [TestMethod]
        public void QuarterStartReturnsCorrectDataForQuarter3()
        {
            var factory = new AutoFactory();
            IActionable process = factory.CreateInstance(_map.Processes.First().ProcessAction.ToLower());
            string testDate = "01/02/2015";
            string result = process.Transform(_map, _map.Processes.First(), testDate);
            Assert.AreEqual(result, "01/10/2014");
        }
    }
}
