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
    /// 图形验证码.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class VCodeController : ControllerBase
    {
        private static List<VCodeModel> _list = new List<VCodeModel>();

        private readonly ILogger<VCodeController> _logger;

        public VCodeController(ILogger<VCodeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 返回一个图形验证码和令牌.
        /// </summary>
        /// <returns></returns>
        /// <remarks>这一步需要生成一个数字，并将数字放入图片中，并返回一个令牌，下次验证的时候需要核对令牌和数字是否对应，这些东西保存到类变量中.</remarks>
        [HttpGet]
        public string Create()
        {
            try
            {
                VCodeModel model = new VCodeModel();
                model.id = Guid.NewGuid().ToString();    // 生成令牌
                var vcode = Utils.GetVCode();    // 生成验证码
                model.code = vcode;
                _list.Add(model);    // 记录到缓存中

                // 返回对象
                VCodeController_Create_Receive result = new VCodeController_Create_Receive();
                result.code = "0";
                result.data = new VCodeController_Create_Receive.Data();
                result.data.id = model.id;
                var image = Utils.DrawImage(vcode);
                var base64 = Utils.BitmapToBase64Str(image);
                result.data.img = base64;
                var json = JsonConvert.SerializeObject(result);
                return json;
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

        /// <summary>
        /// 检查验证码是否有效
        /// </summary>
        /// <param name="id">令牌.</param>
        /// <param name="code">验证码.</param>
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
    }
}
