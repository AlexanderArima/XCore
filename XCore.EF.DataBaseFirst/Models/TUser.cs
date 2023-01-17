using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Workerid { get; set; }
        public DateTime Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
