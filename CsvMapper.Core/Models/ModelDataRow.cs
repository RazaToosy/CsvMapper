using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvMapper.Core.Models
{
    public class ModelDataRow
    {

        private List<string> _value;

        public ModelDataRow()
        {
            Value = new List<string>();
        }

        public List<string> Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
