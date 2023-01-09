using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCore.Web.Models;

namespace XCore.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Demo1()
        {
            Person person = new Person()
            {
                Name = "ZhangSan",
                Age = 26,
                Sex = 1,
                Job = "程序员"
            };

            return View(person);
        }
    }
}
