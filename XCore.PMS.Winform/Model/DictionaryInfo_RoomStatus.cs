using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.VO.DictionaryInfo_RoomStatus;

namespace XCore.PMS.Winform.Model
{
    public class DictionaryInfo_RoomStatus
    {
        public static List<DictionaryInfo_RoomStatus> list_roomstatus;

        public string actualName { get; set; }

        public string displayName { get; set; }

        static DictionaryInfo_RoomStatus()
        {
            try
            {
                var result = HttpService.GetService<GetListVO>(
                  "https://localhost:44384",
                  "Dictionary/GetList",
                  "typeid=2");
                List<DictionaryInfo_RoomStatus> list = new List<DictionaryInfo_RoomStatus>();
                if (result.code == 0)
                {
                    for (int i = 0; i < result.data.Count; i++)
                    {
                        var item = result.data[i];
                        DictionaryInfo_RoomStatus model = new DictionaryInfo_RoomStatus();
                        model.displayName = item.Displayname;
                        model.actualName = item.Actualname;
                        list.Add(model);
                    }
                }
                else
                {
                    list = null;
                }

                DictionaryInfo_RoomStatus.list_roomstatus = list;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("DictionaryInfo_ZJLX：出错：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取房间类型.
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<DictionaryInfo_RoomStatus> GetList()
        {
            return list_roomstatus;
        }
    }
}
