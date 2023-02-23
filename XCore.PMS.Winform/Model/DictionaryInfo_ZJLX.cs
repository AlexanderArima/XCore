using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.VO.DictionaryInfo_RoomType;

namespace XCore.PMS.Winform.Model
{
    public class DictionaryInfo_ZJLX
    {
        public static List<DictionaryInfo_ZJLX> list_roomtype;

        public string actualName { get; set; }

        public string displayName { get; set; }

        static DictionaryInfo_ZJLX()
        {
            try
            {
                var result = HttpService.GetService<GetListVO>(
                  "https://localhost:44384",
                  "Dictionary/GetList",
                  "typeid=3");
                List<DictionaryInfo_ZJLX> list = new List<DictionaryInfo_ZJLX>();
                if (result.code == 0)
                {
                    for (int i = 0; i < result.data.Count; i++)
                    {
                        var item = result.data[i];
                        DictionaryInfo_ZJLX model = new DictionaryInfo_ZJLX();
                        model.displayName = item.Displayname;
                        model.actualName = item.Actualname;
                        list.Add(model);
                    }
                }
                else
                {
                    list = null;
                }

                DictionaryInfo_ZJLX.list_roomtype = list;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("DictionaryInfo_RoomType：出错：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取房间类型.
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<DictionaryInfo_ZJLX> GetList()
        {
            return list_roomtype;
        }
    }
}
