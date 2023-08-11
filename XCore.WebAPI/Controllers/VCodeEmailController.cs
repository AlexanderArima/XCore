using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCore.WebAPI.Model;

namespace XCore.WebAPI.Controllers
{
    /// <summary>
    /// 电子邮箱验证码.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class VCodeEmailController : ControllerBase
    {
        private static List<VCodeEmailModel> _list = new List<VCodeEmailModel>();

        private readonly ILogger<VCodeEmailController> _logger;

        public VCodeEmailController(ILogger<VCodeEmailController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 返回邮件发送状态.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Create(string distEmail)
        {
            distEmail = "917563264@qq.com";
            VCodeEmailModel model = new VCodeEmailModel();
            model.id = Guid.NewGuid().ToString();    // 生成令牌
            var vcode = Utils.GetVCode();    // 生成验证码
            model.code = vcode;
            _list.Add(model);    // 记录到缓存中

            // 发送邮件
            EmailMessage obj = new EmailMessage();
            obj.Get();

            // 返回发送状态
            return null;
        }

        /// <summary>
        /// 检查验证码是否有效.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Check(string id, string code)
        {
            try
            {
                var index = _list.FindIndex(m =>
                {
                    if (m.id.Equals(id) && m.code.Equals(code))
                    {
                        return true;
                    }

                    return false;
                });

                ReceiveObject result = new ReceiveObject();
                if (index >= 0)
                {
                    _list.RemoveAt(index);
                    result.code = "0";
                    result.msg = "验证成功";
                    var json = JsonConvert.SerializeObject(result);
                    return json;
                }
                else
                {
                    result.code = "1";
                    result.msg = "验证失败";
                    var json = JsonConvert.SerializeObject(result);
                    return json;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: ex.Message);
                ReceiveObject result = new ReceiveObject();
                result.code = "999999";
                result.msg = "系统异常";
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
        }
     }
}
