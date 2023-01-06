using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统.读取命令行配置
{
    class CommandConfig
    {
        public static void MainFunc(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddCommandLine(args);
            var config = configurationBuilder.Build();
            string server = config["server"];
            string port = config["port"];
            Console.WriteLine($"server：{server}，port：{port}");

            // 要求输入参数格式：server=127.0.0.1
        }
    }
}
