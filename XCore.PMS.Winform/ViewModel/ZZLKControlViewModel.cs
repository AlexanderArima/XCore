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

        public static List<ZZLKControlViewModel> Query(int index, int size)
        {
            throw new Exception("");
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
