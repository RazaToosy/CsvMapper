using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Models;

namespace CsvMapper.Core.Interfaces
{
   public  interface IMappingRepository
   {
       IEnumerable<ModelMap> ReturnMappings(string nameOfXMLFile);
   }
}
