using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Services;
using CsvMapper.Infrastructure.Logging;

namespace CsvMapper.Infrastructure.ResolvingDepdendencies
{
    public class LoggerSingleton
    {
        private static LoggerSingleton _instance;

        private LoggerSingleton() { }

        public static LoggerSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoggerSingleton();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Accepts General Exception.
        /// </summary>
        /// <param name="Ex"></param>
        public void LogMessage(Exception Ex)
        {
            CreateLoggingDependency(String.Format("Time:{0}\r\nMessage:--->{1}\r\nStack:--->{2}\r\n\r\n", DateTime.Now,
                Ex.Message, Ex.StackTrace));
        }

        //overloads to accept other types of Exceptions to go here.
        //
        //
        //

        /// <summary>
        /// If want to change Logging Dependency, change here. Will need to create Implementation of ILogger Interface in Logging Folder
        /// </summary>
        /// <param name="TheMessage"></param>
        private void CreateLoggingDependency(string TheMessage)
        {
            new LoggingService(new Log4NetLogger()).SendMessage(TheMessage);
        }
    }
}