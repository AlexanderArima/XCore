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
using XCore.PMS.Winform.ViewModel;

namespace XCore.PMS.Winform.Control
{
    public partial class YDDLKControl : DSkinUserControl
    {
        int _pageIndex = 0;

        int _pageSize = 10;

        /// <summary>
        /// 绑定数据到表单中.
        /// </summary>
        public Action<YDDLKControlViewModel> BindFormAction { get; set; }

        public YDDLKControl()
        {
            InitializeComponent();
        }

        private void dSkinButton3_Click(object sender, EventArgs e)
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

                var result = YDDLKControlViewModel.Delete(list);
                if (result.Item1 == false)
                {
                    MessageBox.Show(result.Item2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Query();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("YDDLKControl：dSkinButton3_Click出错，" + ex.Message, ex);
            }
        }

        public void Query()
        {
            try
            {
                var result = YDDLKControlViewModel.Query(this._pageIndex, this._pageSize);
                if (result == null)
                {
                    MessageBox.Show("系统异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var list_room = RoomFormViewModel.GetList().Item2;
                DictionaryInfo_ZJLX dict_zjlx = new DictionaryInfo_ZJLX();
                var list_zjlx = dict_zjlx.GetList();
                var list = result.Item2;
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list.ElementAt(i);
                    if(item.Type != null)
                    {
                        switch (item.Type)
                        {
                            case "1":
                                item.GJ = "中国";
                                break;

                            case "2":
                                item.GJ = "";
                                break;
                        }
                    }

                    item.ZJLX = list_zjlx.Find(m => m.actualName == item.ZJLX).displayName;
                    item.XB = item.XB == "1" ? "男" : "女";
                    item.FJHM = list_room.Find(m => m.ID == item.FJHM).Name;
                }

                this.dSkinDataGridView1.DataSource = result.Item2;
                this.dSkinDataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("YDDLKControl：Query出错，" + ex.Message, ex);
            }
        }

        private void YDDLKControl_Load(object sender, EventArgs e)
        {
            this.Query();
        }

        /// <summary>
        /// 单元格双击事件
        /// </summary>
        private void dSkinDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 单元格单击事件
        /// </summary>
        private void dSkinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dSkinDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            var rows = this.dSkinDataGridView1.Rows;
            if(index < 0)
            {
                return;
            }

            DataGridViewCheckBoxCell chk = rows[index].Cells[0] as DataGridViewCheckBoxCell;
            if (chk.Value == null)
            {
                chk.Value = true;
            }
            else
            {
                chk.Value = !Convert.ToBoolean(chk.Value);
            }
        }

        private void dSkinDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            var rows = this.dSkinDataGridView1.Rows;
            var id = rows[index].Cells[1].Value.ToString();

            // 查询订单详情
            var result = YDDLKControlViewModel.QuerySingle(id);
            if(result.Item1 == false)
            {
                MessageBox.Show(result.Item3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(this.BindFormAction != null)
            {
                this.BindFormAction(result.Item2);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chk = this.checkBox1.Checked;
            var rows = this.dSkinDataGridView1.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                var item = rows[i].Cells[0];
                item.Value = chk;
            }
        }
    }
}
