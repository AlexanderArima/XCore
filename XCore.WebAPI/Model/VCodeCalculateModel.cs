using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCore.WebAPI.Model
{
    public class VCodeCalculateModel
    {
        /// <summary>
        /// 令牌.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 验证码.
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 计算数字列表.
        /// </summary>
        static readonly int[] list = new int[50]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
            31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
            41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
        };

        /// <summary>
        /// 获取随机验证码.
        /// </summary>
        /// <returns>item1 - 第一个数字，item2 - 第二个数字，item3 - 运算符（true - 加法， false - 减法）</returns>
        public static Tuple<int, int, bool> GetVCode()
        {
            Random random = new Random();
            var index = random.Next(0, list.Length);
            int num1 = list[index];
            var index2 = random.Next(0, list.Length);
            int num2 = list[index2];
            bool bool_operator = random.Next(0, 2) == 1 ? true : false;
            return new Tuple<int, int, bool>(num1, num2, bool_operator);
        }

        /// <summary>
        /// 绘制验证码的图片.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Bitmap DrawImage(int num1, int num2, bool isAdd)
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
            string code = string.Format("{0}{1}{2}", num1, isAdd ? "+" : "-", num2);
            for (int i = 0; i < code.Length; i++)
            {
                var item = code[i];
                grp.DrawString(item.ToString(),
                    new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold),
                    new SolidBrush(list_color[random.Next(list_color.Length)]),
                    x: (150 / 5) * i,
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
    }

    /// <summary>
    /// VCodeCalculateController控制器中Create方法的返回对象.
    /// </summary>
    public class VCodeCalculateController_Create_Receive : ReceiveObject
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 令牌.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Base64的验证码图片.
            /// </summary>
            public string img { get; set; }
        }
    }
}
