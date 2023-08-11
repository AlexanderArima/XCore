using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class VCodeEmailModel
    {
        /// <summary>
        /// 令牌.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 验证码.
        /// </summary>
        public string code { get; set; }
    }
}
