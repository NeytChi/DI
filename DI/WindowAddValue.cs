using Gtk;
using Common;
using System;
using Common.NDatabase;
using System.Diagnostics;
using System.Collections.Generic;

namespace DI
{
    public partial class WindowAddValue : Gtk.Window
    {
        public WindowTable windowTable;
        public List<Label> labels = new List<Label>();
        public List<Entry> entries = new List<Entry>();
        WindowTable table;
        public string CurrentTable;

        public WindowAddValue(WindowTable windowTable, string CurrentTable, WindowTable table)
         : base(Gtk.WindowType.Toplevel)
        {
            this.table = table;
            this.CurrentTable = CurrentTable;
            List<string> describes = ClientConnection.client.DescribeTable(CurrentTable);
            this.windowTable = windowTable;
            this.Build();
            ControlForm();
            EditForm(describes);
            Logger.WriteLog("Init add value window.", LogLevel.Usual);
        }
        private void EditForm(List<string> describes)
        {
            for (int i = 0; i < describes.Count; i++)
            {
                labels[i].Text = describes[i];
            }
            for(int i = labels.Count; i > describes.Count; i--)
            {
                labels[i].Hide();
                labels[i].Dispose();
                entries[i].Hide();
                entries[i].Dispose();
            }
        }
        protected void OnButton1Pressed(object sender, EventArgs e)
        {
            int index = 0;
            bool exit = true;
            List<string> describes = ClientConnection.client.DescribeTable(CurrentTable);
            List<dynamic> record = new List<dynamic>();
            Dictionary<string, TypeCode> typeColumns = ClientConnection.client.DescribeTypeTable(CurrentTable);
            foreach (KeyValuePair<string, TypeCode> column in typeColumns)
            {
                string entryText = entries[index].Text;
                ++index;
                dynamic value = null;
                if (ConvertToTypeValue(entryText, ref value, column.Value))
                {
                    record.Add(value);
                }
                else
                {
                    ErrorInsertEntryValue(column.Key);
                    exit = false;
                }
            }
            if (exit)
            {
                ClientConnection.client.InsertValueToTable(record, describes, CurrentTable);
                Hide();
                table.SelectRowColums();
                Logger.WriteLog("Insert record to table->" + CurrentTable, LogLevel.Usual);
                Dispose();
            }
        }
        public void ErrorInsertEntryValue(string errorEntry)
        {
            label11.Text = "False insert value. Column->" + errorEntry + ".";
            Logger.WriteLog("False insert value. Column->" + errorEntry + ".", LogLevel.Warning);
        }
        private bool ConvertToTypeValue(string text, ref dynamic value, TypeCode type)
        {
            switch (type)
            {
                case TypeCode.String:
                    value = text;
                    return true;
                case TypeCode.Int32:
                    int x = 0;
                    if (Int32.TryParse(text, out x))
                    {
                        value = x;
                        return true;
                    }
                    else
                    {
                        Logger.WriteLog("Error with convert string->int.", LogLevel.Warning);
                        return false;
                    }
                case TypeCode.Int64:
                    long y = 0;
                    if (Int64.TryParse(text, out y))
                    {
                        value = y;
                        return true;
                    }
                    else
                    {
                        Logger.WriteLog("Error with convert string->long.", LogLevel.Warning);
                        return false;
                    }
                default: return false;
            }
        }
        private void ControlForm()
        {
            labels.Add(label1);
            entries.Add(entry1);
            labels.Add(label2);
            entries.Add(entry2);
            labels.Add(label3);
            entries.Add(entry3);
            labels.Add(label4);
            entries.Add(entry4);
            labels.Add(label5);
            entries.Add(entry5);
            labels.Add(label6);
            entries.Add(entry6);
            labels.Add(label7);
            entries.Add(entry7);
            labels.Add(label8);
            entries.Add(entry8);
            labels.Add(label9);
            entries.Add(entry9);
            labels.Add(label10);
            entries.Add(entry10);
        }
    }
}
