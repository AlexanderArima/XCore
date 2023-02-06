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
using XCore.PMS.Winform.ViewModel;

namespace XCore.PMS.Winform.View
{
    public partial class RoomForm : DSkinForm
    {
        public RoomForm()
        {
            InitializeComponent();
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {
            var result = RoomFormViewModel.GetList();
            if(result.Item1 == true)
            {
                // 查询成功，加载房间列表
                // 我打算使用webBrowser控件，动态生成html代码，因为这样性能更好些
            }
            else
            {
                MessageBox.Show(result.Item3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
