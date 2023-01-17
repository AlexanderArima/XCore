using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TWorker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Zjlx { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string Zjhm { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? Quittime { get; set; }
        public string Zjz { get; set; }
        public DateTime Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
