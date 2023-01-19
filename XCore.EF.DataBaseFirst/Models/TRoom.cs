using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
        public DateTime Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
