using System;
using XCore.EF.DataBaseFirst.Models;

namespace XCore.EF.DataBaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestHelper.Insert("测试名");
            // TestHelper.Delete(1);
            // TestHelper.Update(2, "修改后的名字");
            // TestHelper.QueryByID(2);
            TestHelper.QueryCount();
            Console.WriteLine("Hello World!");
        }
    }
}
