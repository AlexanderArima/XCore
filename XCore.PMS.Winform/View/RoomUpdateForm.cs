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

            // 初始化时加载房间状态数据字典
            var list = DictionaryInfo_RoomStatus.list_roomstatus;
            var list_after = new List<DictionaryInfo_RoomStatus>();
            list.ForEach(m =>
            {
                if (m.displayName.Equals("使用中") == false)
                {
                    list_after.Add(new DictionaryInfo_RoomStatus()
                    {
                        actualName = m.actualName,
                        displayName = m.displayName
                    });
                }
            });
            comboBox2.DataSource = list_after;
            if (DictionaryInfo_RoomStatus.list_roomstatus != null && DictionaryInfo_RoomStatus.list_roomstatus.Count > 0)
            {
                comboBox2.DisplayMember = "displayName";  // 显示出来的。Text
                comboBox2.ValueMember = "actualName";        // value值。
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

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            var name = this.dSkinTextBox1.Text;
            var type = this.comboBox1.SelectedValue;
            var status = this.comboBox2.SelectedValue;
            var result = RoomFormViewModel.Update(this.roomid, name, type.ToString(), status.ToString());
            if (result.Item1 == false)
            {
                MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
        }
    }
}
