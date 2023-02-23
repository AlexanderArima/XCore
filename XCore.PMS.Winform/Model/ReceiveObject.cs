using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.PMS.Winform.Model
{
    public class ReceiveObject<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
}
