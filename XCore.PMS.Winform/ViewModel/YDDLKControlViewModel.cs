using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.ViewModel
{
    public class YDDLKControlViewModel
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

        public static Tuple<bool, List<YDDLKControlViewModel>, string> Query(int index, int size)
        {
            try
            {
                var result = HttpService.GetService<ReceiveList<TOrder>>(
                                   "https://localhost:44384",
                                   string.Format("Order/GetAppointList?index={0}&size={1}", index, size));
                if (result.code == 0)
                {
                    List<YDDLKControlViewModel> list = new List<YDDLKControlViewModel>();
                    for (int i = 0; i < result.data.count; i++)
                    {
                        var item = result.data.list.ElementAt(i);
                        YDDLKControlViewModel model = new YDDLKControlViewModel();
                        model.Id = item.Id.ToString();
                        model.XM = item.Xm;
                        model.XB = item.Sex.ToString();
                        model.FJHM = item.Roomid;
                        model.ZJLX = item.Zjlx;
                        model.ZJHM = item.Zjhm;
                        model.RZSJ = item.Appointtime;
                        model.YWM = item.Ywm;
                        model.YWX = item.Ywx;
                        model.Type = item.Type;
                        list.Add(model);
                    }

                    return new Tuple<bool, List<YDDLKControlViewModel>, string>(true, list, null);
                }
                else
                {
                    return new Tuple<bool, List<YDDLKControlViewModel>, string>(false, null, result.msg);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("ZZLKControlViewModel：Query出错：" + ex.Message, ex);
                return new Tuple<bool, List<YDDLKControlViewModel>, string>(false, null, ex.Message);
            }
        }

        /// <summary>
        /// 删除.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Tuple<bool, string> Delete(List<string> list)
        {
            throw new Exception("");
        }
    }
}
