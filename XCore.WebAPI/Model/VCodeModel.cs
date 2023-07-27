using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    /// <summary>
    /// VCodeController用到的类变量.
    /// </summary>
    public class VCodeModel
    {
        /// <summary>
        /// 令牌.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///验证码.
        /// </summary>
        public string code { get; set; }
    }

    /// <summary>
    /// VCodeController控制器中Create方法的返回对象.
    /// </summary>
    public class VCodeController_Create_Receive : ReceiveObject
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 令牌.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Base64的验证码图片.
            /// </summary>
            public string img { get; set; }
        }
    }
}
