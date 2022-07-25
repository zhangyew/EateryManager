using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 实力产业管理系统
{
    public partial class WaiterLevel : Form
    {
        public static WaiterLevel waiterLevel;

        public WaiterLevel()
        {
            InitializeComponent();
            waiterLevel = this;
        }

        private void WaiterLevel_Load(object sender, EventArgs e)
        {
            Looking();
        }

        public void Looking()
        {
            lvXX.Items.Clear();
            string sql = $"  SELECT WTID,WTName FROM WaiterType WHERE 1=1 AND IsDaks='0'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["WTID"].ToString();
                lvi.SubItems.Add(r["WTName"].ToString());
                lvXX.Items.Add(lvi);
            }
            // 应用隔行变色
            ChangeListViewColor(lvXX);
        }
        /// <summary>
        /// 隔行变色   
        /// </summary> 
        /// <param name="listView">需要隔行变色的ListView控件名称</param>
        private void ChangeListViewColor(ListView listView)
        {
            listView.OwnerDraw = true;
            listView.DrawColumnHeader += ListView_DrawColumnHeader;
            listView.DrawSubItem += ListView_DrawSubItem;
        }

        /// <summary>
        /// 绘制表头[避免表头消失]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawText(TextFormatFlags.Default);
        }

        /// <summary>
        /// 绘制隔行变色列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                //选中时的颜色
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(198, 219, 181)), e.Bounds);
            }
            else
            {
                //其他列执行隔行变色
                if (e.ItemIndex % 2 == 0)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 255, 231)), e.Bounds);
                else
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), e.Bounds);
            }
            e.DrawText(TextFormatFlags.Default);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateWaiterLevel tj = new UpdateWaiterLevel();
            string id = lvXX.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddWaiterLevel tj = new AddWaiterLevel();
            tj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该服务生等级消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE WaiterType SET IsDaks='1'WHERE WTID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }
    }
}
