using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.Web.Models
{
    public class VCodeCircleModel
    {
        /// <summary>
        /// 令牌.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///验证码.
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 获取随机验证码.
        /// </summary>
        /// <returns></returns>
        public static string GetVCode()
        {
            // 这里的随机码是旋转图片的角度，至少旋转60度，最多旋转300度
            Random random = new Random();
            return random.Next(60, 300).ToString();
        }

        /// <summary>
        /// 随机获取一张图片.
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetImage(int angle)
        {
            // 从文件加载原图
            Random random = new Random();
            var image_index = random.Next(0, 2);
            Image originImage;
            switch (image_index)
            {
                case 0:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "circleImg1.png"));
                    break;

                case 1:
                default:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "circleImg2.png"));
                    break;
            }

            originImage = Rotate(originImage as Bitmap, angle);
            return (Bitmap)originImage;
        }

        /// <summary>
        /// 图片旋转
        /// </summary>
        /// <param name="AngleValue"></param>
        /// <returns></returns>
        public static Bitmap Rotate(Bitmap ImageOriginal, float AngleValue)
        {
            AngleValue = AngleValue % 360;
            double radian = AngleValue * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            int w = ImageOriginal.Width;
            int h = ImageOriginal.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            Bitmap ImageBaseOriginal = new Bitmap(W, H, PixelFormat.Format32bppArgb);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ImageBaseOriginal);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.Clear(Color.White);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - AngleValue);
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(ImageOriginal, rect);
            g.ResetTransform();
            g.Save();
            g.Dispose();
            return ImageBaseOriginal;
        }

        /// <summary>
        /// 将图片对象转成Base64的字符串.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string BitmapToBase64Str(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    /// <summary>
    /// VCodeCircleController.
    /// </summary>
    public class VCodeCircleController_Create_Receive : ReceiveObject
    {
        public Data data { get; set; } = new VCodeCircleController_Create_Receive.Data();

        public class Data
        {
            /// <summary>
            /// 令牌.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Base64被裁剪的图片.
            /// </summary>
            public string img { get; set; }
        }
    }
}
