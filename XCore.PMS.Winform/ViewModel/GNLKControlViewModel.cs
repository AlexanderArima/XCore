using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;
using XCore.PMS.Winform.VO.GNLKControlViewModel;

namespace XCore.PMS.Winform.ViewModel
{
    /// <summary>
    /// 国内旅客对象.
    /// </summary>
    public class GNLKControlViewModel
    {
        public int id { get; set; }

        public string roomid { get; set; }

        public string xm { get; set; }

        public string zjhm { get; set; }

        public string birthday { get; set; }

        public string sex { get; set; }

        public string zjlx { get; set; }

        public string address { get; set; }

        public string type { get; set; }

        public string zjz { get; set; }

        /// <summary>
        /// 预定.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Appoint()
        {
            try
            {
                JObject param = new JObject();
                param.Add(new JProperty("xm", this.xm));
                param.Add(new JProperty("sex", this.sex));
                param.Add(new JProperty("zjlx", this.zjlx));
                param.Add(new JProperty("zjhm", this.zjhm));
                param.Add(new JProperty("roomid", this.roomid));
                param.Add(new JProperty("birthday", this.birthday));
                param.Add(new JProperty("address", this.address));
                param.Add(new JProperty("type", "1"));
                var result = HttpService.PostService<ReceiveObject>(
                   "https://localhost:44384/Order/Appoint",
                   param.ToString());
                if (result.code != 0)
                {
                    return new Tuple<bool, string>(false, result.msg);
                }
                else
                {
                    return new Tuple<bool, string>(true, null);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("GNLKControlViewModel：Appoint出错：" + ex.Message, ex);
                return new Tuple<bool, string>(false, "系统异常");
            }
        }
    }
}
