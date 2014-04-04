using System;
using System.IO;
using CsvMapper.Infrastructure.ResolvingDependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvMapper.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class DataRespostory_TransformData_IsNotNull
    {
        [TestMethod]
        public void DataRespostory_TransformDataFromContainerCreation_IsNotNull()
        {
            var items = new RepositoryData().DataRepository.Create( 
                                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                    "2 year old patient list immunisation targets.csv"), 
                                    new RepositoryMaps("2 year old patient list immunisation targets.xml").Maps);

            items = new RepositoryData().DataRepository.Transform(
                                    new RepositoryMaps("2 year old patient list immunisation targets.xml").Maps,
                                    items);

            Assert.IsNotNull(items);
        }
    }
}
