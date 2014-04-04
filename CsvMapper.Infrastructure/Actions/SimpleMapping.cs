﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Models;

namespace CsvMapper.Infrastructure.Actions
{
    public class SimpleMapping : IActionable
    {
        public string Transform(ModelMap mapToProcess, ModelProcess action, string input)
        {
            return input;
        }
    }
}
