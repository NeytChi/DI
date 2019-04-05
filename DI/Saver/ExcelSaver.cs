using System;
using System.IO;
using DI.NDatabase;
using OfficeOpenXml;
using System.Collections.Generic;
using Common;

namespace DI.Saver
{
    public class ExcelSaver : ISaverDatabase
    {
        ClientSQL clientSQL;

        public ExcelSaver(ClientSQL clientSQL)
        {
            this.clientSQL = clientSQL;
        }
        public void SaveDatabase(string database)
        {
            if (database == null)
            {
                Logger.WriteLog("Input value is null, SaveDatabase() ExcelSaver.", LogLevel.Error);
                return;
            }
            int tempRow = 0;
            string name = DateTime.Now.ToString() + ".xls";
            string path = Directory.GetCurrentDirectory() + "/Saves/Excel/";
            Directory.CreateDirectory(path);
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Worksheet1");
                List<string> tables = clientSQL.ShowTables();
                for (int i = 0; i < tables.Count; i++)
                {
                    List<List<string>> tableData = new List<List<string>>();
                    List<string> columns = clientSQL.DescribeCurrentTable(tables[i]);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        List<string> columData = clientSQL.SelectColumTable(tables[i], columns[j]);
                        tableData.Add(columData);
                    }
                    for (int j = 0; j < tableData.Count; j++)
                    {
                        for (int g = 0; g < tableData[j].Count; g++)
                        {
                            worksheet.Cells[g + tempRow + 1, j + 1].Value = tableData[j][g];
                        }
                    }
                    tempRow += tableData[0].Count;
                    ++tempRow;
                    worksheet.Cells[tempRow, 1].Value = "";
                }
                FileInfo excelFile = new FileInfo(path + name);
                excel.SaveAs(excelFile);
            }
        }
    }
}
