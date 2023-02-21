using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.VO.RoomFormViewModel_GetSingle
{
    public class GetListVO
    {
        public int code { get; set; }

        public string msg { get; set; }

        public List<TRoom> data { get; set; }
    }
}
