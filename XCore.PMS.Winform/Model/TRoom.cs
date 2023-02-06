using System;
using System.Collections.Generic;

namespace XXCore.PMS.Winform.Model
{
    public partial class TRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
        public string Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
