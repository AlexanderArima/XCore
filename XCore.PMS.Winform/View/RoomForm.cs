using DSkin.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore.PMS.Winform.Model;
using XCore.PMS.Winform.ViewModel;

namespace XCore.PMS.Winform.View
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)] // C#调用Js要带上这个
    public partial class RoomForm : DSkinForm
    {
        public RoomForm()
        {
            InitializeComponent();

            string path = PathHelper.ApplicationPath;
            this.webBrowser1.Url = new Uri(path + "Html\\RoomForm.html");
            this.webBrowser1.ObjectForScripting = this;  // 这句必须，不然js不能调用C#

            // 注册页面事件
            HtmlDocument document = this.webBrowser1.Document;
            var windows = document.Window;
            windows.AttachEventHandler("onload", new EventHandler(this.Query));
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {
            var result = RoomFormViewModel.GetList();
            if (result.Item1 == true)
            {
                // 查询成功，加载房间列表
                // 我打算使用webBrowser控件，动态生成html代码，因为这样性能更好些
            }
            else
            {
                MessageBox.Show(result.Item3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 窗体初始化加载事件.
        /// </summary>
        public void Query(object sender, EventArgs e)
        {
            var result = RoomFormViewModel.GetList();
            if(result.Item1 == false)
            {
                MessageBox.Show(result.Item3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var list = result.Item2;
            HtmlDocument document = this.webBrowser1.Document;
            var div_main = document.GetElementById("div_main");
            div_main.InnerHtml = string.Empty;
            var string_item = string.Empty;
            var list_roomtype = DictionaryInfo_RoomType.list_roomtype;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var sfzz_classname = string.Empty;
                var kxfj_checkbox = string.Empty;
                var sfzz_tagname = string.Empty;
                if (item.Statue == "1")
                {
                    // 空闲
                    sfzz_classname = "div_unuse";
                    sfzz_tagname = "tag_unuse";
                    kxfj_checkbox = "<input type='checkbox' value='" + item.ID + "' />";
                }
                else if (item.Statue == "2")
                {
                    // 在住
                    sfzz_classname = "div_use";
                    sfzz_tagname = "tag_use";
                }
                else if (item.Statue == "3")
                {
                    // 脏房
                    sfzz_classname = "div_dirty";
                    sfzz_tagname = "tag_dirty";
                }
                else if (item.Statue == "4")
                {
                    // 维修
                    sfzz_classname = "div_repair";
                    sfzz_tagname = "tag_repair";
                }

                var type = list_roomtype.Single(m => m.actualName == item.Type).displayName.Substring(0, 1);
                if(type == null)
                {
                    Log4NetHelper.Error("未知的房间类型");
                    continue;
                }

                string_item = string_item + "<div class='div_item " + sfzz_tagname + "' id='div_item" + (i + 1) + "'><div class='div_head " + sfzz_classname + "'>" + kxfj_checkbox + "</div ><div class='div_title'>" + item.Name + "</div><div class='div_attr'>" + type + "</div></div>";
            }

            div_main.InnerHtml = string_item;
        }

        private void 添加房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomAddForm form = new RoomAddForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void RoomForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void 修改房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomUpdateForm form = new RoomUpdateForm("3");
            if(form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 删除房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = RoomFormViewModel.Delete("4");
            if(result.Item1 == true)
            {
                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
