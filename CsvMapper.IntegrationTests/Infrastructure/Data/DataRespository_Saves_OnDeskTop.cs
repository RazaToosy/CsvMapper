using System;
using System.IO;
using System.Runtime;
using CsvMapper.Infrastructure.ResolvingDependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvMapper.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class DataRespository_Saves_OnDeskTop
    {
        [TestMethod]
        public void DataRespostory_SavesOnDeskTop_ETAlsoDone()
        {
            var items = new RepositoryData().DataRepository.Create(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        "2 year old patient list immunisation targets.csv"),
                        new RepositoryMaps("2 year old patient list immunisation targets.xml").Maps);

            items = new RepositoryData().DataRepository.Transform(
                                    new RepositoryMaps("2 year old patient list immunisation targets.xml").Maps,
                                    items);

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                string.Format("{0}.Output.csv",
                    Path.GetFileNameWithoutExtension("2 year old patient list immunisation targets.csv")));

           new RepositoryData().DataRepository.Save(filename, items);

            Assert.IsTrue(File.Exists(filename));

        }
    }
}
