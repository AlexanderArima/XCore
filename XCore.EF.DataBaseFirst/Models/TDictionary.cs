using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TDictionary
    {
        public int Id { get; set; }
        public string Typeid { get; set; }
        public string Displayname { get; set; }
        public string Actualname { get; set; }
        public string Sx { get; set; }
        public DateTime Createtime { get; set; }
        public string Deleteflag { get; set; }
    }
}
