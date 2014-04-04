using System.Text.RegularExpressions;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;

namespace CsvMapper.Infrastructure.Actions
{
    public class RegexAction : IActionable
    {
        public string Transform(ModelMap mapToProcess, ModelProcess action, string input)
        {
            var regex = new Regex(action.Where);
            return regex.IsMatch(input) ? action.Make : input;
        }
    }
}
