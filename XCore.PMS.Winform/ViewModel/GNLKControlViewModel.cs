using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Tuple<bool, string> Appoint()
        {
            return new Tuple<bool, string>(false, null);
        }
    }
}
