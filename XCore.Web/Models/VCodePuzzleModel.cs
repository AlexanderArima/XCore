using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.Web.Models;

namespace XCore.WebAPI.Model
{
    public class VCodePuzzleModel
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
            // 这里的随机码是移动图片的X轴距离，目前使用的图片的宽度是400，挖出来的图片宽度是50
            // 所以X轴可以移动的距离是350，但只要要让用户移动一段距离，所以最小移动距离设置为了50
            Random random = new Random();
            return random.Next(50, 350).ToString();
        }

        /// <summary>
        /// 随机获取一张图片.
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetImage()
        {
            // 从文件加载原图
            Random random = new Random();
            var image_index = random.Next(0, 2);
            Image originImage;
            switch (image_index)
            {
                case 0:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "img1.png"));
                    break;

                case 1:
                default:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "img2.png"));
                    break;
            }

            return (Bitmap)originImage;
        }

        /// <summary>
        /// 将复制被裁剪图片（大图）裁剪出裁剪图片（小图），返回裁剪后的大图和裁剪出的小图.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Tuple<Bitmap, Bitmap, int> DrawImage(string code)
        {
            Color[] list_color =
            {
                Color.FromArgb(240, 230, 140),    // 黄褐色（亮）
                Color.FromArgb(138, 54, 15),    // 黄褐色（暗）
                Color.FromArgb(51, 161, 201),    // 蓝色（亮）
                Color.FromArgb(25, 25, 112),    // 蓝色（暗）
                Color.FromArgb(192, 192, 192),    // 灰白（亮）
                Color.FromArgb(128, 128, 105),    // 灰白（暗）
            };

            // 从文件加载原图
            Random random = new Random();
            var image_index = random.Next(0, 2);
            Image originImage;
            
            switch (image_index)
            {
                case 0:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "img1.png"));
                    break;

                case 1:
                default:
                    originImage = Image.FromFile(string.Format(@"{0}\Images\{1}", PathHelper.Path, "img2.png"));
                    break;
            }

            // 随机产生裁剪的Y轴位置，尺寸是固定的：50 * 50，Y轴的范围是0-150
            var x = Convert.ToInt32(code);
            var y = random.Next(0, 150);

            #region 生成裁剪图片

            Image image_big = (Image)originImage.Clone();    // 复制被裁剪图片（大图）
            Rectangle cropRegion = new Rectangle(x, y, 50, 50);

            // 创建空白画布，大小为裁剪区域大小
            Bitmap image_small = new Bitmap(cropRegion.Width, cropRegion.Height);

            // 创建Graphics对象，并指定要在result（目标图片画布）上绘制图像
            Graphics graphics = Graphics.FromImage(image_small);

            //使用Graphics对象把原图指定区域图像裁剪下来并填充进刚刚创建的空白画布
            graphics.DrawImage(originImage, new Rectangle(0, 0, cropRegion.Width, cropRegion.Height), cropRegion, GraphicsUnit.Pixel);

            #endregion

            #region 生成被裁剪图片

            Graphics grp = Graphics.FromImage(image_big);

            // 绘制随机的线条
            var color = list_color[random.Next(list_color.Length)];
            for (int i = 0; i < 50; i++)
            {
                grp.DrawLine(
                    new Pen(color, 1),
                    new Point(x, y + i),
                    new Point(x + 50, y + i )
                );
            }

            #endregion

            return new Tuple<Bitmap, Bitmap, int>((Bitmap)image_big, (Bitmap)image_small, y);
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
    /// VCodeCalculateController控制器中Create方法的返回对象.
    /// </summary>
    public class VCodePuzzleController_Create_Receive : ReceiveObject
    {
        public Data data { get; set; } = new VCodePuzzleController_Create_Receive.Data();

        public class Data
        {
            /// <summary>
            /// 令牌.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 图片Y轴的值.
            /// </summary>
            public int y { get; set; }

            /// <summary>
            /// Base64被裁剪的图片.
            /// </summary>
            public string bigImg { get; set; }

            /// <summary>
            /// Base64裁剪的图片.
            /// </summary>
            public string smallImg { get; set; }
        }
    }
}
