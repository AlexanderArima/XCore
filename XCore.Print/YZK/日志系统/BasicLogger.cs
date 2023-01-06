using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.日志系统
{
    public class BasicLogger
    {
        public static void MainFunc()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddLogging(log => { log.AddConsole(); });
            using (var sp = services.BuildServiceProvider())
            {
                var logger =  sp.GetRequiredService<ILogger<BasicLogger>>();  // 置顶日志中显示出错类的名称，一般这段代码位于哪里就些那个地方的类名
                logger.LogInformation("普通信息");
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
