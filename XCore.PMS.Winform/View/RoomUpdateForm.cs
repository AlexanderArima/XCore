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
    public partial class RoomUpdateForm : DSkinForm
    {
        public string roomid { get; set; }

        public RoomUpdateForm(string roomid)
        {
            InitializeComponent();
            this.roomid = roomid;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomUpdateForm_Load(object sender, EventArgs e)
        {
            // 初始化时加载房间类型数据字典
            comboBox1.DataSource = DictionaryInfo_RoomType.list_roomtype;
            if (DictionaryInfo_RoomType.list_roomtype != null && DictionaryInfo_RoomType.list_roomtype.Count > 0)
            {
                comboBox1.DisplayMember = "displayName";  // 显示出来的。Text
                comboBox1.ValueMember = "actualName";        // value值。
            }

            var model = RoomFormViewModel.GetSingle(this.roomid);
            if(model.Item1 == true)
            {
                this.dSkinTextBox1.Text = model.Item2.Name;
                var type = DictionaryInfo_RoomType.list_roomtype.Single(m => m.actualName == model.Item2.Type);
                if(type != null)
                {
                    this.comboBox1.SelectedValue = type.actualName;
                }
            }
            else
            {
                MessageBox.Show(model.Item3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RoomUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
