using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.日志系统
{
    public class BasicLogger
    {
        // 输入日志到控制台
        public static void MainFunc()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddLogging(log => { log.AddConsole(); });  // 将日志服务注册到容器中
            using (var sp = services.BuildServiceProvider())
            {
                var logger =  sp.GetRequiredService<ILogger<BasicLogger>>();  // 获得服务
                logger.LogInformation("普通信息");  // 输入日志
                logger.LogWarning("警告信息");
                logger.LogError("错误信息");
                try
                {
                    int age = Convert.ToInt32("aa");
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "类型转换时出错");
                }
            }
        }
    }
}
