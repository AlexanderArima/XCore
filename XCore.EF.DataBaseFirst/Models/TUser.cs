using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.EF.DataBaseFirst.Models
{
    public partial class TUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Workerid { get; set; }
        public DateTime Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
