﻿using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.PMS.WebAPI.Model_ORM
{
    public partial class TWorker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zjlx { get; set; }
        public string Birthday { get; set; }
        public int Sex { get; set; }
        public string Zjhm { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Quittime { get; set; }
        public string Zjz { get; set; }
        public string Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
