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
    public class VCodeCalculateController : ControllerBase
    {
        private static List<VCodeCalculateModel> _list = new List<VCodeCalculateModel>();

        private readonly ILogger<VCodeCalculateController> _logger;

        public VCodeCalculateController(ILogger<VCodeCalculateController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 返回一个图形验证码和令牌.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Create()
        {
            VCodeCalculateModel model = new VCodeCalculateModel();
            model.id = Guid.NewGuid().ToString();    // 生成令牌
            var codes = VCodeCalculateModel.GetVCode();
            var num1 = codes.Item1;
            var num2 = codes.Item2;
            var bool_operator = codes.Item3;
            if (bool_operator)
            {
                // 加法运算，生成验证码
                model.code = Convert.ToString(num1 + num2);
            }
            else
            {
                // 减法运算，生成验证码
                model.code = Convert.ToString(num1 - num2);
            }

            _list.Add(model);

             // 返回对象
             VCodeCalculateController_Create_Receive result = new VCodeCalculateController_Create_Receive();
            result.code = "0";
            result.data = new VCodeCalculateController_Create_Receive.Data();
            result.data.id = model.id;
            var image = VCodeCalculateModel.DrawImage(num1, num2, bool_operator);
            var base64 = Utils.BitmapToBase64Str(image);
            result.data.img = base64;
            var json = JsonConvert.SerializeObject(result);
            return json;
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
