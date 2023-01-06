using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.配置系统
{
    class MainClass
    {
        public static void DynamicReadConfig()
        {

        }

        public static void ReadConfig()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();

            // AddJsonFile的第二个参数为true时，如果配置文件不存在程序会报错，为false时会报错
            // 第三个参数表示配置文件被修改后是否会重新加载配置
            builder.AddJsonFile("YZK\\配置系统\\读取字符串\\config.json", false, false);
            IConfigurationRoot root = builder.Build();  // IConfigurationRoot用来读取配置项
            string name = root["name"];
            Console.WriteLine("name：" + name);

            // 访问更深层次的节点
            string address = root.GetSection("proxy:address").Value;
            Console.WriteLine("address：" + address);
        }
    }
}
