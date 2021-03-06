﻿using LoowooTech.Stock.Common;
using LoowooTech.Stock.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LoowooTech.Stock.Tool
{
    /// <summary>
    /// 核对表的字段
    /// </summary>
    public class FieldStructureTool:ValueBaseTool, ITool
    {
        public string Name
        {
            get
            {
                return string.Format("规则{0}:核对表{1}的数据库标准字段类型、长度",ID,TableName);
            }
        }
        public bool Check(OleDbConnection connection)
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                
                var dict = new Dictionary<string, Field>();
                var table = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });
                var m = table.Columns.IndexOf("COLUMN_NAME");
                var n = table.Columns.IndexOf("NUMERIC_PRECISION");
                var l = table.Columns.IndexOf("CHARACTER_MAXIMUM_LENGTH");
                var a = table.Columns.IndexOf("DATA_TYPE");
                var length = 0;
                for(var i = 0; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    var name = row.ItemArray.GetValue(m).ToString().Trim();
                    if (!dict.ContainsKey(name))
                    {
                        dict.Add(name, new Field()
                        {
                            Name = name,
                            Title = row.ItemArray.GetValue(n).ToString().Trim(),
                            Length = int.TryParse(row.ItemArray.GetValue(l).ToString().Trim(), out length) ? length : 0,
                            Type = (FieldType)Enum.Parse(typeof(FieldType), row.ItemArray.GetValue(a).ToString().Trim())
                        });
                    }
                }
                var requireField = XmlClass.GetField(TableName);
                var str = string.Empty;
                foreach(var field in requireField)
                {
                    if (dict.ContainsKey(field.Name))
                    {
                        if (field != dict[field.Name])
                        {
                            str = string.Format("字段{0}与要求的类型或者长度不符", field.Name);
                            Console.WriteLine(str);
                            Messages.Add(str);
                           
                        }
                    }
                    else
                    {
                        str = string.Format("缺失字段{0};", field.Name);
                        Console.WriteLine(str);
                        Messages.Add(str);
                    }
                }
                QuestionManager.AddRange(Messages.Select(e => new Question { Code = "3101", Name =Name,Project=CheckProject.结构符合性, TableName = TableName, Description = e }).ToList());
                
            }
            return false;
        }
    }
}
