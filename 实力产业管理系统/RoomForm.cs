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
    public partial class RoomForm : Form
    {
        public static RoomForm roomForm;

        public RoomForm()
        {
            InitializeComponent();
            roomForm = this;
        }
        //主窗体加载事件
        private void RoomForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT RTID,RTName FROM RoomType where IsDelete=0  UNION SELECT 0,'全部房间' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboLX.DisplayMember = "RTName";
            cboLX.ValueMember = "RTID";
            cboLX.DataSource = table;
            Looking();
            Looking2();
            //应用隔行变色
            ChangeListViewColor(lvCT);
            ChangeListViewColor(lvFJ);
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
        public void Looking2()
        {
            lvCT.Items.Clear();

            int lx = cboLX.SelectedIndex;
            string xx = "";
            string sql = @" SELECT A.TableName,B.RTName,A.TableState,A.TableArea,a.TableID FROM Tables A INNER JOIN RoomType B ON B.RTID=A.RTID WHERE 1=1  and a.IsDelete='0'";
            if (lx != 0)
            {
                sql += $" AND b.RTID='{lx}'";
            }
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["TableName"].ToString();
                lvi.SubItems.Add(r["RTName"].ToString());
                int zy = Convert.ToInt32(r["TableState"]);
                if (zy == 0)
                {
                    xx = "可用";
                }
                else if (zy == 1)
                {
                    xx = "占用";
                }
                else if (zy == 2)
                {
                    xx = "预定";
                }
                else if (zy == 3)
                {
                    xx = "停用";
                }
                lvi.SubItems.Add(xx);
                lvi.SubItems.Add(r["TableArea"].ToString());
                lvi.SubItems.Add(r["TableID"].ToString());
                lvCT.Items.Add(lvi);
            }
        }

        public void Looking()
        {
            lvFJ.Items.Clear();
            string sql = @" SELECT RTID,RTName,RTMin,RTIsDis,RTNum FROM  RoomType WHERE 1=1 and IsDelete='0'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["RTID"].ToString();
                lvi.SubItems.Add(r["RTName"].ToString());
                lvi.SubItems.Add(r["RTMin"].ToString());
                string sex = Convert.ToInt32(r["RTIsDis"]) == 0 ? "是" : "否";
                lvi.SubItems.Add(sex);
                lvi.SubItems.Add(r["RTNum"].ToString());
                lvFJ.Items.Add(lvi);
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cboLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            Looking2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            //if (lvFJ.SelectedItems.Count == 0)
            //{
            //    return;
            //}
            AddRoomForm tj = new AddRoomForm();
            //string id = lvFJ.SelectedItems[0].SubItems[0].Text;
            //tj.ID = id;
            tj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvFJ.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateRoomForm tj = new UpdateRoomForm();
            string id = lvFJ.SelectedItems[0].SubItems[0].Text;
            tj.ID = id;
            tj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult r= MessageBox.Show("是否删除这条消息？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvFJ.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvFJ.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE RoomType SET IsDelete='1'WHERE RTID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking();
                string sql1 = $"  SELECT RTID,RTName FROM RoomType where IsDelete=0  UNION SELECT 0,'全部房间' ";
                DataTable table = DBHpler.GetTable(sql1);
                cboLX.DisplayMember = "RTName";
                cboLX.ValueMember = "RTID";
                cboLX.DataSource = table;
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddTableForm tj = new AddTableForm();
            tj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lvCT.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateTableForm tj = new UpdateTableForm();
            string id = lvCT.SelectedItems[0].SubItems[4].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除这条消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvCT.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvCT.SelectedItems[0].SubItems[4].Text;
            string sql = $" UPDATE Tables SET IsDelete='1'WHERE TableID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking();
                string sql1 = $"  SELECT RTID,RTName FROM RoomType where IsDelete=0  UNION SELECT 0,'全部房间' ";
                DataTable table = DBHpler.GetTable(sql1);
                cboLX.DisplayMember = "RTName";
                cboLX.ValueMember = "RTID";
                cboLX.DataSource = table;
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }
    }

}

