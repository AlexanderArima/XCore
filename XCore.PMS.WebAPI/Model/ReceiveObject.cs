using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.Model
{
    public class ReceiveObject<T>
    {
        /// <summary>
        /// 代码，0表示请求成功
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 请求数据，无错误时返回值
        /// </summary>
        public T data { get; set; }
    }
}
