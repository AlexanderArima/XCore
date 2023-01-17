using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI
{
    public class LoginService
    {
        public LoginService()
        {
            Debug.WriteLine("初始化LoginService服务");
        }

        /// <summary>
        /// 验证.
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            return true;
        }
    }
}
