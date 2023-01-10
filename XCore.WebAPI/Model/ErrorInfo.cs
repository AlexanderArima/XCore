using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class ErrorInfo
    {
        public ErrorInfo(int code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        /// 错误码.
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误信息.
        /// </summary>
        public string msg { get; set; }
    }
}
