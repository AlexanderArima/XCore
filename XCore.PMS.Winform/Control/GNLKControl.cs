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
    }
}
