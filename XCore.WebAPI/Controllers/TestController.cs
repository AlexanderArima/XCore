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
    }
}
