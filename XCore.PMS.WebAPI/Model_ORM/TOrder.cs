using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.PMS.WebAPI.Model_ORM
{
    public partial class TOrder
    {
        public int Id { get; set; }
        public string Roomid { get; set; }
        public string Xm { get; set; }
        public string Ywx { get; set; }
        public string Ywm { get; set; }
        public string Zjhm { get; set; }
        public string Birthday { get; set; }
        public int Sex { get; set; }
        public string Zjlx { get; set; }
        public string Qzlx { get; set; }
        public string Address { get; set; }
        public string Appointtime { get; set; }
        public string Checkintime { get; set; }
        public string Checkouttime { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Zjz { get; set; }
        public string Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
