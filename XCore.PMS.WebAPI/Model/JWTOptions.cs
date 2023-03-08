using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.Model
{
    public class JWTOptions
    {
        /// <summary>
        /// 密钥.
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// 过期时间（单位是秒）.
        /// </summary>
        public int ExpireSeconds { get; set; }
    }
}
