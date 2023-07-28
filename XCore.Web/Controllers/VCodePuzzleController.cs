using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCore.Web.Models;
using XCore.WebAPI.Model;

namespace XCore.Web.Controllers
{
    public class VCodePuzzleController : Controller
    {
        static List<VCodePuzzleModel> _list = new List<VCodePuzzleModel>();

        private readonly ILogger<VCodePuzzleController> _logger;

        public VCodePuzzleController(ILogger<VCodePuzzleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 生成两张图片，一个是缺了一块的图，一个是挖出来的图，还有令牌.
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            try
            {
                // 记录验证码到缓存中
                VCodePuzzleModel model = new VCodePuzzleModel();
                model.id = Guid.NewGuid().ToString();    // 生成令牌
                var vcode = VCodePuzzleModel.GetVCode();    // 生成验证码
                model.code = vcode;
                _list.Add(model);

                // 返回图片
                var images = VCodePuzzleModel.DrawImage(vcode);
                VCodePuzzleController_Create_Receive result = new VCodePuzzleController_Create_Receive();
                result.code = "0";
                result.data.id = model.id;
                result.data.bigImg = VCodePuzzleModel.BitmapToBase64Str(images.Item1);
                result.data.smallImg = VCodePuzzleModel.BitmapToBase64Str(images.Item2);
                result.data.y = images.Item3;
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch(Exception ex)
            {
                _logger.LogWarning(exception: ex, message: ex.Message);
                VCodePuzzleController_Create_Receive result = new VCodePuzzleController_Create_Receive();
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
                // 移动图片的误差在±5
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
