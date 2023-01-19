using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class User : IdentityUser<long>
    {
        public DateTime CreateTime { get; set; }

        public string NickName { get; set; }
    }
}
