using Gtk;
using Common;
using System;
using Common.NDatabase;
using System.Collections.Generic;
using System.Diagnostics;

namespace DI
{
    public partial class WindowAddValue : Gtk.Window
    {
        public WindowTable windowTable;
        public List<Label> labels = new List<Label>();
        public List<Entry> entries = new List<Entry>();
        public List<string> describes;
        public string CurrentTable;

        public WindowAddValue(WindowTable windowTable, string CurrentTable)
         : base(Gtk.WindowType.Toplevel)
        {
            this.describes = ClientConnection.client.DescribeCurrentTable(CurrentTable);
            this.windowTable = windowTable;
            this.Build();
            ControlForm();
            EditForm();
            Logger.WriteLog("Init add value window.", LogLevel.Usual);
        }
        private void EditForm()
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
            Dictionary<string, Type> typeColumns = ClientConnection.client.DescribeTypeTable(CurrentTable);
            foreach(KeyValuePair<string,Type> keyValuePair in typeColumns)
            {

            }
            Hide();
            Dispose();
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
