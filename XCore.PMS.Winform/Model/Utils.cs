using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Tls;

namespace XCore.PMS.Winform.Model
{
    public class Utils
    {
        /// <summary>
        /// 调用Https请求之前，调用此方法，解决"基础连接已经关闭 未能为 SSLTLS 安全通道建立信任关系"的问题
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            //  System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }

        /// <summary>
        /// GET方式请求数据，无需Token
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="timeout">请求超时时间，以毫秒为单位</param>
        /// <returns></returns>
        /// <remarks>范围：只在定时轮询异常数据时使用.</remarks>
        public static string HttpGetNoAuth(string url, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";

            // 处理：基础连接已经关闭: 接收时发生意外错误
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Accept = "*/*";
            request.Timeout = timeout;
            request.AllowAutoRedirect = false;
            //request.Headers.Add("Authorization", string.Format("bearer {0}", HttpService.Token));
            //request.Headers[HttpRequestHeader.Connection] = "keep-alive";
            //request.Headers.Add("stationid", GlobalVariable.StationID);
            //request.Headers.Add("pcVersion", GlobalVariable.PCVersion);
            IWebProxy theProxy = request.Proxy;
            if (theProxy != null)
            {
                // Use the default credentials of the logged on user.
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }
            // 指定请求包的安全协议，因为不知道你当前项目到底是哪个版本所以为了安全保障都加上
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            SetCertificatePolicy();
            using (WebResponse response = request.GetResponse())
            {
                string responseStr = null;
                try
                {
                    if (response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseStr = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
                return responseStr;
            }
        }

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="timeout">请求超时时间，以毫秒为单位.</param>
        /// <returns></returns>
        /// <remarks>
        /// 2020/07/01 取消轮询：1：时间接口（更换成接口catch时，再去判断是否有网络，再走离线逻辑）
        /// 发送请求如果抛出WebException则调用3次时间接口
        /// </remarks>
        public static string HttpGet(string url, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";

            // 处理：基础连接已经关闭: 接收时发生意外错误
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Accept = "*/*";
            request.Headers.Add("Authorization", string.Format("bearer {0}", HttpService.Token));
            request.Timeout = timeout;
            request.AllowAutoRedirect = false;
            //request.Headers.Add("stationid", GlobalVariable.StationID);
            //request.Headers.Add("pcVersion", GlobalVariable.PCVersion);
            IWebProxy theProxy = request.Proxy;
            if (theProxy != null)
            {
                // Use the default credentials of the logged on user.
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }
            // 指定请求包的安全协议，因为不知道你当前项目到底是哪个版本所以为了安全保障都加上
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            SetCertificatePolicy();
            string responseStr = null;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    try
                    {
                        if (response != null)
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                            {
                                responseStr = reader.ReadToEnd();
                            }
                        }
                    }
                    finally
                    {
                        if (request != null)
                        {
                            request.Abort();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                //调用时间接口判断是否在线
                //如果3次轮询后依然离线就修改全局变量的值，UI层会轮询该参数决定是否进入离线模式
                //if (IsOnline() == false)
                //{
                //    GlobalVariable.IsOnline_bool = false;
                //}
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseStr;
        }

        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <param name="timeOut">过期时间(毫秒)</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param, int timeOut)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.Timeout = timeOut;
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", string.Format("bearer {0}", HttpService.Token));
            IWebProxy theProxy = request.Proxy;
            if (theProxy != null)
            {
                // Use the default credentials of the logged on user.
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }
            SetCertificatePolicy();
            try
            {
                //添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(param.ToString());
                request.ContentLength = data.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                Stream stream = null;
                HttpWebResponse resp = null;
                //获取响应内容
                string result = "";
                try
                {
                    resp = (HttpWebResponse)request.GetResponse();
                    stream = resp.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                catch(Exception ex)
                {
                    Log4NetHelper.Error("Utils：HttpPost出错：" + ex.Message, ex);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (resp != null)
                    {
                        resp.Close();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("Utils：HttpPost出错：" + ex.Message, ex);
                return null;
            }
        }
    }
}
