using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TDictionary
    {
        public string Id { get; set; }
        public string Typeid { get; set; }
        public string Displayname { get; set; }
        public string Actualname { get; set; }
        public string Sx { get; set; }
        public DateTime Createtime { get; set; }
        public string Deleteflag { get; set; }
    }
}
