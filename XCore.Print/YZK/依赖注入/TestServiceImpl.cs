using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.依赖注入
{
    public class TestServiceImpl : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine("Hi, I'm " + this.Name);
        }
    }
}
