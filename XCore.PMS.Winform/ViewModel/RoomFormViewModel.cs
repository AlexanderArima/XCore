using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;
using XCore.PMS.Winform.VO.DictionaryInfo_RoomType;
using XCore.PMS.Winform.VO.RoomFormViewModel_GetSingle;

namespace XCore.PMS.Winform.ViewModel
{
    /// <summary>
    /// 房间管理的视图模型类.
    /// </summary>
    public class RoomFormViewModel
    {
        /// <summary>
        /// 房间编号.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 房间名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 房间类型.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 房间状态.
        /// </summary>
        public string Statue { get; set; }

        /// <summary>
        /// 获取房间列表.
        /// </summary>
        public static Tuple<bool, List<RoomFormViewModel>, string> GetList()
        {
            try
            {
                var result = HttpService.PostService<XCore.PMS.Winform.VO.RoomFormViewModel_GetSingle.GetListVO>(
                   "https://localhost:44384",
                   "Room/GetList");
                if(result.code == 0)
                {
                    List<RoomFormViewModel> list = new List<RoomFormViewModel>();
                    for (int i = 0; i < result.data.Count; i++)
                    {
                        var item = result.data.ElementAt(i);
                        RoomFormViewModel model = new RoomFormViewModel();
                        model.ID = item.Id.ToString();
                        model.Name = item.Name;
                        model.Type = item.Type;
                        model.Statue = item.Status;
                        list.Add(model);
                    }

                    return new Tuple<bool, List<RoomFormViewModel>, string>(true, list, null);
                }
                else
                {
                    return new Tuple<bool, List<RoomFormViewModel>, string>(false, null, result.msg);
                }
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("RoomFormViewModel：GetList出错：" + ex.Message, ex);
                return new Tuple<bool, List<RoomFormViewModel>, string>(false, null, ex.Message);
            }
        }

        public static Tuple<bool, RoomFormViewModel, string> GetSingle(string roomid)
        {
            try
            {
                var result = HttpService.GetService<GetSingleVO>(
                   "https://localhost:44384/Room", "GetRoom?id=" + roomid);
                if (result.code != 0)
                {
                    return new Tuple<bool, RoomFormViewModel, string>(false, null, result.msg);
                }
                else
                {
                    RoomFormViewModel model = new RoomFormViewModel();
                    model.Name = result.data.Name;
                    model.Type = result.data.Type;
                    model.ID = result.data.Id.ToString();
                    model.Statue = result.data.Status;
                    return new Tuple<bool, RoomFormViewModel, string>(true, model, null);
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("RoomFormViewModel：GetSingle出错：" + ex.Message, ex);
                return new Tuple<bool, RoomFormViewModel, string>(false, null, "系统异常");
            }
        }

        public static Tuple<bool, string> Add(string name, string typeid)
        {
            try
            {
                JObject param = new JObject();
                param.Add(new JProperty("Name", name));
                param.Add(new JProperty("Type", typeid));
                var result = HttpService.PostService<ReceiveObject>(
                   "https://localhost:44384/Room/AddRoom",
                   param.ToString());
                if(result.code != 0)
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
                Log4NetHelper.Error("RoomFormViewModel：GetList出错：" + ex.Message, ex);
                return new Tuple<bool, string>(false, "系统异常");
            }
        }

        public static Tuple<bool, string> Delete(string roomid)
        {
            try
            {
                var result = HttpService.GetService<ReceiveObject>(
                   "https://localhost:44384/Room", "DeleteRoom?id=" + roomid);
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
                Log4NetHelper.Error("RoomFormViewModel：Delete出错：" + ex.Message, ex);
                return new Tuple<bool, string>(false, "系统异常");
            }

        }

    }
}
