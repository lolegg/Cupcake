using Cupcake.Core.Domain;
using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Cupcake.Core.Log4
{
    public class Log4Helper
    {
        public static void Initialize(string configFilePath)
        {
            var fileInfo = new FileInfo(configFilePath);
            if (!fileInfo.Exists)
            {
                throw new NopException("未找到配置文件:" + configFilePath);
            }
            XmlConfigurator.ConfigureAndWatch(fileInfo);
        }

        public static void Error(Type type, Exception exception, Guid? creatorId = null)
        {
            WriteLog(LogType.Error, type, exception, creatorId);
        }

        private static void WriteLog(LogType logType, Type type, Exception exception, Guid? creatorId = null)
        {
            var log = LogManager.GetLogger(type);
            log4net.LogicalThreadContext.Properties["Ip"] = IpHelper.IP;
            log4net.LogicalThreadContext.Properties["creator"] = creatorId;
            log4net.LogicalThreadContext.Properties["stackTrace"] = HttpUtility.UrlEncode(exception.StackTrace);
            switch (logType)
            {
                case LogType.Debug:
                    log.Debug(exception.Message, exception);
                    break;
                case LogType.Info:
                    log.Info(exception.Message, exception);
                    break;
                case LogType.Warn:
                    log.Warn(exception.Message, exception);
                    break;
                case LogType.Error:
                    try
                    {
log.Error(exception.Message, exception);
                    }
                    catch (Exception exc)
                    {
                        
                        throw;
                    }
                    
                    break;
                case LogType.Fatal:
                    log.Fatal(exception.Message, exception);
                    break;
            }
        }
        private static void Error(string ip, string Operator, Type t, string msg, string errInfo = "")
        {
            var message = string.Empty;
            ILog log = LogManager.GetLogger(t);
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            if (string.IsNullOrEmpty(errInfo))
                message = string.Format("方法名称：{0}\r\n日志内容：{1}", methodBase.Name, msg);
            else
                message = string.Format("方法名称：{0}\r\n日志内容：{1}\r\n异常详情：{2}", methodBase.Name, msg, errInfo);
            log4net.LogicalThreadContext.Properties["Ip"] = ip;
            log4net.LogicalThreadContext.Properties["Operand"] = Operator;
            log.Error(message);
        }
        private static void Info(string ip, string Operator, Type t, string msg, string errInfo = "")
        {
            var message = string.Empty;
            var log = LogManager.GetLogger(t);
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            if (string.IsNullOrEmpty(errInfo))
                message = string.Format("方法名称：{0}\r\n日志内容：{1}", methodBase.Name, msg);
            else
                message = string.Format("方法名称：{0}\r\n日志内容：{1}\r\n异常详情：{2}", methodBase.Name, msg, errInfo);
            log4net.LogicalThreadContext.Properties["IP"] = ip;
            log4net.LogicalThreadContext.Properties["Operand"] = Operator;
            log.Info(message);
        }
        public static void Info(Type type, Exception exception, Guid creatorId)
        {
            WriteLog(LogType.Info, type, exception, creatorId);
        }
        private static void Fatal(string ip, string Operator, Type t, string msg, string errInfo = "")
        {
            var message = string.Empty;
            var log = LogManager.GetLogger(t);
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            if (string.IsNullOrEmpty(errInfo))
                message = string.Format("方法名称：{0}\r\n日志内容：{1}", methodBase.Name, msg);
            else
                message = string.Format("方法名称：{0}\r\n日志内容：{1}\r\n异常详情：{2}", methodBase.Name, msg, errInfo);
            log4net.LogicalThreadContext.Properties["IP"] = ip;
            log4net.LogicalThreadContext.Properties["Operand"] = Operator;
            log.Fatal(message);
        }
        public static void Fatal(Type type, Exception exception, Guid creatorId)
        {
            WriteLog(LogType.Fatal, type, exception, creatorId);
        }
        private static void Debug(string ip, string Operator, Type t, string msg, string errInfo = "")
        {
            var message = string.Empty;
            var log = LogManager.GetLogger(t);
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            if (string.IsNullOrEmpty(errInfo))
                message = string.Format("方法名称：{0}\r\n日志内容：{1}", methodBase.Name, msg);
            else
                message = string.Format("方法名称：{0}\r\n日志内容：{1}\r\n异常详情：{2}", methodBase.Name, msg, errInfo);
            log4net.LogicalThreadContext.Properties["IP"] = ip;
            log4net.LogicalThreadContext.Properties["Operand"] = Operator;
            log.Debug(message);
        }
        public static void Debug(Type type, Exception exception, Guid creatorId)
        {
            WriteLog(LogType.Debug, type, exception, creatorId);
        }
        private static void Warn(string ip, string Operator, Type t, string msg, string errInfo = "")
        {
            var message = string.Empty;
            var log = LogManager.GetLogger(t);
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            if (string.IsNullOrEmpty(errInfo))
                message = string.Format("方法名称：{0}\r\n日志内容：{1}", methodBase.Name, msg);
            else
                message = string.Format("方法名称：{0}\r\n日志内容：{1}\r\n异常详情：{2}", methodBase.Name, msg, errInfo);
            log4net.LogicalThreadContext.Properties["IP"] = ip;
            log4net.LogicalThreadContext.Properties["Operand"] = Operator;
            log.Warn(message);
        }
        public static void Warn(Type type, Exception exception, Guid creatorId)
        {
            WriteLog(LogType.Warn, type, exception, creatorId);
        }
    }
}
