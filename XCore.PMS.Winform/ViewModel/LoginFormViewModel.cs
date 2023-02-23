using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.ViewModel
{
    public class LoginFormViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 登录获取Token.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Login()
        {
            if(string.IsNullOrWhiteSpace(this.UserName) || string.IsNullOrWhiteSpace(this.Password))
            {
                return new Tuple<bool, string>(false, "用户名或密码不能为空");
            }

            try
            {
                var result = HttpService.GetServiceNoAuth<ReceiveObject<string>>(
                "https://localhost:44384",
                "User/Login",
                string.Format("username={0}&password={1}", this.UserName, this.Password));
                if (result.code == 0)
                {
                    HttpService.Token = result.data;
                    return new Tuple<bool, string>(true, string.Empty);
                }
                else
                {
                    return new Tuple<bool, string>(false, result.msg);
                }
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("LoginFormViewModel：Login出错：" + ex.Message, ex);
                return new Tuple<bool, string>(false, "系统异常");
            }
        }

    }
}
