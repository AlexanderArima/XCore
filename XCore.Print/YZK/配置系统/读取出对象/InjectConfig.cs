using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统.读取出对象
{
    class InjectConfig
    {
        public static void ReadConfig()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("YZK\\配置系统\\读取出对象\\appsettings.json", false, true);
            var config = configurationBuilder.Build();
            ServiceCollection services = new ServiceCollection();

            // Configure的参数是Action<TOptions>，它的第一个参数是TOptions的匿名委托，Bind方法将加载的配置从字符串绑定到对象中去
            services.AddOptions().Configure<DBSetting>(e => config.GetSection("DB").Bind(e)).Configure<SmtpSetting>(e => config.GetSection("Smtp").Bind(e));
            services.AddTransient<UseConfig>();
            using(var sp = services.BuildServiceProvider())
            {
                while (true)
                {
                    using (var scope = sp.CreateScope())
                    {
                        var spScope = scope.ServiceProvider;
                        var use_config = spScope.GetRequiredService<UseConfig>();
                        use_config.Test();
                    }

                    Console.WriteLine("可以修改配置");
                    Console.ReadKey();
                }
            }

            //  DBSetting setting = new DBSetting();
            // config.GetSection("DB").Bind(setting);
            // Console.WriteLine($"DB：{setting.DbType}，{setting.ConnectionString}");
        }
    }
}
