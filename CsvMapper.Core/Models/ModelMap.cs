using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CsvMapper.Core.Models
{
    public class ModelMap
    {
        public int Id { get; set; }
        public int ConnectTo { get; set; }
        public string DataType { get; set; }
        public string Source { get; set; }
        public int SourceId { get; set; }
        public string Destination { get; set; }
        public IEnumerable<ModelProcess> Processes { get; set; }
    }
}
