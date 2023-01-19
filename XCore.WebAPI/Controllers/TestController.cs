using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly MyService myService;

        public TestController(MyService service)
        {
            // 构造函数，依赖注入，完成实例化
            this.myService = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return myService.GetName();
        }

        [HttpGet]
        public ActionResult<string> Login([FromServices]LoginService loginService)
        {
            var result = loginService.Valid();
            return result == true ? "验证通过" : "验证不通过";
        }

        [HttpGet]
        [Authorize(Roles = "NormalUser")]
        public ActionResult<string> Query()
        {
            return "NormalUser123";
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> QueryAdmin()
        {
            return "Admin123";
        }
    }
}
