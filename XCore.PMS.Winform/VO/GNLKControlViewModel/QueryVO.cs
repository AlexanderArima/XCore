using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.VO.GNLKControlViewModel
{
    public class QueryVO
    {
        public int code { get; set; }

        public string msg { get; set; }

        public List<TOrder> data { get; set; }
    }
}
