﻿using LoowooTech.Stock.Common;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LoowooTech.Stock.Rules
{
    public class ExcelOne:ExcelBase,IExcel
    {
        public ExcelOne()
        {
            ExcelName = "表1";
            Space = 1;
        }
        public override void Check()
        {
            base.Check();
        }
    }
}
