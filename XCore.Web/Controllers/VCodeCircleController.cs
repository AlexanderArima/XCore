using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCore.Web.Models;

namespace XCore.Web.Controllers
{
    public class VCodeCircleController : Controller
    {
        static List<VCodeCircleModel> _list = new List<VCodeCircleModel>();

        private readonly ILogger<VCodeCircleController> _logger;

        public VCodeCircleController(ILogger<VCodeCircleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回一张图片和令牌.
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            try
            {
                // 记录验证码到缓存中
                VCodeCircleModel model = new VCodeCircleModel();
                model.id = Guid.NewGuid().ToString();    // 生成令牌
                var vcode = VCodeCircleModel.GetVCode();    // 生成验证码
                model.code = vcode;
                _list.Add(model);

                // 返回图片
                var images = VCodeCircleModel.GetImage(Convert.ToInt32(vcode));
                VCodeCircleController_Create_Receive result = new VCodeCircleController_Create_Receive();
                result.code = "0";
                result.data.id = model.id;
                result.data.img = VCodeCircleModel.BitmapToBase64Str(images);
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: ex.Message);
                VCodeCircleController_Create_Receive result = new VCodeCircleController_Create_Receive();
                result.code = "999999";
                result.msg = "系统异常";
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
        }

        public string Check(string id, string code)
        {
            try
            {
                // 旋转图片的误差在±5
                var temp = Convert.ToInt32(code) - 5;
                var index = _list.FindIndex(m =>
                {
                    if (m.id.Equals(id))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (m.code.Equals(temp.ToString()))
                            {
                                return true;
                            }

                            temp++;
                        }
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
