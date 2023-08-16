using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.Web.Models
{
    public class VCodeImageSelectModel
    {
        /// <summary>
        /// 令牌.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 验证码（文字+图片列表）.
        /// </summary>
        public Data code { get; set; } = new Data();

        public class Data
        {
            public string text { get; set; }

            public List<string> image { get; set; }
        }

        /// <summary>
        /// 文字描述
        /// </summary>
        readonly static List<string> List_Text = new List<string>()
        {
            "风扇",
            "风车"
        };

        /// <summary>
        /// 获取随机的文字和验证码.
        /// </summary>
        /// <returns>
        ///  第一个参数 - 图片的类型（文字描述）
        ///  第二个参数 - 图片数组，用来显示图片
        ///  第三个参数 - 图片对应的序号
        ///  第四个参数 - 该类型的图片对应的序号
        /// </returns>
        public static Tuple<string, List<Bitmap>, List<string>, List<string>> GetVCode()
        {
            Random random = new Random();
            var type = random.Next(1, List_Text.Count + 1).ToString();
            var typeName = List_Text.ElementAt(Convert.ToInt32(type) - 1);
            var result = GetVCodeList(type);
            return new Tuple<string, List<Bitmap>, List<string>, List<string>>(typeName, result.Item1, result.Item2, result.Item3);
        }

        /// <summary>
        /// 获取随机的验证码.
        /// </summary>
        /// <param name="type">图片的类型.</param>
        /// <returns>
        ///  第一个参数 - 图片数组，用来显示图片
        ///  第二个参数 - 图片对应的序号
        ///  第三个参数 - 该类型的图片对应的序号
        /// </returns>
        private static Tuple<List<Bitmap>, List<string>, List<string>> GetVCodeList(string type)
        {
            // 这里的随机码是一个有四个元素的数组，如果发现没有生成指定类型的，就重新生成.
            var list_files = Directory.GetFiles(PathHelper.Path + @"Images\imageSelect");
            var count = list_files.Count();
            List<string> list_index = new List<string>();
            var list_fileName = new List<string>();
            List<string> list_selectedIndex = new List<string>();
            Random random = new Random();
            while (true)
            {
                while (true)
                {
                    var index = random.Next(0, count).ToString();
                    if (list_index.Exists(m => m.Equals(index)) == false)
                    {
                        list_index.Add(index);
                        var temp = list_files.ElementAt(Convert.ToInt32(index)).Replace(PathHelper.Path + @"Images\imageSelect", "");    // 只保留文件名，去掉路径
                        list_fileName.Add(temp);
                        if(temp.Replace("\\img", "").Substring(0, 1) == type)
                        {
                            list_selectedIndex.Add(index);
                        }
                    }

                    if (list_index.Count >= 4)
                    {
                        break;
                    }
                }

                // 判断是否至少生成了一个指定类型的图片
                var flag = false;
                flag = list_fileName.Exists(m =>
                {
                    if (m.Contains("img" + type))
                    {
                        return true;
                    }

                    return false;
                });

                if (flag == false)
                {
                    list_index.Clear();
                    list_fileName.Clear();
                    list_selectedIndex.Clear();
                    continue;
                }
                else
                {
                    // 至少生成了一个指定类型的图片
                    break;
                }
            }

            // 加载图片
            List<Bitmap> list_image = new List<Bitmap>();
            for (int i = 0; i < list_fileName.Count; i++)
            {
                var image = Image.FromFile(string.Format(@"{0}Images\imageSelect\{1}", PathHelper.Path, list_fileName.ElementAt(i)));
                list_image.Add((Bitmap)image);
            }

            return new Tuple<List<Bitmap>, List<string>, List<string>>(list_image, list_index, list_selectedIndex);
        }

        /// <summary>
        /// 将图片对象转成Base64的字符串.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static List<string> BitmapToBase64Str(List<Bitmap> bitmap)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < bitmap.Count; i++)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.ElementAt(i).Save(memoryStream, ImageFormat.Jpeg);
                    byte[] bytes = memoryStream.ToArray();
                    list.Add(Convert.ToBase64String(memoryStream.ToArray()));
                }
            }

            return list;
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
            Bitmap bitmap = new Bitmap(85, 50);

            // 创建画笔
            Graphics grp = Graphics.FromImage(bitmap);
            grp.Clear(Color.White);    // 设置背景色为白色

            // 绘制噪点
            for (int i = 0; i < random.Next(30, 40); i++)
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
                    x: (75 / 2) * i,
                    y: random.Next(5));
            }

            // 在验证码上绘制噪点
            for (int i = 0; i < random.Next(15, 25); i++)
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

    public class VCodeImageSelectController_Create_Receive : ReceiveObject
    {
        public Data data { get; set; } = new VCodeImageSelectController_Create_Receive.Data();

        public class Data
        {
            /// <summary>
            /// 令牌.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 描述文字的图片.
            /// </summary>
            public string img_text { get; set; }

            /// <summary>
            /// Base64被裁剪的图片.
            /// </summary>
            public List<string> img { get; set; }

            /// <summary>
            /// 图片的序号.
            /// </summary>
            public List<string> img_index { get; set; }
        }
    }
}
