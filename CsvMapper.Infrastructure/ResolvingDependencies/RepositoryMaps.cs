using System.Collections.Generic;
using CsvMapper.Core.Models;
using CsvMapper.Core.Services;
using CsvMapper.Infrastructure.Data;

namespace CsvMapper.Infrastructure.ResolvingDependencies
{
    public class RepositoryMaps
    {
        public IEnumerable<ModelMap> Maps { get; set; }
        public RepositoryMaps(string nameOfXMLFile)
        {
            Maps = new MappingService(new XmlMappingRepository()).ListMap(nameOfXMLFile);
        }
    }
}
