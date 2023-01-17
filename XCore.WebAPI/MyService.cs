using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI
{
    /// <summary>
    /// 一个自定义服务
    /// </summary>
    public class MyService
    {
        public MyService()
        {
            Debug.WriteLine("初始化MyService服务");
        }

        public string GetName()
        {
            return "Jack";
        }
    }
}
