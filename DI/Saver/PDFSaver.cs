using Common;
using System;
using System.IO;
using DI.NDatabase;
using iTextSharp.text;
using Common.NDatabase;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace DI.Saver
{
    public class PDFSaver : ISaverDatabase
    {
        ClientSQL clientSQL;

        public PDFSaver(ClientSQL clientSQL)
        {
            this.clientSQL = clientSQL;
        }
        public void SaveDatabase(string database)
        {
            if (database == null)
            {
                Logger.WriteLog("Null insert parameter, function SaveDatabase, class PDFSaver.", LogLevel.Usual);
                return;
            }
            Document doc = new Document();
            string path = Directory.GetCurrentDirectory() + "/Saves/PDF/";
            Directory.CreateDirectory(path);
            PdfWriter.GetInstance(doc, new FileStream( path + DateTime.Now.ToString(), FileMode.Create));
            doc.Open();
            List<string> tables = ClientConnection.client.ShowTables();
            for (int i = 0; i < tables.Count; i++)
            {
                List<string> describes = ClientConnection.client.DescribeTable(tables[i]);
                PdfPTable table = new PdfPTable(describes.Count);
                PdfPCell cell = new PdfPCell(new Phrase("Database " + database + ", table №" + i));
                cell.Colspan = describes.Count;
                cell.HorizontalAlignment = 1;
                cell.Border = 0;
                table.AddCell(cell);
                for (int j = 0; j < describes.Count; j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(describes[j])));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
                List<List<string>> colums = new List<List<string>>();
                for (int j = 0; j < describes.Count; j++)
                {
                    colums.Add(ClientConnection.client.SelectColumTable(tables[i], describes[j]));
                }
                colums = ReSetTableCells(colums);
                foreach(List<string> rows in colums)
                {
                    foreach(string record in rows)
                    {
                        table.AddCell(new Phrase(record));
                    }
                }
                doc.Add(table);
            }
            doc.Close();
            Logger.WriteLog("Save PDF database->" + database, LogLevel.Usual);
        }
        private List<List<string>> ReSetTableCells(List<List<string>> colums)
        {
            int colums_length = colums[0].Count;
            List<List<string>> output = new List<List<string>>();
            for (int i = 0; i < colums_length; i++)
            {
                List<string> obj = new List<string>();
                foreach (List<string> colum in colums)
                {
                    obj.Add(colum[i]);
                }
                output.Add(obj);
            }
            Logger.WriteLog("Reset Table cells", LogLevel.Usual);
            return output;
        }
    }
}
