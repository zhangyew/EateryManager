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
    public partial class CustomerBilling : Form
    {
        /// <summary>
        /// 房间编号
        /// </summary>
        public string Id { get; set; }
        public CustomerBilling()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 面板边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.Green, 1, ButtonBorderStyle.Solid, Color.Green, 1, ButtonBorderStyle.Solid, Color.Green, 1, ButtonBorderStyle.Solid, Color.Green, 1, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 点击确定将选择的值输入到文本框中，并隐藏控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (lvGLY.SelectedItems.Count == 0)
            {
                return;
            }
            string x = lvGLY.SelectedItems[0].SubItems[0].Text;
            string sql = $"  SELECT UserID,UserName,TrueName FROM Admins WHERE 1=1 AND IsDelete='0' AND UserID='{x}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                txtSYY.Text = r["TrueName"].ToString();
                txtYXRY.Text = r["UserID"].ToString();
            }
            PFWS1.Visible = false;
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerBilling_Load(object sender, EventArgs e)
        {
            Looking();
        }
        /// <summary>
        /// 加载信息
        /// </summary>
        private void Looking()
        {
            //加载隐藏控件信息
            lvGLY.Items.Clear();
            string sql = $"  SELECT UserID,UserName,TrueName FROM Admins WHERE 1=1 AND IsDelete='0'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["UserID"].ToString();
                lvi.SubItems.Add(r["UserName"].ToString());
                lvi.SubItems.Add(r["TrueName"].ToString());
                lvGLY.Items.Add(lvi);
            }
            string sql2 = $"  SELECT A.TableName,B.RTMin,B.RTName FROM Tables A INNER JOIN RoomType B ON B.RTID=A.RTID WHERE 1=1 AND A.IsDelete='0'AND B.IsDelete='0' AND A.TableID='{Id}'";
            SqlDataReader x = DBHpler.ExecuteReader(sql2);
            while (x != null && x.HasRows && x.Read())
            {
                lblBH.Text = x["TableName"].ToString();
                lblXF.Text = "￥" + x["RTMin"].ToString() + ".00";
                lblLX.Text = x["RTName"].ToString();

            }
            ChangeListViewColor(lvGLY);
        }
        /// <summary>
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
        /// <summary>
        /// 开单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string rq = DateTime.Now.ToString("yyyyMMdd");
            Random rd = new Random();
            string bh = "ZY" + $"{rq}" + rd.Next(1, 9999);
            string rs = txtRS.Text;
            string bz = txtBZ.Text;
            string syy = txtYXRY.Text;

            string sql = $"INSERT INTO OrderInfo(OIID, TID, PeopleNum,UID,Remart,IsDaks)VALUES('{bh}','{Id}','{rs}','{syy}','{bz}','0')";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                string sql2 = $" UPDATE Tables SET TableState='1'WHERE 1=1 AND TableID='{Id}'";
                bool x = DBHpler.ExecuteNonQuery(sql2);
                if (x)
                {
                    if (MainForm.mainForm != null)
                    {
                        MainForm.mainForm.BinRoomType();
                    }
                }
                //开单成功后打开增加消费界面，并将所获取的餐台号传给增加消费界面 

                if (cbXF.Checked == true)
                {
                    AddConsume tj = new AddConsume();
                    tj.id = Id;
                    // tj.name = lblBH.Text;
                    tj.ShowDialog();
                    this.Hide();
                }
                this.Hide();
            }
        }
        /// <summary>
        /// 数字判断与面板隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void txtRS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }

        private void CustomerBilling_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = false;
        }

        private void lvGLY_DoubleClick(object sender, EventArgs e)
        {
            if (lvGLY.SelectedItems.Count == 0)
            {
                return;
            }
            string x = lvGLY.SelectedItems[0].SubItems[0].Text;
            string sql = $"  SELECT UserID,UserName,TrueName FROM Admins WHERE 1=1 AND IsDelete='0' AND UserID='{x}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                txtSYY.Text = r["TrueName"].ToString();
                txtYXRY.Text = r["UserID"].ToString();
            }
            PFWS1.Visible = false;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = false;
        }
        /// <summary>
        /// 显示面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFWS_Enter(object sender, EventArgs e)
        {
            PFWS1.Visible = true;
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = false;
        }
        /// <summary>
        /// 点击按钮显示隐藏控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = true;

        }
        private void button5_Click(object sender, EventArgs e)
        {
            PFWS1.Visible = true;
        }
        #endregion

       
    }
}
