using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.ViewModel
{
    public class ZZLKControlViewModel
    {
        public string Id { get; set; }
        public string XM { get; set; }
        public string YWX { get; set; }
        public string YWM { get; set; }
        public string XB { get; set; }
        public string FJHM { get; set; }
        public string RZSJ { get; set; }
        public string MZ { get; set; }
        public string GJ { get; set; }
        public string ZJLX { get; set; }
        public string ZJHM { get; set; }
        public string Type { get; set; }

        public static Tuple<bool, List<ZZLKControlViewModel>, string> Query(int index, int size)
        {
            try
            {
                var result = HttpService.GetService<XCore.PMS.Winform.VO.GNLKControlViewModel.QueryVO>(
                                   "https://localhost:44384",
                                   "Order/GetCheckinList");
                if (result.code == 0)
                {
                    List<ZZLKControlViewModel> list = new List<ZZLKControlViewModel>();
                    for (int i = 0; i < result.data.Count; i++)
                    {
                        var item = result.data.ElementAt(i);
                        ZZLKControlViewModel model = new ZZLKControlViewModel();
                        model.Id = item.Id.ToString();
                        model.XM = item.Xm;
                        model.XB = item.Sex.ToString();
                        model.FJHM = item.Roomid;
                        model.ZJLX = item.Zjlx;
                        model.ZJHM = item.Zjhm;
                        model.RZSJ = item.Checkintime;
                        model.YWM = item.Ywm;
                        model.YWX = item.Ywx;
                        model.Type = item.Type;
                        list.Add(model);
                    }

                    return new Tuple<bool, List<ZZLKControlViewModel>, string>(true, list, null);
                }
                else
                {
                    return new Tuple<bool, List<ZZLKControlViewModel>, string>(false, null, result.msg);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("ZZLKControlViewModel：Query出错：" + ex.Message, ex);
                return new Tuple<bool, List<ZZLKControlViewModel>, string>(false, null, ex.Message);
            }
        }

        /// <summary>
        /// 退房.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Tuple<bool, string> Checkout(List<string> list)
        {
            throw new Exception("");
        }

        /// <summary>
        /// 换房.
        /// </summary>
        /// <returns></returns>
        public static Tuple<bool, string> ChangeRoom(string id, string roomid)
        {
            throw new Exception("");
        }
    }
}
