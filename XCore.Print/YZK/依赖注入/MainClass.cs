using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.依赖注入
{
    class MainClass
    {
        public static void MainFun()
        {
            ServiceCollection services = new ServiceCollection();   // ServiceCollection是注册容器的地址
            services.AddTransient<TestServiceImpl>();   // 想要设置一个【瞬态】的服务
            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                var testService = provider.GetRequiredService<TestServiceImpl>();
                testService.Name = "Tom";
                testService.SayHi();
            }

            Console.ReadKey();
        }
    }
}
