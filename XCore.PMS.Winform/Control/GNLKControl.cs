using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore.PMS.Winform.Common;
using XCore.PMS.Winform.Model;
using XCore.PMS.Winform.ViewModel;

namespace XCore.PMS.Winform.Control
{
    public partial class GNLKControl : UserControl
    {
        public GNLKControl()
        {
            InitializeComponent();

            this.yddlkControl1.BindFormAction += (YDDLKControlViewModel obj) =>
            {
                this.dSkinTextBox1.Text = obj.XM;
                this.comboBox1.SelectedValue = obj.ZJLX;
                this.dSkinTextBox2.Text = obj.ZJHM;
                if(obj.XB == "1")
                {
                    this.dSkinRadioButton1.Checked = true;
                }
                else
                {
                    this.dSkinRadioButton2.Checked = true;
                }

                this.dSkinDateTimePicker1.Text = obj.CSRQ;
                this.comboBox2.SelectedValue = obj.FJHM;
                this.dSkinTextBox4.Text = obj.XZ;
                this.dSkinLabel9.Text = obj.RZSJ;
            };
        }

        /// <summary>
        /// 旅客预定.
        /// </summary>
        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            var name = this.dSkinTextBox1.Text;
            var zjlx = this.comboBox1.SelectedValue.ToString();
            var zjhm = this.dSkinTextBox2.Text;
            string xb;
            if(this.dSkinRadioButton1.Checked == true)
            {
                xb = "1";
            }
            else
            {
                xb = "2";
            }

            var csrq = this.dSkinDateTimePicker1.Text;
            var rzfh = this.comboBox2.SelectedValue.ToString();
            var xz = this.dSkinTextBox4.Text;
            GNLKControlViewModel model = new GNLKControlViewModel();
            model.xm = name;
            model.zjlx = zjlx;
            model.zjhm = zjhm;
            model.sex = xb;
            model.birthday = csrq;
            model.roomid = rzfh;
            model.address = xz;
            var zjz_image = this.dSkinPictureBox1.Image;
            var pathName = string.Format("{0}Images\\", PathHelper.ApplicationPath);
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var bool_image = ImageHelper.Save(pathName, fileName, zjz_image, ImageFormat.Jpeg);
            if(bool_image == false)
            {
                MessageBox.Show("转换证件照失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var zjz_base64 = ImageHelper.GetImgByte(pathName + fileName);
            model.zjz = zjz_base64;
            var result = model.Appoint();
            if(result.Item1 == false)
            {
                MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("预定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GNLKControl_Load(object sender, EventArgs e)
        {
            // 初始化证件类型
            DictionaryInfo_ZJLX dict2 = new DictionaryInfo_ZJLX();
            var list2 = dict2.GetList();
            comboBox1.DataSource = list2;
            if (list2 != null && list2.Count > 0)
            {
                comboBox1.DisplayMember = "displayName";  // 显示出来的。Text
                comboBox1.ValueMember = "actualName";        // value值。
            }

            // 初始化房间号
            var result = RoomFormViewModel.GetList();
            if (result.Item1 == true)
            {
                var list = result.Item2;
                comboBox2.DataSource = list;
                if (list != null && list.Count > 0)
                {
                    comboBox2.DisplayMember = "name";  // 显示出来的。Text
                    comboBox2.ValueMember = "id";        // value值。
                }
            }

            this.dSkinTextBox1.Text = "张三";
            this.dSkinTextBox2.Text = "420101199909091111";
            this.dSkinDateTimePicker1.Text = "1999-09-09";
            this.dSkinTextBox4.Text = "2233";
        }

        private void dSkinTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.dSkinTabControl1.SelectedIndex)
            {
                case 0:
                    this.yddlkControl1.Query();
                    break;
                case 1:
                    this.zzlkControl1.Query();
                    break;
            }
        }
    }
}
