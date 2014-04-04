using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;
using CsvMapper.Infrastructure.ResolvingDepdendencies;

namespace CsvMapper.Infrastructure.Actions
{
    public class QuarterStart : IActionable
    {
        public string Transform(ModelMap mapToProcess, ModelProcess action, string input)
        {
            if (input == string.Empty) input = DateTime.Now.ToShortDateString();
            string result = string.Empty;
            try
            {
                DateTime date = DateTime.Parse(input);
                int quarter = ((((date.Month - 1) / 3) + 3) % 4) + 1;
                switch (quarter)
                {
                    case 2: result = string.Format("01/04/{0}", date.Year);
                        break;
                    case 3: result = string.Format("01/07/{0}", date.Year);
                        break;
                    case 4: result = string.Format("01/10/{0}", date.Year - 1);
                        break;
                    case 1: result = string.Format("01/01/{0}", date.Year);
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }
            return result;
        }
    }
}
