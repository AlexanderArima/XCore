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
    }
}
