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
    public class RemoveSpace_ReturnsStringWithNoSpace
    {
        private ModelMap _map;

        [TestInitialize]
        public void TestInitialise()
        {
            _map = new ModelMap
            {
                ConnectTo = 0,
                DataType = "String",
                Destination = "NHS_Number",
                Id = 1,
                Processes = (IEnumerable<ModelProcess>)new List<ModelProcess>
            {
                new ModelProcess
                {
                    Where = string.Empty,
                    Make = string.Empty,
                    ProcessAction = "Removespaces"
                }
            },
                Source = "NHS Number"
            };
        }

        [TestMethod]
        public void CheckToSeeIfSpacesRemoved()
        {
            var factory = new AutoFactory();
            IActionable process = factory.CreateInstance(_map.Processes.First().ProcessAction.ToLower());
            string testNHSNo = "121 2432 532 543 2342 234 234  324 ";
            string result = process.Transform(_map, _map.Processes.First(), testNHSNo);
            Assert.IsFalse(result.Contains(" "));
        }
    }
}
