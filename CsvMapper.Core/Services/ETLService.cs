using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;

namespace CsvMapper.Core.Services
{
    public class ETLService : IDataRepository
    {
        private readonly IDataRepository _dataRepository;

        public ETLService(IDataRepository theRepository)
        {
            _dataRepository = theRepository;
        }

        public IDataRepository DataRepository
        {
            get { return _dataRepository; }
        }

        public ModelDataContainer Create(string source, IEnumerable<ModelMap> maps)
        {
            return DataRepository.Create(source, maps);
        }

        public ModelDataContainer Transform(IEnumerable<ModelMap> maps, ModelDataContainer items)
        {
            return  DataRepository.Transform(maps,items);
        }

        public void Save(string destination, ModelDataContainer items)
        {
            DataRepository.Save(destination,items);
        }
    }
}
