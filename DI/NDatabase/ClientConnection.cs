using System;
using System.Data;
using DI.NDatabase;
using MySql.Data.MySqlClient;

namespace Common.NDatabase
{
    public static class ClientConnection
    {
        public static ClientSQL client;
        public static object locker = new object();
        public static MySqlConnectionStringBuilder connectionstring = new MySqlConnectionStringBuilder();
        public static MySqlConnection connection;

        public static bool Authorization(string UserName, string Password)
        {
            SetAuthConnection(UserName, Password);
            connection = new MySqlConnection(connectionstring.ToString());
            try
            {
                connection.Open();
                client = new ClientSQL(connection, locker);
                Logger.WriteLog("Open MySQL connection.", LogLevel.Usual);
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteLog("Can't authorize user to database. Message->" + e.Message, LogLevel.Warning);
                return false;
            }
        }
        private static void SetAuthConnection(string UserName, string Password)
        {
            connectionstring.SslMode = MySqlSslMode.None;
            connectionstring.ConnectionReset = true;
            connectionstring.Server = "localhost";
            connectionstring.UserID = UserName;
            connectionstring.Password = Password;
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            connection = new MySqlConnection(connectionstring.ToString());
        }
        public static void UseDatabase(string database)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            connectionstring.Database = database;
            connection = new MySqlConnection(connectionstring.ToString());
            connection.Open();
            client.connection = connection;
            Logger.WriteLog("Use database->" + database + ".", LogLevel.Usual);
        }
    }
}
