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
        public List<string> DescribeCurrentTable(string table)
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
        public Dictionary<string, Type> DescribeTypeTable(string table)
        {
            Dictionary<string,Type> describe = new Dictionary<string, Type>();
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
                            describe.Add(reader.GetString(0), reader.GetFieldType(1));
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
                            values.Add(reader.GetString(0));
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
            using (MySqlCommand sqlCommand = new MySqlCommand("GRANT ALL PRIVILEGES ON * . * TO '" + userName + "'@'localhost';"
            , connection))
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            Logger.WriteLog("Create user, user_name->" + userName + ".", LogLevel.Usual);
        }
    }
}
