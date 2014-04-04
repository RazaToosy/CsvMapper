using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Models;

namespace CsvMapper.Core.Interfaces
{
    public interface IActionable
    {
        string Transform(ModelMap mapToProcess, ModelProcess action, string input);
    }
}
