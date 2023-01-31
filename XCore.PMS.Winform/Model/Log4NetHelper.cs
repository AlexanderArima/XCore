using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore.PMS.Winform.Model
{
     public partial class Log4NetHelper
    {
        //定义信息的二次处理
        public static event Action<string> OutputMessage;
        //ILog对象
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 定义信息二次处理方式
        private static void HandMessage(object Msg)
        {
            if (OutputMessage != null)
            {
                OutputMessage(Msg.ToString());
            }
        }
        private static void HandMessage(object Msg, Exception ex)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format("{0}:{1}", Msg.ToString(), ex.ToString()));
            }
        }
        private static void HandMessage(string format, params object[] args)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format(format, args));
            }
        }
        #endregion

        #region 封装Log4net
        public static void Debug(object message)
        {
            HandMessage(message);
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public static void Debug(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
        }

        private static void DebugFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsDebugEnabled)
            {
                log.DebugFormat(format, args);
            }
        }

        public static void Error(object message)
        {
            HandMessage(message);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Error(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="message">信息信息</param>
        /// <param name="ex">异常堆栈信息</param>
        /// <param name="url">接口地址</param>
        /// <param name="param">请求参数</param>
        /// <param name="receive_json">响应参数</param>
        public static void Error(object message, Exception ex, string url = "", string param = "", string receive_json = "")
        {
            HandMessage(message, ex);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        
        private static void ErrorFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat(format, args);
            }
        }
        
        public static void Fatal(object message)
        {
            HandMessage(message);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Fatal(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, ex);
            }
        }
        public static void FatalFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsFatalEnabled)
            {
                log.FatalFormat(format, args);
            }
        }
        public static void Info(object message)
        {
            HandMessage(message);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }

        public static void Info(object message, Exception ex, string url = "", string param = "", string receive_json = "")
        {
            HandMessage(message, ex);
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }

        private static void InfoFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsInfoEnabled)
            {
                log.InfoFormat(format, args);
            }
        }

        public static void Warn(object message)
        {
            HandMessage(message);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        public static void Warn(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }

        public static void WarnFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsWarnEnabled)
            {
                log.WarnFormat(format, args);
            }
        }
        #endregion
    }
}