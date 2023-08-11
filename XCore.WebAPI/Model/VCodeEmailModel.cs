using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class VCodeEmailModel
    {
        /// <summary>
        /// 邮箱地址.
        /// </summary>
        public string emailAddress { get; set; }

        /// <summary>
        /// 验证码.
        /// </summary>
        public string code { get; set; }
    }
}
