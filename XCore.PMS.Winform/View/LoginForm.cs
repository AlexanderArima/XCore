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
    public partial class LoginForm : DSkinForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            string username = this.dSkinTextBox1.Text;
            string password = this.dSkinTextBox2.Text;
            LoginFormViewModel model = new LoginFormViewModel();
            model.UserName = username;
            model.Password = password;
            var result = model.Login();
            if (result.Item1)
            {
                MessageBox.Show("登录成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
