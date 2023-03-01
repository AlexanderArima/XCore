using System;
using System.Collections.Generic;

#nullable disable

namespace XCore.EF.DataBaseFirst.Model_ORM
{
    public partial class TUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Workerid { get; set; }
        public string Createtime { get; set; }
        public int Deleteflag { get; set; }
    }
}
