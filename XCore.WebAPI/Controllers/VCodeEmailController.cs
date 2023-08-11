using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

        private readonly IMemoryCache _cache;

        public VCodeEmailController(ILogger<VCodeEmailController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// 返回邮件发送状态.
        /// </summary>
        /// <param name="emailAddress">邮件地址.</param>
        /// <returns></returns>
        [HttpGet]
        public string Create(string emailAddress)
        {
            try
            {
                VCodeEmailModel model = new VCodeEmailModel();
                model.emailAddress = emailAddress;
                var vcode = Utils.GetVCode();    // 生成验证码
                model.code = vcode;
                _list.Add(model);

                // 将邮件记录到缓存中
                this._cache.GetOrCreate(emailAddress, m =>
                {
                    // 设置缓存有效期
                    m.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    return "";
                });

                // 发送邮件
                EmailMessage email_model = new EmailMessage();
                email_model.Send(emailAddress, vcode);

                // 返回发送状态
                ReceiveObject result = new ReceiveObject();
                result.code = "0";
                result.msg = "验证成功";
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch(Exception ex)
            {
                _logger.LogError(exception: ex, message: ex.Message);
                ReceiveObject result = new ReceiveObject();
                result.code = "999999";
                result.msg = "系统异常";
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
        }

        /// <summary>
        /// 检查验证码是否有效.
        /// </summary>
        /// <param name="emailAddress">邮件地址.</param>
        /// <param name="code">验证码.</param>
        /// <returns></returns>
        [HttpGet]
        public string Check(string emailAddress, string code)
        {
            try
            {
                ReceiveObject result = new ReceiveObject();
                if (this._cache.Get(emailAddress) == null)
                {
                    _list.Remove(_list.Find(m => m.emailAddress == emailAddress));    // 发现过期的邮箱，将它从变量中剔除
                    result.code = "2";
                    result.msg = "验证码已过期";
                    var json = JsonConvert.SerializeObject(result);
                    return json;
                }

                var index = _list.FindIndex(m =>
                {
                    if (m.emailAddress.Equals(emailAddress) && m.code.Equals(code))
                    {
                        return true;
                    }

                    return false;
                });

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
