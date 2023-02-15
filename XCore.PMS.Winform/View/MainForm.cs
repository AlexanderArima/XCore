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

namespace XCore.PMS.Winform.View
{
    public partial class MainForm : DSkinForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 房间管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 打开房间管理画面
            RoomForm form = new RoomForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {

        }

        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void 房间管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void 房间管理ToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            // 打开房间管理画面
            RoomForm form = new RoomForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }
    }
}
