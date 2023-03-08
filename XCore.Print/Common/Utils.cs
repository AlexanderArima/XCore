using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Print.Common
{
    public class Utils
    {
        public static string Base64URLToBase64(string str)
        {
            str = str.Replace('-', '+').Replace('_', '/');
            switch (str.Length % 4)
            {
                case 2:
                    str += "==";
                    break;
                case 3:
                    str += "=";
                    break;
            }

            var bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
