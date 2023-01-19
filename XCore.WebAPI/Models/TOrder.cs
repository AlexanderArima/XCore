using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.WebAPI.Models
{
    public partial class TOrder
    {
        public int Id { get; set; }
        public string Roomid { get; set; }
        public string Xm { get; set; }
        public string Ywx { get; set; }
        public string Ywm { get; set; }
        public string Zjhm { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string Zjlx { get; set; }
        public string Qzlx { get; set; }
        public string Address { get; set; }
        public DateTime? Appointtime { get; set; }
        public DateTime? Checkintime { get; set; }
        public DateTime? Checkouttime { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Zjz { get; set; }
        public DateTime Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
