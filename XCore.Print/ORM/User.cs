using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.ORM
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string workerid { get; set; }
        public string createtime { get; set; }
        public string deleteflag { get; set; }
    }
}
