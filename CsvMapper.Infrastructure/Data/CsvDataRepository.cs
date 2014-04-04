using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;
using CsvMapper.Infrastructure.Factory;
using CsvMapper.Infrastructure.ResolvingDepdendencies;
using CsvMapper.Infrastructure.Services;
using LINQtoCSV;

namespace CsvMapper.Infrastructure.Data
{
    public class CsvDataRepository : IDataRepository
    {
        public ModelDataContainer Create(string source, IEnumerable<ModelMap> maps)
        {
            var dataContainer = new ModelDataContainer();
            var ColumnsIndexesToExclude = maps.Where(d => d.Destination == string.Empty).Select(i => i.Id).ToList();
            try
            {
                CsvFileDescription outputFileDescription = new CsvFileDescription
               {
                   SeparatorChar = ',', // tab delimited
                   FirstLineHasColumnNames = false, // no column names in first record
                   FileCultureName = "en-GB" // use formats used in The UK
               };

                CsvContext csvContext = new CsvContext();
                var theList = csvContext.Read<DataRow>(source, outputFileDescription);

                bool firstRow = true;
                //Need to Create ModelContainer from output of Linq to Csv
                foreach (DataRow dataRow in theList)
                {
                    if (firstRow)
                    {
                        int columnIndex = 1;
                        IEnumerable<ModelMap> sourceMappings =
                            maps.Select(s => s).Where(sid => sid.SourceId > 0).OrderBy(o => o.SourceId).ToList();

                        foreach (DataRowItem item in dataRow)
                        {
                            if (item.Value !=null)
                                dataContainer.TheColumns.Add(new ModelDataColumn
                                {
                                    ColumnName = item.Value,
                                    ConnectTo = sourceMappings.Where(id => id.SourceId == columnIndex).Select(c => c.ConnectTo).First()
                                });
                                columnIndex++;
                        }
                        firstRow = false;
                    }
                    else if (dataRow[0].Value != null)
                    {
                        var destinationRow = new ModelDataRow();
                        destinationRow.Value.AddRange(dataRow.Select(s => s.Value ?? string.Empty).ToList());
                        dataContainer.TheRows.Add(destinationRow);
                    }
                }

                //From LinqToCSV Get extra columns at the end of the sheet. These need to be removed too.
                for (int i = dataContainer.TheRows.First().Value.Count; i > dataContainer.TheColumns.Count; i--)
                {
                    ColumnsIndexesToExclude.Add(i);
                }

            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }

            return removeExcludedColumns(ColumnsIndexesToExclude, dataContainer);
        }

        public ModelDataContainer Transform(IEnumerable<ModelMap> maps, ModelDataContainer input)
        {
            // Will contain mapping to convert the Source to Destination.
            // Use the XML Mapping here with a ?factory pattern to work out the transformation of each row?
            var outputContainer = new ModelDataContainer();
            var factory = new AutoFactory();
            try
            {

                outputContainer.TheColumns = (from map in maps
                                              where map.Destination != string.Empty
                                              orderby map.Id
                                              select new ModelDataColumn
                                              {
                                                  ColumnName = map.Destination,
                                                  ConnectTo = map.ConnectTo

                                              }).ToList();

                foreach (var dataRow in input.TheRows)  //DataRow in the input Container 
                {
                    var newRow = new ModelDataRow();
                    string theValue = string.Empty;
                    var inputRow = new List<string>(dataRow.Value);
                    outputContainer.TheRows.Add(newRow);
                    foreach (var map in maps) //Loop at  Process which maps to a Class. Factory Pattern uses reflection to instantiate it
                    {
                        theValue = map.ConnectTo == 0 ? string.Empty : inputRow[returnIndexOfInputConnection(input,map.ConnectTo)];
                        foreach (var action in map.Processes)
                        {
                            IActionable process = factory.CreateInstance(action.ProcessAction.ToLower()); //return the IActionable Object which takes the current Map, string and returns the result as a string.
                            theValue = process.Transform(map, action, theValue);
                        }
                        if (map.Processes.ToList().Count>0) newRow.Value.Add(theValue); //add this value to the Row only if process. Otherwise need to ignore
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }
            return outputContainer;
        }

        private int returnIndexOfInputConnection(ModelDataContainer input, int connectTo)
        {
            int columnIndex = 0;
            foreach (var column in input.TheColumns)
            {
                if (column.ConnectTo == connectTo) return columnIndex;
                columnIndex++;
            }
            return -1;
        }

        public void Save(string destination, ModelDataContainer items)  //Use inhouse csv exporter and takes ModelDataContainer which is in house.
        {
            try
            {
                new CsvModelDataContainerExport(items).ExportToFile(destination);
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }
        }

        private ModelDataContainer removeExcludedColumns(IEnumerable<int> columnsIndexesToExclude, ModelDataContainer dataContainer)
        {
            var newContainer = new ModelDataContainer();
            int columnCount = 1;

            foreach (var columnName in dataContainer.TheColumns)
            {
                if (!columnsIndexesToExclude.Contains(columnCount)) newContainer.TheColumns.Add(columnName);
                columnCount++;
            }

            foreach (var row in dataContainer.TheRows)
            {
                int rowCount = 1;
                var newRow = new ModelDataRow();
                foreach (var value in row.Value)
                {
                    if (!columnsIndexesToExclude.Contains(rowCount)) newRow.Value.Add(value);
                    rowCount++;
                }
                newContainer.TheRows.Add(newRow);
            }
            return newContainer;
        }
    }

    internal class DataRow : List<DataRowItem>, IDataRow
    {
    }




}
