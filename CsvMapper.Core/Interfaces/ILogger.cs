using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvMapper.Core.Interfaces
{
    public interface ILogger
    {
        void SendMessage(string theMessage);
    }
}
