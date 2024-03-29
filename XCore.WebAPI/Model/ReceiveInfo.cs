﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class ReceiveInfo
    {
        public ReceiveInfo(int code,string msg, string data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }

        public int code { get; set; }

        public string msg { get; set; }

        public string data { get; set; }
    }
}
