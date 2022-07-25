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
    public partial class EmployeeInForm : Form
    {
        public static EmployeeInForm employeeInForm;
        public EmployeeInForm()
        {
            InitializeComponent();
            employeeInForm = this;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EmployeeInForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT WTID,WTName FROM WaiterType where 1=1 AND IsDaks='0' UNION SELECT 0,'所有服务生' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboDJ.DisplayMember = "WTName";
            cboDJ.ValueMember = "WTID";
            cboDJ.DataSource = table;
            Looking();
            //应用隔行变色
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
        public void Looking()
        {
            lvXX.Items.Clear();
            int x = cboDJ.SelectedIndex;
            string sql = $"SELECT a.WID,a.WName,a.WNameSimple,b.WTName,a.Sex,a.Phone FROM WaiterInfo A INNER JOIN WaiterType B ON B.WTID=A.WID WHERE 1=1 and A.IsDaks='0'";
            if (x != 0)
            {
                sql +=$" AND B.WTID='{x}'";
            }
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["WID"].ToString();
                lvi.SubItems.Add(r["WName"].ToString());
                lvi.SubItems.Add(r["WNameSimple"].ToString());
                lvi.SubItems.Add(r["WTName"].ToString());
                lvi.SubItems.Add(r["Sex"].ToString());
                lvi.SubItems.Add(r["Phone"].ToString());
                lvXX.Items.Add(lvi);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddWaiterFrom tj = new AddWaiterFrom();
            tj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateWaiteFrom tj = new UpdateWaiteFrom();
            string id = lvXX.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void cboDJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            Looking();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该服务生消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE WaiterInfo SET IsDaks='1'WHERE WID='{id}'";
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

        private void button10_Click(object sender, EventArgs e)
        {
            WaiterLevel tj = new WaiterLevel();
            tj.ShowDialog();
        }
    }
}
