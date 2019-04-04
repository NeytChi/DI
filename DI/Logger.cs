using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Common.NDatabase.LogData;

namespace Common
{
    public enum LogLevel { Usual, Warning, Error, Fatal }

    public static class Logger
    {
        public static bool stateLogging = true;
        private static string PathLogsDirectory = "/files/logs/";
        private static string FileName = "log";
        private static string Full_Path_File = "";
        private static string UserName = Environment.UserName;
        private static string MachineName = Environment.MachineName;

        private static FileStream FileWriter;
        private static FileInfo FileLogExist;
        /// <summary>
        /// Logging.
        /// </summary>
        /// <param name="logCmd">Log cmd. Is a recorded log.</param>
        /// <param name="level">This is the type of log level</param>
        public static void WriteLog(string logCmd, LogLevel level)
        {
            if (!string.IsNullOrEmpty(logCmd))
            {
                if (logCmd.Length > 2000)
                {
                    logCmd = logCmd.Substring(0, 2000);
                }
            }
            else
            {
                WriteLog("Insert value is null, function WriteLog()", LogLevel.Error);
                return;
            }
            if (stateLogging == true)
            {
                DateTime localDate = DateTime.Now;
                Log loger = new Log
                {
                    log = logCmd,
                    user_computer = UserName + " " + MachineName,
                    seconds = (short)localDate.Second,
                    minutes = (short)localDate.Minute,
                    hours = (short)localDate.Hour,
                    day = (short)localDate.Day,
                    month = (short)localDate.Month,
                    year = localDate.Year,
                    level = SetLevelLog(level)
                };
                CheckExistLogFile();
                Write(ref loger);
            }
            else
            {
                Debug.WriteLine(logCmd);
            }
        }
        private static void CheckExistLogFile()
        {
            string CurrentDirectory = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(CurrentDirectory + PathLogsDirectory);
            Full_Path_File = CurrentDirectory + PathLogsDirectory + FileName;
            if (FileLogExist == null)
            {
                FileLogExist = new FileInfo(Full_Path_File);
            }
            if (!FileLogExist.Exists)
            {
                FileStream fs = File.Create(Full_Path_File);
                fs.Close();
            }
            if (FileWriter == null)
            {
                FileWriter = new FileStream(Full_Path_File, FileMode.Append, FileAccess.Write);
            }
        }
        /// <summary>
        /// Write log info to txt file.
        /// </summary>
        /// <param name="loger">Loger.</param>
        private static void Write(ref Log loger)
        {
            byte[] array = Encoding.ASCII.GetBytes(
                "T/D: " + loger.hours + ":" + loger.minutes + ":" + loger.seconds + "__"
                + loger.day + ":" + loger.month + ":" + loger.year
                + "; " + "User_comp.: " + loger.user_computer + "; " +
                "Log: " + loger.log + "; Level: " + loger.level + ";" + "\r\n");
            FileWriter.Write(array, 0, array.Length);
            FileWriter.Flush();
            Debug.WriteLine(loger.log);
        }
        private static string SetLevelLog(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Usual: return "usual";
                case LogLevel.Warning: return "warning";
                case LogLevel.Error: return "error";
                case LogLevel.Fatal: return "fatal";
                default: return "indefinite";
            }
        }
        public static void SetUpNewLogFile()
        {
            string answer;
            DateTime time = DateTime.Now;
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string renameFullpath = CurrentDirectory + PathLogsDirectory + time.Month + "." + time.Day + "." + time.Year;
            CheckExistLogFile();
            if (FileWriter != null)
            {
                FileWriter.Close();
            }
            if (Directory.Exists(Path.GetDirectoryName(CurrentDirectory + PathLogsDirectory)))
            {
                if (File.Exists(Full_Path_File))
                {
                    if (!File.Exists(renameFullpath))
                    {
                        File.Move(Full_Path_File, renameFullpath);
                        FileStream creating = File.Create(Full_Path_File);
                        creating.Close();
                        FileWriter = new FileStream(Full_Path_File, FileMode.Append, FileAccess.Write);
                        FileLogExist = new FileInfo(Full_Path_File);
                        WriteLog("Set new file for logs.", LogLevel.Usual);
                        return;
                    }
                    else
                    {
                        answer = "Dont set new file, because has the same another.";
                    }

                }
                else
                {
                    answer = "Log file is not exists.";
                }
            }
            else
            {
                answer = "Dont set new file, because has not create set directory.";
            }
            FileWriter = new FileStream(Full_Path_File, FileMode.Append, FileAccess.Write);
            FileLogExist = new FileInfo(Full_Path_File);
            WriteLog(answer, LogLevel.Error);
        }
        public static void Dispose()
        {
            if (FileWriter != null)
            {
                FileWriter.Flush();
                FileWriter.Close();
            }
        }
    }
}
