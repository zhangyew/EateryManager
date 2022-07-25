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
    public partial class ScheduledFrom : Form
    {
        public static ScheduledFrom scheduledFrom;
        public ScheduledFrom()
        {
            InitializeComponent();
            scheduledFrom = this;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScheduledFrom_Load(object sender, EventArgs e)
        {
            //加载预定信息
            Looking();
            ChangeListViewColor(lvXX);
        }

        public void Looking()
        {
            lvXX.Items.Clear();
            string zt = "";
            string tj = txtTJ.Text;
            string sql = $"SELECT A.Name,A.Phone,B.TableName,D.RTName,A.Status,A.UpdateDate,A.EstimateDate,A.Remark,C.UserName,a.ROID FROM ReserveOrder A INNER JOIN Tables B ON B.TableID=A.TID INNER JOIN Admins C ON C.UserID=A.UID INNER JOIN RoomType D ON D.RTID=B.RTID WHERE 1=1 ";
            if (tj != null)
            {
                sql += $" AND A.Name LIKE '%{tj}%' OR A.Phone LIKE '%{tj}%' OR B.TableName LIKE '%{tj}%'";

            }
            if (rbtQX.Checked == true)
            {
                sql += $"OR A.Status='2'";

            }
            if (rbtDD.Checked == true)
            {
                sql += $"OR A.Status='1'";
            }
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["Name"].ToString();
                lvi.SubItems.Add(r["Phone"].ToString());
                lvi.SubItems.Add(r["TableName"].ToString());
                lvi.SubItems.Add(r["RTName"].ToString());
                int z =Convert.ToInt32(r["Status"]);
                switch (z)
                {
                    case 0:
                        zt = "未到店";
                        break;
                    case 1:
                        zt = "已到店";
                        break;
                    case 2:
                        zt = "已取消";
                        break;
                    case 3:
                        zt = "准备中";
                        break;
                    default:
                        break;
                }
                lvi.SubItems.Add(zt.ToString());
                lvi.SubItems.Add(r["UpdateDate"].ToString());
                lvi.SubItems.Add(r["EstimateDate"].ToString());
                lvi.SubItems.Add(r["Remark"].ToString());
                lvi.SubItems.Add(r["UserName"].ToString());
                lvi.SubItems.Add(r["ROID"].ToString());
                lvXX.Items.Add(lvi);

            }
            if (r != null)
                r.Close();

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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ScheAddDuledFrom tj = new ScheAddDuledFrom();
            tj.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {


            if (lvXX.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要修改预定的信息","提示");
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[9].Text;
            string sql = $" SELECT Status FROM ReserveOrder WHERE 1=1 AND ROID='{id}'";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (x > 0)
            {
                MessageBox.Show("请对‘未到店’的宾客信息进行选择", "提示信息");
                return;
            }
            ScheUpdateDuledFrom tj = new ScheUpdateDuledFrom();
            tj.id = id;
            tj.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[9].Text;
            string sq2l = $" SELECT Status FROM ReserveOrder WHERE 1=1 AND ROID='{id}'";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sq2l));
            if (x > 0)
            {
                MessageBox.Show("请对‘未到店’的宾客信息进行选择", "提示信息");
                return;
            }
            DialogResult r= MessageBox.Show("确定要取消选中的预定记录吗？","信息提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

            if (lvXX.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要取消的预定信息", "提示");
                return;
            }
            if (DialogResult.Yes == r)
            {
               
                string sql = $" DELETE ReserveOrder WHERE 1=1 AND ROID='{id}'";
                DBHpler.ExecuteNonQuery(sql);
                Looking();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择已到达的预定信息", "提示");
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[9].Text;
            string sql = $" SELECT Status FROM ReserveOrder WHERE 1=1 AND ROID='{id}'";
            int x=Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (x > 0)
            {
                MessageBox.Show("请对‘未到店’的宾客信息进行选择","提示信息");
                return;
            }
            ScheDaoDaDuledFrom tj = new ScheDaoDaDuledFrom();
            tj.id = id;
            tj.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Looking();
        }
    }
}
