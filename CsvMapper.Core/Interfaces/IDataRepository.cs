using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CsvMapper.Core.Models;

namespace CsvMapper.Core.Interfaces
{
    public interface IDataRepository
    {
        ModelDataContainer Create(string source, IEnumerable<ModelMap> maps);
        ModelDataContainer Transform(IEnumerable<ModelMap> maps, ModelDataContainer items);
        void Save(string destination, ModelDataContainer items );
    }
}
