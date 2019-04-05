using System;
using System.IO;
using DI.NDatabase;
using Xceed.Words.NET;
using System.Collections.Generic;

namespace DI.Saver
{
    public class WordSaver : ISaverDatabase
    {
        ClientSQL clientSQL;

        public WordSaver(ClientSQL clientSQL)
        {
            this.clientSQL = clientSQL;
        }
        public void SaveDatabase(string database)
        {
            string name = DateTime.Now.ToString();
            string path = Directory.GetCurrentDirectory() + "/Saves/Word/";
            Directory.CreateDirectory(path);
            DocX document = DocX.Create(path + name, DocumentTypes.Document);
            document.SaveAs(path + name + ".docx");
            List<string> tables = clientSQL.ShowTables();
            foreach (string table in tables)
            {
                List<List<string>> columsData = new List<List<string>>();
                List<string> describeTable = clientSQL.DescribeTable(table);
                foreach(string describe in describeTable)
                {
                    List<string> columData = clientSQL.SelectColumTable(table, describe);
                    columsData.Add(columData);
                }
                Table docTable = document.AddTable(columsData[0].Count, describeTable.Count);
                docTable.Design = TableDesign.ColorfulListAccent1;
                docTable.Alignment = Alignment.center;
                for (int i = 0; i < columsData.Count - 1; i++)
                {
                    docTable.SetColumnWidth(i, 1000);
                    List<string> data = columsData[i];
                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        docTable.Rows[j].Cells[i].Paragraphs[0].Append(data[j]);
                    }
                }
                document.InsertTable(docTable);
            }
            document.Save();
        }
    }
}