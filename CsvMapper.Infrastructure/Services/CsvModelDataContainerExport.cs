using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvMapper.Core.Models;

namespace CsvMapper.Infrastructure.Services
{
    /// <summary>
    /// Converts a ModelDataContainer to a Csv File
    /// Usage new CsvModelDataContainerExport(ModelDataContainer).ExportTofile(path)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvModelDataContainerExport
    {
        private readonly ModelDataContainer _theContainer;

        public CsvModelDataContainerExport(ModelDataContainer theContainer)
        {
            _theContainer = theContainer;
        }

        public string Export()
        {
            return Export(true);
        }

        public string Export(bool includeHeaderLine)
        {

            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            //IList<PropertyInfo> propertyInfos = typeof(T).GetProperties();

            if (includeHeaderLine)
            {
                //add header line.
                foreach (var header in _theContainer.TheColumns)
                {
                    sb.Append(header.ColumnName).Append(",");
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            //add value for each property.
            foreach (ModelDataRow row in _theContainer.TheRows)
            {
                foreach (string value in row.Value)
                {
                    sb.Append(MakeValueCsvFriendly(value)).Append(",");
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            return sb.ToString();
        }

        //export to a file.
        public void ExportToFile(string path)
        {
            File.WriteAllText(path, Export());
        }

        //export as binary data.
        public byte[] ExportToBytes()
        {
            return Encoding.UTF8.GetBytes(Export());
        }

        //get the csv value for field.
        private string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";
            if (value is Nullable && ((INullable)value).IsNull) return "";

            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();

            if (output.Contains(",") || output.Contains("\""))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;

        }
    }
}
