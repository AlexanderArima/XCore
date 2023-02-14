using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.WebAPI.Model_ORM;

namespace XCore.PMS.WebAPI.VO.Room
{
    public class GetListVO
    {
        public int code { get; set; }

        public string msg { get; set; }

        public List<TRoom> data { get; set; }
    }
}
