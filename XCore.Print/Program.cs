using System;
using XCore.Print.ORM;
using XCore.Print.YZK.依赖注入;
using XCore.Print.YZK.日志系统;
using XCore.Print.YZK.配置系统.读取命令行配置;
using XCore.Print.YZK.配置系统.读取环境变量;

namespace XCore.Print
{
    class Program
    {
        static void Main(string[] args)
        {
            // XCore.Print.YZK.配置系统.读取出对象.InjectConfig.ReadConfig();
            // CommandConfig.MainFunc(args);
            // EnvironmentConfig.MainFunc();
            // BasicLogger.MainFunc();
            // Log4NetDemo.MainFunc02();
            UseMySQL.MainFunc();
            Console.ReadKey();
        }
    }
}
