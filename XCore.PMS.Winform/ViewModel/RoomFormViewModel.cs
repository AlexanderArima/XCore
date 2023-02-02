using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

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
        public void GetList()
        {
            try
            {
                var result = HttpService.GetService<ReceiveObject>(
                   "https://localhost:44384",
                   "Room/GetList");

            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("RoomFormViewModel：GetList出错：" + ex.Message, ex);
            }
        }


    }
}
