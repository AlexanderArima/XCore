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

namespace XCore.PMS.Winform.View
{
    public partial class RoomAddForm : DSkinForm
    {
        public RoomAddForm()
        {
            InitializeComponent();
        }

        private void RoomAddForm_Load(object sender, EventArgs e)
        {
            // 初始化时加载房间类型数据字典
            DictionaryInfo_RoomType dict = new DictionaryInfo_RoomType();
            var list = dict.GetList();
            comboBox1.DataSource = list;
            if (list != null && list.Count > 0)
            {
                comboBox1.DisplayMember = "displayName";  // 显示出来的。Text
                comboBox1.ValueMember = "actualName";        // value值。
            }
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            var typeid = comboBox1.SelectedValue;
        }

        private void RoomAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
