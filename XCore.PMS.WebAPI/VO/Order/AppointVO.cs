using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.VO.Order
{
    public class AppointVO
    {
        public int Id { get; set; }

        public string Roomid { get; set; }

        public string Xm { get; set; }

        public string Ywx { get; set; }

        public string Ywm { get; set; }

        public string Zjhm { get; set; }

        public string Birthday { get; set; }

        public int Sex { get; set; }

        public string Zjlx { get; set; }

        public string Qzlx { get; set; }

        public string Address { get; set; }

        public string Appointtime { get; set; }

        public string Zjz { get; set; }

        public string Gj { get; set; }

        /// <summary>
        /// 是否是国内旅客.
        /// </summary>
        public string Type { get; set; }
    }
}
