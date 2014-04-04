using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;
using CsvMapper.Infrastructure.ResolvingDepdendencies;
using LINQtoCSV;
using DataRow = CsvMapper.Infrastructure.Data.DataRow;

namespace CsvMapper.Infrastructure.Actions
{
    /// <summary>
    /// Loads a csv with 2 columns, looks for the 1st and replaces with the 2nd.
    /// </summary>
    public class Load : IActionable
    {
        public string Transform(ModelMap mapToProcess, ModelProcess actionProcess, string input)
        {

            string result = string.Empty;

            try
            {
                CsvFileDescription outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',', // tab delimited
                    FirstLineHasColumnNames = false, // no column names in first record
                    FileCultureName = "en-GB" // use formats used in The Netherlands
                };

                CsvContext csvContext = new CsvContext();

                string fileLocation =
                    Path.Combine(
                        new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.FullName,
                        actionProcess.Where);

                var theList = csvContext.Read<Data.DataRow>(fileLocation, outputFileDescription);

                var thePairs = theList.ToDictionary(
                                                                    d=>d[0].Value.ToString(),
                                                                    d =>d[1].Value.ToString());

                if (thePairs.ContainsKey(input)) result = input != null ? thePairs[input] : "No Code Found";
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }

            return result;

        }
    }
}
