using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class Utils
    {
        static readonly char[] list = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', };

        /// <summary>
        /// 获取随机验证码.
        /// </summary>
        /// <returns></returns>
        public static string GetVCode()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                var index = random.Next(0, list.Length);
                builder.Append(list[index]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 绘制验证码的图片.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Bitmap DrawImage(string code)
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

            Random random = new Random();

            // 创建画板
            Bitmap bitmap = new Bitmap(150, 50);

            // 创建画笔
            Graphics grp = Graphics.FromImage(bitmap);
            grp.Clear(Color.White);    // 设置背景色为白色

            // 绘制噪点
            for (int i = 0; i < random.Next(60, 80); i++)
            {
                int x = random.Next(bitmap.Width);
                int y = random.Next(bitmap.Height);
                grp.DrawLine(new Pen(Color.LightGray, 1), x, y, x + 1, y);
            }

            // 绘制随机的线条
            grp.DrawLine(
                new Pen(list_color[random.Next(list_color.Length)], random.Next(3)),
                new Point(random.Next(bitmap.Width / 2), random.Next(bitmap.Height / 2)),
                new Point(bitmap.Width / 2 + random.Next(bitmap.Width / 2), bitmap.Height / 2 + random.Next(bitmap.Height / 2))
            );

            // 绘制验证码
            for (int i = 0; i < code.Length; i++)
            {
                var item = code[i];
                grp.DrawString(item.ToString(),
                    new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold),
                    new SolidBrush(list_color[random.Next(list_color.Length)]),
                    x: (150 / 4) * i,
                    y: random.Next(5));
            }

            // 在验证码上绘制噪点
            for (int i = 0; i < random.Next(30, 50); i++)
            {
                int x = random.Next(bitmap.Width);
                int y = random.Next(bitmap.Height);
                grp.DrawLine(new Pen(list_color[random.Next(list_color.Length)], 1), x, y, x + 1, y);
            }

            // 绘制随机的线条
            grp.DrawLine(
                new Pen(list_color[random.Next(list_color.Length)], random.Next(3)),
                new Point(random.Next(bitmap.Width / 2), random.Next(bitmap.Height / 2)),
                new Point(bitmap.Width / 2 + random.Next(bitmap.Width / 2), bitmap.Height / 2 + random.Next(bitmap.Height / 2))
            );

            return bitmap;
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
}
