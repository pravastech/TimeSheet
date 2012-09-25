using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace TimeSheetUtil
{
    public enum LogLevel { Debug, Error, Info, Warning }

    public class Logger
    {
        public bool IsDebugEnabled { get; set; }
        public bool IsWarnEnabled { get; set; }
        public string logFile { get; set; }

        public Logger()
        {
            IsDebugEnabled = false;
        }

        public Logger(string logfile)
        {
            this.logFile = logFile;
            IsDebugEnabled = false;
        }

        private void WriteToFile(string text)
        {
            if (string.IsNullOrEmpty(this.logFile))
            {
                this.logFile = "C:\\MQWinProcess.log";
            }
            if(! File.Exists(this.logFile))
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(this.logFile))
                    {
                        sw.WriteLine(text);
                        sw.Close();
                    }
                }
                catch { };
            }

            try
            {
                using (StreamWriter sw = File.AppendText(this.logFile))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            catch { };
        }

        public void Log(LogLevel loglevel, string logMsg, string sql)
        {
            WriteToFile(string.Format("{0} {1} {2}", DateTime.Now, logMsg, sql));
        }

        public void Debug(string format, object arg0)
        {
            if (IsDebugEnabled)
            {
                WriteToFile(string.Format("{0}" + format, DateTime.Now, arg0));
            }
        }

        public void Debug(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                WriteToFile(string.Format("{0} " + format, DateTime.Now, args));
            }
        }

        public void Error(Exception e)
        {
            WriteToFile(string.Format("{0} {1} ", DateTime.Now, e.Message));
        }

        public void Error(string format, object arg0)
        {
            WriteToFile(string.Format("{1}" + format, DateTime.Now, arg0));
        }

        public void Error(string format, params object[] args)
        {
            WriteToFile(string.Format(format, args));
        }

        public void Warn(string warn)
        {
            if (IsWarnEnabled)
            {
                WriteToFile(string.Format("{0} {1} ", DateTime.Now, warn));
            }
        }

        public void Warn(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                WriteToFile(string.Format(format, args));
            }
        }

        public void Warn(string format, object arg0)
        {
            if (IsWarnEnabled)
            {
                WriteToFile(string.Format("{0} " + format, DateTime.Now, arg0));
            }
        }

    }
}
