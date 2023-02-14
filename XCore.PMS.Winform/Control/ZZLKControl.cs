using DSkin.Controls;
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
using XCore.PMS.Winform.View;
using XCore.PMS.Winform.ViewModel;

namespace XCore.PMS.Winform.Control
{
    public partial class ZZLKControl : DSkinUserControl
    {
        int _pageIndex = 1;

        int _pageSize = 10;

        public ZZLKControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化控件加载.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZZLKControl_Load(object sender, EventArgs e)
        {
            this.Query();
        }

        private void Query()
        {
            try
            {
                var result = ZZLKControlViewModel.Query(this._pageIndex, this._pageSize);
                if (result == null)
                {
                    MessageBox.Show("系统异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.dSkinDataGridView1.DataSource = result;
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("ZZLKControl：Query出错，" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 退房.
        /// </summary>
        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < this.dSkinDataGridView1.Rows.Count; i++)
                {
                    var item = this.dSkinDataGridView1.Rows[i];
                    if (Convert.ToBoolean(item.Cells[0].Value) == true)
                    {
                        var id = item.Cells[1].Value.ToString();
                        list.Add(id);
                    }
                }

                if (list.Count < 1)
                {
                    MessageBox.Show("请勾选一行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = ZZLKControlViewModel.Checkout(list);
                if (result.Item1 == false)
                {
                    MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("退房成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Query();
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("ZZLKControl：dSkinButton1_Click出错，" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 换房.
        /// </summary>
        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < this.dSkinDataGridView1.Rows.Count; i++)
                {
                    var item = this.dSkinDataGridView1.Rows[i];
                    if (Convert.ToBoolean(item.Cells[0].Value) == true)
                    {
                        var id = item.Cells[1].Value.ToString();
                        list.Add(id);
                    }
                }

                if (list.Count > 1)
                {
                    MessageBox.Show("换房操作只能勾选单条", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (list.Count < 1)
                {
                    MessageBox.Show("请勾选一行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ZZLKControl_HF form = new ZZLKControl_HF(list[0]);
                if(form.ShowDialog() == DialogResult.OK)
                {
                    form.Dispose();
                }

                this.Query();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("ZZLKControl：dSkinButton1_Click出错，" + ex.Message, ex);
            }
        }
    }
}
