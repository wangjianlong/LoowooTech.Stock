﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Stock.Models
{
    public class ExcelField
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public ExcelType Type { get; set; }
        public string WhereClause { get; set; }
        public Compute Compute { get; set; }
        public string SQL
        {
            get
            {
                var str = string.Empty;
                switch (Compute)
                {
                    case Compute.Sum:
                        str = string.Format("Sum({0})", Name);
                        break;
                    case Compute.Count:
                        str = "Count(*)";
                        break;
                }
                return str;
            }
        }
        public string Unit { get; set; }
        public object Val { get; set; }
        public string Value { get; set; }

    }
    public enum ExcelType
    {
        Int,
        Double
    }
    public enum Compute
    {
        Sum,
        Count
    }
}
