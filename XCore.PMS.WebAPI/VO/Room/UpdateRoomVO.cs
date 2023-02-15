using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.VO.Room
{
    public class UpdateRoomVO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
