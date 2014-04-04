using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvMapper.Core.Models
{
    public class ModelDataContainer
    {

        private List<ModelDataColumn> _theColumns;
        private List<ModelDataRow> _theRows;

        public ModelDataContainer()
        {
            TheColumns = new List<ModelDataColumn>();
            TheRows = new List<ModelDataRow>();
        }

        public List<ModelDataColumn> TheColumns
        {
            get { return _theColumns; }
            set { _theColumns = value; }
        }

        public List<ModelDataRow> TheRows
        {
            get { return _theRows; }
            set { _theRows = value; }
        }
    }
}
