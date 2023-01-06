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
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<TestServiceImpl>();
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
