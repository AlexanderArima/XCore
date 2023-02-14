using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.Winform.Model;

namespace XCore.PMS.Winform.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 将图片以二进制流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetImgByte(string path)
        {
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
                br = new BinaryReader(fs);
                byte[] imgBytesIn = br.ReadBytes((int)fs.Length); //将流读入到字节数组中
                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                string stImageByte = Convert.ToBase64String(imgBytesIn);
                return stImageByte;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(string.Format("将图片转成二进制流时出错，错误信息：{0}", ex.Message), ex);
            }
            finally
            {
                if (br != null)
                {
                    br.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return "";
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool Save(string path, string fileName, Image image, ImageFormat format = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bmp = null;
                try
                {
                    //这句很重要，不然不能正确保存图片或出错（关键就这一句）
                    bmp = new Bitmap(image);
                    if (format == null)
                    {
                        bmp.Save(path + "\\" + fileName);
                    }
                    else
                    {
                        bmp.Save(path + "\\" + fileName, format);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw ex;
                }
                finally
                {
                    if (bmp != null)
                    {
                        bmp.Dispose();
                        //保存到磁盘文件
                    }
                }
            }
        }
    }
}
