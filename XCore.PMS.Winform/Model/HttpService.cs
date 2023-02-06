using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.PMS.Winform.Model
{
    public class HttpService
    {
        public static string Token;

        /// <summary>
        /// Get请求数据
        /// </summary>
        /// <param name="url">IP地址</param>
        /// <param name="path">访问路径</param>
        /// <param name="seriviceName">服务名</param>
        /// <returns></returns>
        public static T GetServiceNoAuth<T>(string url, string path, string param)
        {
            var json = Utils.HttpGetNoAuth(string.Format(@"{0}/{1}?{2}", url, path, param), 3000);
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Get请求数据
        /// </summary>
        /// <param name="url">IP地址</param>
        /// <param name="path">访问路径</param>
        /// <param name="seriviceName">服务名</param>
        /// <returns></returns>
        public static T GetService<T>(string url, string path, string param)
        {
            var json = Utils.HttpGet(string.Format(@"{0}/{1}?{2}", url, path, param), 3000);
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Get请求数据
        /// </summary>
        /// <param name="url">IP地址</param>
        /// <param name="path">访问路径</param>
        /// <param name="seriviceName">服务名</param>
        /// <returns></returns>
        public static T GetService<T>(string url, string path)
        {
            var json = Utils.HttpGet(string.Format(@"{0}/{1}", url, path), 3000);
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <param name="path">访问路径</param>
        /// <param name="seriviceName">服务名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static T PostService<T>(string path,
            string param)
        {
            var json = Utils.HttpPost(path, param, 10000);
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <param name="path">访问路径</param>
        /// <param name="seriviceName">服务名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static T PostService<T>(string path, 
            string param, int timeOut)
        {
            var json = Utils.HttpPost(path, param, timeOut);
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
