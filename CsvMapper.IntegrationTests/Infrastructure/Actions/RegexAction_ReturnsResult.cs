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
    public class RegexAction_ReturnsResult
    {
        private ModelMap _map;

        [TestInitialize]
        public void TestInitialise()
        {
            _map = new ModelMap
            {
                ConnectTo = 0,
                DataType = "String",
                Destination = "MMR_UNDER_GMS",
                Id = 1,
                Processes = (IEnumerable<ModelProcess>)new List<ModelProcess>
            {
                new ModelProcess
                {
                    Where = "^True$", //^.*True.*$
                    Make = "Y",
                    ProcessAction = "RegexAction"
                },
                 new ModelProcess
                {
                    Where = "^False$", //^.*False.*$
                    Make = "N",
                    ProcessAction = "RegexAction"
                }
            },
                Source = string.Empty
            };
        }

        [TestMethod]
        public void RegexAction_TestIfMatch_True_Returns_Y()
        {
            var factory = new AutoFactory();
            string testData = "True";
            foreach (var process in _map.Processes)
            {
                IActionable action = factory.CreateInstance(process.ProcessAction.ToLower());
                testData = action.Transform(_map, process, testData);
            }
            Assert.AreEqual(testData, "Y");
        }

        [TestMethod]
        public void RegexAction_TestIfMatch_False_Returns_N()
        {
            var factory = new AutoFactory();
            string testData = "False";
            foreach (var process in _map.Processes)
            {
                IActionable action = factory.CreateInstance(process.ProcessAction.ToLower());
                testData = action.Transform(_map, process, testData);
            }
            Assert.AreEqual(testData, "N");
        }

        [TestMethod]
        public void RegexAction_TestIfMatch_Testing_Returns_Testing()
        {
            var factory = new AutoFactory();
            string testData = "Testing";
            foreach (var process in _map.Processes)
            {
                IActionable action = factory.CreateInstance(process.ProcessAction.ToLower());
                testData = action.Transform(_map, process, testData);
            }
            Assert.AreEqual(testData, "Testing");
        }

        [TestMethod]
        public void RegexAction_TestIfMatch_Truelight_Returns_Truelight()
        {
            var factory = new AutoFactory();
            string testData = "Truelight";
            foreach (var process in _map.Processes)
            {
                IActionable action = factory.CreateInstance(process.ProcessAction.ToLower());
                testData = action.Transform(_map, process, testData);
            }
            Assert.AreEqual(testData, "Truelight");
        }
    }
}
