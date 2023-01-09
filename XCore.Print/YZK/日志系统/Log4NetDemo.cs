using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XCore.Print.YZK.日志系统
{
    class Log4NetDemo
    {
        /// <summary>
        /// 控制台输出日志
        /// </summary>
        public static void MainFunc()
        {
            ILoggerRepository repository = LogManager.CreateRepository("MyRepository");
            BasicConfigurator.Configure(repository);
            ILog log = LogManager.GetLogger(repository.Name, "MyLog");
            log.Info("普通信息");
            log.Warn("警告信息");
            log.Error("错误信息");
            Console.ReadKey();
        }

        public static void MainFunc02()
        {
            ILoggerRepository repository = LogManager.CreateRepository("MyRepository"); // 创建一个日志仓库
            XmlConfigurator.Configure(repository, new FileInfo("YZK/日志系统/config.xml")); // 注册，读取配置文件
            ILog log = LogManager.GetLogger(repository.Name, "MyLog");  // 获得服务
            log.Info("普通信息");   // 输出日志
            log.Warn("警告信息");
            log.Error("错误信息");
            Console.WriteLine("输入日志完毕");
            Console.ReadKey();
        }
    }
}
