using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;

namespace CsvMapper.Core.Services
{
    public class MappingService
    {
        private readonly IMappingRepository _mappingRepository;

        public MappingService(IMappingRepository mappingRepository)
        {
            _mappingRepository = mappingRepository;
        }

        public IEnumerable<ModelMap> ListMap(string nameOfXmlFile)
        {
            return _mappingRepository.ReturnMappings(nameOfXmlFile);
        }
    }
}
