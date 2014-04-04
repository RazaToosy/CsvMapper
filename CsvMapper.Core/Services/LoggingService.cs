using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;

namespace CsvMapper.Core.Services
{
    public class LoggingService
    {
        private readonly ILogger _theMessageSender;

        public LoggingService(ILogger TheMessageSender)
        {
            _theMessageSender = TheMessageSender;
        }

        public void SendMessage(string theMessage)
        {
            _theMessageSender.SendMessage(theMessage);
        }
    }
}
