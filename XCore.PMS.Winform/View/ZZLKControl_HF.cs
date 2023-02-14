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
    public partial class ZZLKControl_HF : DSkinForm
    {
        string _id;

        public ZZLKControl_HF(string id)
        {
            InitializeComponent();
            _id = id;
        }

        private void ZZLKControl_HF_Load(object sender, EventArgs e)
        {

        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = ZZLKControlViewModel.ChangeRoom(this._id, this.comboBox1.SelectedValue.ToString());
                if (result.Item1 == false)
                {
                    MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("换房成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("ZZLKControl_HF：dSkinButton1_Click出错，" + ex.Message, ex);
            }
        }

        private void ZZLKControl_HF_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
