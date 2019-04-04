﻿using Gtk;
using System;
using Common;
using Common.NDatabase;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DI.Saver;

namespace DI
{
    public partial class WindowTable : Gtk.Window
    {
        public List<string> databases;
        public List<string> tables;
        public List<TreeIter> ItersDatabase = new List<TreeIter>();
        public List<TreeIter> ItersTable = new List<TreeIter>();
        public string SelectedDatabase;
        private TreeStore databaseListStore;
        public string CurrentTable;
        public TreeView databaseTree;
        public List<TreeViewColumn> cellsTreeView = new List<TreeViewColumn>();

        public WindowTable() : base(WindowType.Toplevel)
        {
            this.Build();
            InitTreeView();
            Logger.WriteLog("Init window table.", LogLevel.Usual);
        }
        public void InitTreeView()
        {
            if (databaseTree != null)
            {
                Remove(databaseTree);
            }
            databaseTree = new TreeView();
            Add(databaseTree);
            TreeViewColumn databaseColumn = new TreeViewColumn();
            databaseColumn.Title = "Database";
            treeview1.AppendColumn(databaseColumn);
            databaseListStore = new TreeStore(typeof(string));
            treeview1.Model = databaseListStore;
            databases = ClientConnection.client.ShowDatabases();
            foreach (string database in databases)
            {
                TreeIter iter = databaseListStore.AppendValues(database);
                ItersDatabase.Add(iter);
            }
            CellRendererText databaseNameCell = new CellRendererText();
            databaseColumn.PackStart(databaseNameCell, true);
            databaseColumn.AddAttribute(databaseNameCell, "text", 0);
            Logger.WriteLog("Init tree view.", LogLevel.Usual);
        }
        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
            Logger.WriteLog("Quit from window table.", LogLevel.Usual);
            MainWindow win = new MainWindow();
            Dispose();
        }
        protected void OnTreeview1RowActivated(object o, RowActivatedArgs args)
        {
            Regex regex = new Regex(@"\d+:\d+");
            Match match = regex.Match(args.Path.ToString());
            if (match.Success)
            {
                HandleTables(match.Value);
            }
            else
            {
                EditTables(args.Path.ToString());
            }
        }
        private void HandleTables(string path)
        {
            string value = "";
            int indexTable = -1;
            int startIndex = path.IndexOf(':');
            if (startIndex != -1)
            {
                ++startIndex;
                value = path.Substring(startIndex);
                indexTable = Convert.ToInt32(value);
                CurrentTable = tables[indexTable];
                SelectRowColums();
            }
        }
        private void SelectRowColums()
        {
            int index = 0;
            foreach(TreeViewColumn treeViewColumn in cellsTreeView)
            {
                treeview2.RemoveColumn(treeViewColumn);
            }
            List<string> describes = ClientConnection.client.DescribeCurrentTable(CurrentTable);
            List<List<string>> colums = new List<List<string>>();
            TreeStore listStore = new TreeStore
            (
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string)            
            );
            treeview2.Model = listStore;
            foreach (string describe in describes)
            {
                colums.Add(ClientConnection.client.SelectColumTable(CurrentTable, describe, 0));
            }
            colums = ReSetTableCells(colums);
            foreach (List<string> colum in colums)
            {
                listStore.AppendValues
                (
                    colum[0], colum[1], colum[2], colum[3], colum[4], 
                    colum[5], colum[6], colum[7], colum[8], colum[9]
                );
            }
            foreach (string describe in describes)
            {
                TreeViewColumn column = new TreeViewColumn();
                column.Title = describe;
                CellRendererText NameCell = new CellRendererText();
                column.PackStart(NameCell, true);
                column.AddAttribute(NameCell, "text", index);
                treeview2.AppendColumn(column);
                cellsTreeView.Add(column);
                ++index;
            }
        }
        private List<List<string>> ReSetTableCells(List<List<string>> colums)
        {
            int colums_length = colums[0].Count;
            List<List<string>> output = new List<List<string>>();
            for (int i = 0; i < colums_length; i++)
            {
                List<string> obj = new List<string>();
                foreach(List<string> colum in colums)
                {
                    obj.Add(colum[i]);
                }
                output.Add(obj);
            }
            Logger.WriteLog("Reset Table cells", LogLevel.Usual);
            return output;
        }
        private void EditTables(string path)
        {
            if (ItersTable != null)
            {
                foreach (TreeIter table in ItersTable)
                {
                    TreeIter ref_table = table;
                    if (databaseListStore.IterIsValid(table))
                    {
                        databaseListStore.Remove(ref ref_table);
                    }
                }
            }
            else
            {
                ItersTable = new List<TreeIter>();
            }
            int indexIter = Convert.ToInt32(path);
            TreeIter IterDatabase = ItersDatabase[indexIter];
            SelectedDatabase = databases[indexIter];
            ClientConnection.UseDatabase(SelectedDatabase);
            tables = ClientConnection.client.ShowTables();
            foreach (string table in tables)
            {
                TreeIter tableIter = databaseListStore.AppendValues(IterDatabase, table);
                ItersTable.Add(tableIter);
            }
            treeview1.Model = databaseListStore;
        }
        protected void OnButton1Pressed(object sender, EventArgs e)
        {
            WindowCreateDatabase createDatabase = new WindowCreateDatabase();
            createDatabase.windowTable = this;
            Logger.WriteLog("Init window create database table.", LogLevel.Usual);
        }
        protected void OnButton2Pressed(object sender, EventArgs e)
        {
            List<string> values = new List<string>();
            List<string> describes = ClientConnection.client.DescribeCurrentTable(CurrentTable);
            WindowAddValue windowValue = new WindowAddValue(this, describes);
            Logger.WriteLog("Init windows create value table.", LogLevel.Usual);
        }
        protected void OnButton4Pressed(object sender, EventArgs e)
        {
            WCreateUser createUser = new WCreateUser();
            Logger.WriteLog("Init window create user.", LogLevel.Usual);
        }
        protected void OnButton6Pressed(object sender, EventArgs e)
        {
            ISaverDatabase saver = new WordSaver(ClientConnection.client);
            saver.SaveDatabase(SelectedDatabase);
            Logger.WriteLog("Save database as Word format.", LogLevel.Usual);
        }
        protected void OnButton5Pressed(object sender, EventArgs e)
        {
            Logger.WriteLog("Save database as Excel format.", LogLevel.Usual);
        }
        protected void OnButton7Pressed(object sender, EventArgs e)
        {
            Logger.WriteLog("Save database as PDF format.", LogLevel.Usual);
        }
    }
}