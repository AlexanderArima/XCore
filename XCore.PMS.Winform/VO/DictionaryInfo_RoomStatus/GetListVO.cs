using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.VO.DictionaryInfo_RoomStatus
{
    public class GetListVO
    {
        public int code { get; set; }

        public string msg { get; set; }

        public List<TDictionary> data { get; set; }
    }
}
