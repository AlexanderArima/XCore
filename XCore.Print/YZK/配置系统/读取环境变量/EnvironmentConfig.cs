using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统.读取环境变量
{
    class EnvironmentConfig
    {
        public static void MainFunc()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddEnvironmentVariables("CLASS");  // 第一个参数起到了筛选前缀的作用
            var root = configurationBuilder.Build();
            string name = root["PATH"];
            Console.WriteLine($"name：{name}");  // 这种方式会有延迟

            // EnvironmentVariableTarget.Machine方法获取系统的环境变量（不需要管理员权限也能运行）
            // EnvironmentVariableTarget.User获取当前用户的
            // EnvironmentVariableTarget.Process读取时会有延迟，请重启电脑后再试
            var test_name = Environment.GetEnvironmentVariable("TEST_NAME", EnvironmentVariableTarget.Process);
            Console.WriteLine($"test_name：{test_name}");
        }
    }
}
