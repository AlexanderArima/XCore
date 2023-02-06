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
            var typeid = comboBox1.SelectedValue.ToString();
            var name = this.dSkinTextBox1.Text;
            var result = RoomFormViewModel.Add(name, typeid);
            if (result.Item1 == false)
            {
                MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void RoomAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
