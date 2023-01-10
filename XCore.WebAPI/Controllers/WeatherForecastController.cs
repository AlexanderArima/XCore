using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XCore.WebAPI.Model;

namespace XCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Update(string title, string context)
        {
            return "修改完成，具体信息：" + this.SetModel(title, context);
        }

        /// <summary>
        /// 为了避免打开swagger时由于方法未被[HttpGet]和[HttpPost]等标记，而报错，所以需要添加[ApiExplorerSettings(IgnoreApi = true)]标记.
        /// </summary>
        /// <param name="title">标题.</param>
        /// <param name="context">内容.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public string SetModel(string title, string context)
        {
            return string.Format(string.Format("标题：{0}，内容字数：{1}", title, context));
        }
        
        /// <summary>
        /// 生成一个文件，并指定内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public string Save(string title, string content)
        {
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）

            var xmlPath = Path.Combine(basePath, title + ".txt");
            System.IO.File.WriteAllText(xmlPath, content);
            return xmlPath;
        }

        /// <summary>
        /// 用异步的方式处理请求.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReceiveInfo>> BatchSave(string title, string content)
        {
            await Task.Delay(3000);
            return await Task.Run<ReceiveInfo>(() =>
            {
                return new ReceiveInfo(1, "保存", null);
            });
        }

        /// <summary>
        /// 获取天气的集合
        /// </summary>
        /// <response code="201">返回value字符串，这个会显示在swagger上</response>
        /// <response code="400">如果id为空，这个会显示在swagger上</response>  
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<ReceiveInfo> Get(int id)
        {
            switch (id)
            {
                case 0:
                    return BadRequest(new ErrorInfo(1, "请求参数是0"));
                case 1:
                    return new ReceiveInfo(0, "杨中科", "18");
                default:
                    return NotFound(new ErrorInfo(1, "请求参数异常"));
            }
        }

        /// <summary>
        /// 获取Route和QueryString的参数.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="birthday"></param>
        /// <param name="idCard"></param>
        /// <returns></returns>
        [HttpGet("name={name}&csrq={birthday}&zjhm={idCard}")]
        public ActionResult<ReceiveInfo> GetAll(string name, string birthday, string idCard)
        {
            return new ReceiveInfo(0, "张三", "20");
        }

        /// <summary>
        /// 混合使用QueryString和请求报文
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("id={id}")]
        public ActionResult<string> UpdateBatch(string id,[FromBody] Person model)
        {
            return "修改完成";
        }
    }
}
