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
    public class LoadCsv_ReturnsNotEmptyDictionary
    {
        [TestMethod]
        public void LoadCsvFileAndConvertToDictionaryThenReplaceInputWithValue()
        {
            ModelMap map = new ModelMap
            {
                ConnectTo = 0,
                DataType = "String",
                Destination = "GP_CODE",
                Id = 1,
                Processes = (IEnumerable<ModelProcess>)new List<ModelProcess>
            {
                new ModelProcess
                {
                    Where = "GPCodes.csv",
                    Make = string.Empty,
                    ProcessAction = "Load"
                }
            },
                Source = "Usual GP's Full Name"
            };

            string input = "TOOSY, Raza (Dr)";

            var factory = new AutoFactory();

            IActionable process = factory.CreateInstance(map.Processes.First().ProcessAction.ToLower());

            string result = process.Transform(map, map.Processes.First(), input);

            Assert.AreNotEqual(result, "No Code Found");
        }
    }
}
