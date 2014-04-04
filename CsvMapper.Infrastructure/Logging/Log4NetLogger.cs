using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Services;
using log4net;

namespace CsvMapper.Infrastructure.Logging
{
    public class Log4NetLogger : ILogger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Log4NetLogger));

        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void SendMessage(string theMessage)
        {
            Log.Error(theMessage);
        }
    }
}
