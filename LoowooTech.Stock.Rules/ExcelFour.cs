﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Stock.Rules
{
    public class ExcelFour:ExcelBase,IExcel
    {
        public ExcelFour()
        {
            ExcelName = "表4";
            Space = 1;
        }
    }
}
