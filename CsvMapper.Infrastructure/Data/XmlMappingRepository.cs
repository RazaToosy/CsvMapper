using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;

namespace CsvMapper.Infrastructure.Data
{
    public class XmlMappingRepository : IMappingRepository
    {
        public IEnumerable<ModelMap> ReturnMappings(string nameOfXMLFile)
        {
            XDocument xmlDoc = XDocument.Load(nameOfXMLFile);
            return (from search in xmlDoc.Descendants("Map")
                select new ModelMap
                {
                    Id = Convert.ToInt32(search.Attribute("id").Value),
                    DataType = search.Attribute("datatype").Value,
                    Source = search.Element("Source").Value,
                    ConnectTo = Convert.ToInt32(search.Element("Source").Attribute("ConnectTo").Value),
                    SourceId = Convert.ToInt32(search.Element("Source").Attribute("SourceId").Value),
                    Destination = search.Element("Destination").Value,
                    Processes = (from p in search.Descendants("Command")
                        select new ModelProcess
                        {
                            Make = p.Element("Make") != null ? p.Element("Make").Value : string.Empty ,
                            ProcessAction = p.Attribute("action").Value,
                            Where = p.Element("Where") != null ? p.Element("Where").Value : string.Empty
                        }).ToList(),
                }).ToList();
        }
    }
}
