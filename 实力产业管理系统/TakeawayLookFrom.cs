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
    public partial class TakeawayLookFrom : Form
    {
        public TakeawayLookFrom()
        {
            InitializeComponent();
        }

        private void TakeawayLookFrom_Load(object sender, EventArgs e)
        {
            Looking();
            ChangeListViewColor(lvXX);
        }
        /// 隔行变色   应用隔行变色
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
        private void Looking()
        {
           
            string tj = txtTJ.Text;
            string sql = $"SELECT TOIID,OpenDate,OrderTotal,Remart,Name,Phone,Address FROM TakeOutOrderInfo WHERE 1=1 and IsDaks='1'";
            if (tj != null)
            {
                sql += $" AND Name LIKE '%{tj}%' OR TOIID LIKE '%{tj}%' OR Phone LIKE '%{tj}%'";
            }
            lvXX.Items.Clear();
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null&&r.HasRows&&r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["TOIID"].ToString();
                lvi.SubItems.Add(r["OpenDate"].ToString());
                lvi.SubItems.Add(r["OrderTotal"].ToString());
                lvi.SubItems.Add(r["Remart"].ToString());
                lvi.SubItems.Add(r["Name"].ToString());
                lvi.SubItems.Add(r["Phone"].ToString());
                lvi.SubItems.Add(r["Address"].ToString());
                lvXX.Items.Add(lvi);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Looking();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Looking();
        }

        private void txtTJ_TextChanged(object sender, EventArgs e)
        {
            Looking();
        }
    }
}
