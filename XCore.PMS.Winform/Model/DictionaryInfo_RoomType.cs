using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.VO.DictionaryInfo_RoomType;

namespace XCore.PMS.Winform.Model
{
    public class DictionaryInfo_RoomType
    {
        public string actualName { get; set; }

        public string displayName { get; set; }

        /// <summary>
        /// 获取房间类型.
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<DictionaryInfo_RoomType> GetList()
        {
            try
            {
                var result = HttpService.GetService<GetListVO>(
                  "https://localhost:44384",
                  "Dictionary/GetList",
                  "typeid=1");
                List<DictionaryInfo_RoomType> list = new List<DictionaryInfo_RoomType>();
                if (result.code == 0)
                {
                    for (int i = 0; i < result.data.Count; i++)
                    {
                        var item = result.data[i];
                        DictionaryInfo_RoomType model = new DictionaryInfo_RoomType();
                        model.displayName = item.Displayname;
                        model.actualName = item.Actualname;
                        list.Add(model);
                    }
                }
                else
                {
                    list = null;
                }

                return list;
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("DictionaryInfo_RoomType：GetList出错：" + ex.Message, ex);
                return null;
            }
        }
    }
}
