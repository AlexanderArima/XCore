using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.YZK.依赖注入
{
    public interface ITestService
    {
        public string Name { get; set; }

        public void SayHi();
    }
}
