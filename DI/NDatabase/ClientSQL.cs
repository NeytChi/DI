using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Common;

namespace DI.NDatabase
{
    public class ClientSQL
    {
        public MySqlConnection connection;
        private object locker;

        public ClientSQL(MySqlConnection connection, object locker)
        {
            this.connection = connection;
            this.locker = locker;
        }
        public void CreateDatabase(string databaseName)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand("CREATE DATABASE IF NOT EXISTS " + databaseName + ";", connection))
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            Logger.WriteLog("Create table->" + databaseName + ".", LogLevel.Usual);
        }
        public List<string> ShowDatabases()
        {
            List<string> databases = new List<string>();
            lock(locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("show databases;", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            databases.Add(reader.GetString(0));
                        }
                    }
                }
            }
            Logger.WriteLog("Show databases.", LogLevel.Usual);
            return databases;
        }
        public List<string> ShowTables()
        {
            List<string> tables = new List<string>();
            lock (locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("show tables;", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }
                }
            }
            Logger.WriteLog("Show tables.", LogLevel.Usual);
            return tables;
        }
        public List<string> DescribeTable(string table)
        {
            List<string> describe = new List<string>();
            lock (locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("describe " + table + ";", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            describe.Add(reader.GetString(0));
                        }
                    }
                }
            }
            Logger.WriteLog("Describe Current Table.", LogLevel.Usual);
            return describe;
        }
        public Dictionary<string, TypeCode> DescribeTypeTable(string table)
        {
            Dictionary<string, TypeCode> describe = new Dictionary<string, TypeCode>();
            lock (locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("SELECT column_name, data_type" +
                " from information_schema.columns" +
                " where table_schema = 'repair_work'" +
                " AND table_name = 'repair_table';", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            switch (reader.GetString(1))
                            {
                                case "varchar":
                                    describe.Add(reader.GetString(0), TypeCode.String);
                                    break;
                                case "int":
                                    describe.Add(reader.GetString(0), TypeCode.Int32);
                                    break;
                                case "text":
                                    describe.Add(reader.GetString(0), TypeCode.String);
                                    break;
                                case "long":
                                    describe.Add(reader.GetString(0), TypeCode.Int64);
                                    break;
                            }
                        }
                    }
                }
            }
            Logger.WriteLog("Describe type of table.", LogLevel.Usual);
            return describe;
        }
        public List<string> SelectColumTable(string table, string colum, int from)
        {
            int range_from = from * 20;
            List<string> values = new List<string>();
            lock (locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("SELECT " + colum + " FROM " + table + ";", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        for (int i = 0; i < range_from; i++)
                        {
                            if (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                {
                                    values.Add("");
                                }
                                else
                                {
                                    values.Add(reader.GetString(0));
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            Logger.WriteLog("Select " + colum + " from Table ->" + table + ".", LogLevel.Usual);
            return values;
        }
        public List<string> SelectColumTable(string table, string colum)
        {
            List<string> values = new List<string>();
            lock (locker)
            {
                using (MySqlCommand sqlCommand = new MySqlCommand("SELECT " + colum + " FROM " + table + ";", connection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                values.Add("NULL");
                            }
                            else
                            {
                                values.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            Logger.WriteLog("Select " + colum + " from Table ->" + table + ".", LogLevel.Usual);
            return values;
        }
        public void CreateUser(string userName, string userPassword)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand("CREATE USER '" + userName + "'@'localhost' " +
                " IDENTIFIED BY '" + userPassword +  "';", connection))
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            using (MySqlCommand sqlCommand = new MySqlCommand("GRANT ALL PRIVILEGES ON * . * TO '" + userName + "'@'localhost';", connection))
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            Logger.WriteLog("Create user, user_name->" + userName + ".", LogLevel.Usual);
        }
        public void InsertValueToTable(List<dynamic> cells, List<string> describes, string table)
        {
            string request = null;
            string values = null;
            string insertinto = null;
            lock (locker)
            {
                for (int i = 0; i < describes.Count; i++)
                {
                    if (insertinto == null)
                    {
                        insertinto = "INSERT INTO " + table + " (";
                        insertinto += describes[i];
                    }
                    else
                    {
                        insertinto += ", " + describes[i];
                    }
                }
                for (int i = 0; i < cells.Count; i++)
                {
                    if (values == null)
                    {
                        values = ") VALUES ( ";
                        values += "@param" + i.ToString();
                    }
                    else
                    {
                        values += ", @param" + i.ToString();
                    }
                }
                request = insertinto + values + ");";
                using (MySqlCommand sqlCommand = new MySqlCommand(request, connection))
                {
                    for (int i = 0; i < cells.Count; i++)
                    {
                        sqlCommand.Parameters.AddWithValue("@param" + i, cells[i]);
                    }
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                }
            }
        }
        public void DeleteCell(List<string> values, List<string> describes, string table)
        {
            string valuesWhere = null;
            string delete = "DELETE FROM " + table + " WHERE ";
            lock (locker)
            {
                for (int i = 0; i < describes.Count; i++)
                {
                    if (valuesWhere == null)
                    {
                        valuesWhere = describes[i] + "=@param" + i.ToString();
                    }
                    else
                    {
                        valuesWhere += " AND " + describes[i] + "=@param" + i.ToString();
                    }
                }
                delete += valuesWhere + ";";
                using (MySqlCommand sqlCommand = new MySqlCommand(delete, connection))
                {
                    for (int i = 0; i < describes.Count; i++)
                    {
                        sqlCommand.Parameters.AddWithValue("@param" + i.ToString(), values[i]);
                    }
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                }
            }
            Logger.WriteLog("Delete record from table->" + table, LogLevel.Usual);
        }
    }
}



























