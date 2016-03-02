using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using Vulcan.AspNetMvc.Interfaces;

namespace SampleWebApp.Models.Service
{
    public class LogService:ILogService
    {
        // 实际项目中，可以使用NLog 或者Log4N 来实现该接口
        public void Debug(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "debug.txt");
            info.Message = msg;
            AddLog(info);
        }

        public void Trace(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "trace.txt");
            info.Message = msg;
            AddLog(info);
        }

        public void Info(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "info.txt");
            info.Message = msg;
            AddLog(info);
        }

        public void Warn(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "warn.txt");
            info.Message = msg;
            AddLog(info);
        }

        public void Error(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "error.txt");
            info.Message = msg;
            AddLog(info);
        }

        public void Fatal(string msg)
        {
            LogInfo info = new LogInfo();
            info.FilePath = string.Format("{0}{1}/", basePath, DateTime.Today.ToString("yyyyMMdd"));
            info.FileName = string.Format("{0}/{1}", info.FilePath, "fatal.txt");
            info.Message = msg;
            AddLog(info);
        }


        private static Queue<LogInfo> logDict = new Queue<LogInfo>();
        private static object lockobject = new object();
        private static Dictionary<string, StreamWriter> fileWrite = new Dictionary<string, StreamWriter>();
        private static object lockdict = new object();
        private static bool _isruning = false;

        private static string basePath = System.AppDomain.CurrentDomain.BaseDirectory + "/logs/";
     
        private static void AddLog(LogInfo loginfo)
        {
            loginfo.LogTime = DateTime.Now;
            lock (lockobject)
            {
                logDict.Enqueue(loginfo);
            }
            StartWrite();
        }

        private static void StartWrite()
        {
            if (_isruning)
            {
                return;
            }
            _isruning = true;
            Thread thread = new Thread(new ThreadStart(WriteLog));
            thread.IsBackground = true;
            thread.Start();
        }

        private static void WriteLog()
        {
            while (logDict.Count > 0)
            {
                LogInfo log = logDict.Dequeue();
                try
                {
                    if (!Directory.Exists(log.FilePath))
                    {
                        Directory.CreateDirectory(log.FilePath);
                    }
                    using (StreamWriter sw = new StreamWriter(log.FileName, true, Encoding.UTF8))
                    {
                        sw.WriteLine("{0:yyyy-MM-dd HH:mm:ss}:{1}", log.LogTime, log.Message);
                        sw.Flush();
                    }
                }
                catch
                {
                    //写文件失败了哦，那我还能干啥呢
                }
            }
            _isruning = false;
        }
    }

    public class LogInfo
    {
        public string FilePath
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public DateTime LogTime { get; set; }
    }
}