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
    public class VCodeImageSelectController : Controller
    {
        static List<VCodeImageSelectModel> _list = new List<VCodeImageSelectModel>();

        private readonly ILogger<VCodeImageSelectController> _logger;

        public VCodeImageSelectController(ILogger<VCodeImageSelectController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回多张图片，一个文字和令牌.
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            try
            {
                VCodeImageSelectModel model = new VCodeImageSelectModel();
                model.id = Guid.NewGuid().ToString();    // 生成令牌
                var vcode = VCodeImageSelectModel.GetVCode();    // 生成验证码
                model.code.text = vcode.Item1;
                model.code.image = vcode.Item4;
                _list.Add(model);

                // 返回结果
                var image = VCodeImageSelectModel.DrawImage(model.code.text);
                var base64 = VCodeImageSelectModel.BitmapToBase64Str(image);
                var images = vcode.Item2;
                VCodeImageSelectController_Create_Receive result = new VCodeImageSelectController_Create_Receive();
                result.code = "0";
                result.data.id = model.id;
                result.data.img = VCodeImageSelectModel.BitmapToBase64Str(images);
                result.data.img_index = vcode.Item3;
                result.data.img_text = base64;
                var json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: ex.Message);
                VCodeImageSelectController_Create_Receive result = new VCodeImageSelectController_Create_Receive();
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
                ReceiveObject result = new ReceiveObject();
                var list_result = code.Split(',').ToList();    // 拆分验证码为数组
                var list_temp = new List<string>(); 
                if(_list.Find(m => m.id == id) == null)
                {
                    result.code = "999999";
                    result.msg = "系统异常";
                    var json = JsonConvert.SerializeObject(result);
                    return json;
                }

                _list.Find(m => m.id == id).code.image.ForEach(m =>
                {
                    // 复制数组，避免重复提交后仍然能获取到完整的数据
                    list_temp.Add(m);
                });

                var index = _list.FindIndex(m =>
                {
                    var flag = false;
                    for (int i = 0; i < list_result.Count; i++)
                    {
                        var item = list_result[i];
                        flag = m.code.image.Exists(m => m.Equals(item));
                        if(flag == false)
                        {
                            // 选中了错误的
                            return false;
                        }
                        else
                        {
                            // 每次将选中的项删除，这样最终得到的数组应该是空的
                            list_temp.Remove(item);
                        }
                    }
                    
                    if(list_temp.Count > 0)
                    {
                        // 未全部选中
                        return false;
                    }

                    if (m.id.Equals(id) && flag)
                    {
                        // 通过
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
