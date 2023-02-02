using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.PMS.Winform.Model
{
    interface DictionaryInfo
    {
        List<T> GetList<T>(string typeid);
    }
}
