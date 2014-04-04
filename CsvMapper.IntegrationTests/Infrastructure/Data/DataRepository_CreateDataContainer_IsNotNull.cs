using System;
using System.Collections.Generic;
using System.IO;
using CsvMapper.Core.Models;
using CsvMapper.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvMapper.Infrastructure.ResolvingDependencies;

namespace CsvMapper.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class DataRepository_CreateDataContainer_IsNotNull
    {
        [TestMethod]
        public void CreateDataContainer_IsNotNull()
        {
            var items = new RepositoryData().DataRepository.Create(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "2 year old patient list immunisation targets.csv"), new RepositoryMaps("2 year old patient list immunisation targets.xml").Maps);

            Assert.IsNotNull(items);

        }
    }
}
