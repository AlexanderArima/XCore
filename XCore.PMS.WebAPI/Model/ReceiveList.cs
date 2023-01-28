using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.Model
{
    public class ReceiveList<T>
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
        public Data data { get; set; }

        public class Data
        {
            public int index { get; set; }

            public int count { get; set; }

            public List<T> list { get; set; }
        }
    }
}
