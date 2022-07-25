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
    public partial class TakeawayFrom : Form
    {
        public TakeawayFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 隐藏面板显隐控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            plSP.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            plSP.Visible = false;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            plSP.Visible = false;
            paXG.Visible = false;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            plSP.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            plSP.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            plSP.Visible = true;
        }
        private void lvQD_Click(object sender, EventArgs e)
        {
            plSP.Visible = false;
            if (lvQD.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvQD.SelectedItems[0].SubItems[0].Text;
            lblBZ.Text = $"“{id}”" + "的备注: ***";
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
        #endregion
        private void TakeawayFrom_Load(object sender, EventArgs e)
        {

            //加载流水单号
            string unm = $"WZY{DateTime.Now.ToString("yyyyMMdd")}{new Random().Next(1000, 10000)}";
            txtDH.Text = unm;
            //加载隐藏面板信息
            LookingYC();
            ChangeListViewColor(lvSP);
            ChangeListViewColor(lvQD);
        }
        /// <summary>
        /// 加载隐藏面板信息
        /// </summary>
        private void LookingYC()
        {
            lvSP.Items.Clear();
            string mc = txtSP.Text;
            string sql = $"SELECT GID,GName,BasePrice,GoodsStock FROM GoodsInfo WHERE 1=1 AND IsDaks='0'";
            if (string.IsNullOrEmpty(mc))
            {
                sql += $" AND GID LIKE '%{mc}%' AND GName LIKE '%{mc}%' AND GNameSimple LIKE '%{mc}%' ";
            }
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["GID"].ToString();
                lvi.SubItems.Add(r["GName"].ToString());
                double dj = double.Parse(r["BasePrice"].ToString());
                lvi.SubItems.Add(dj.ToString("f2"));
                lvi.SubItems.Add(r["GoodsStock"].ToString());
                lvSP.Items.Add(lvi);
            }
        }
        /// <summary>
        /// 双击添加商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSP_DoubleClick(object sender, EventArgs e)
        {
            Addxx();
            plSP.Visible = false;
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        private void Addxx()
        {
            string sl = nudSL.Value.ToString();
            string bh = txtDH.Text;
            //查询订单是否存在
            string sql2 = $" SELECT COUNT(TOIID) FROM TakeOutOrderInfo WHERE 1=1 AND TOIID='{bh}'";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            if (x == 0)
            {
                //添加一条订单
                string sql = $" INSERT INTO TakeOutOrderInfo(TOIID, Status,OpenDate, UID,IsDaks)VALUES('{bh}','1',GETDATE(),'1','0')";
                bool r = DBHpler.ExecuteNonQuery(sql);
                //添加商品信息
                if (!r)
                {
                    MessageBox.Show("外卖订单添加失败！");
                }
            }
            if (lvSP.SelectedItems.Count == 0)
            {
                return;
            }
            string sp = lvSP.SelectedItems[0].SubItems[0].Text;
            string j = lvSP.SelectedItems[0].SubItems[2].Text;
            double ex = double.Parse(j);
            double xe = double.Parse(sl);
            double je = ex * xe;
            //添加订单详情
            string sql3 = $"INSERT INTO TakeOutOrderDetails(TOIID, GID, BuyNum, Subtotal, UpdateTime,IsDake)VALUES('{bh}','{sp}','{sl}','{je}',GETDATE(),'0')";
            DBHpler.ExecuteNonQuery(sql3);
            //菜品库存减少对应数字
            //string sql1 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock - '{sl}') WHERE 1 = 1 AND GID = '{sp}'";
            //bool g = DBHpler.ExecuteNonQuery(sql1);
            //if (g)
            //{
            //    Lookingxx();
            //}
            Lookingxx();
            plSP.Visible = false;
        }
        /// <summary>
        /// 点击确定加菜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Addxx();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            paXG.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sl = txtSL.Text;

            if (lvQD.SelectedItems.Count == 0)
            {
                return;
            }
            DialogResult r = MessageBox.Show("真的要删除此菜品嘛？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            string id = lvQD.SelectedItems[0].SubItems[7].Text;
            string sql = $" UPDATE TakeOutOrderDetails SET IsDake='1'WHERE TODID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                string bh = txtDH.Text;
                string sql5 = $" SELECT SUM(Subtotal) FROM TakeOutOrderDetails WHERE 1=1 AND TOIID='{bh}' AND IsDake='0'";
                string k = DBHpler.ExecuteScalar(sql5).ToString();
                //string sql2 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock + '{sl}') WHERE 1 = 1 AND GID = '{bh}'";
                //DBHpler.ExecuteNonQuery(sql2);
                //Lookingxx();
                //LookingYC();

                try
                {
                    double m = double.Parse(k);
                    lblJE.Text = m.ToString("F2");
                }
                catch (Exception)
                {
                }
                Lookingxx();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void Lookingxx()
        {
            //查询订单信息
            lvQD.Items.Clear();
            string bh = txtDH.Text;
            string sql4 = $"SELECT B.GName,B.BasePrice,B.IsDiscount,A.BuyNum,A.Subtotal,C.UserName,A.UpdateTime,a.TODID FROM TakeOutOrderDetails A INNER JOIN GoodsInfo B ON A.GID=B.GID INNER JOIN Admins C INNER JOIN TakeOutOrderInfo D ON C.UserID=D.UID ON A.TOIID=D.TOIID WHERE 1=1 AND D.TOIID='{bh}' AND A.IsDake='0'";
            SqlDataReader w = DBHpler.ExecuteReader(sql4);
            while (w != null && w.HasRows && w.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w["GName"].ToString();
                string dj = w["BasePrice"].ToString();
                double djj = double.Parse(dj);
                lvi.SubItems.Add(djj.ToString("F2"));
                lvi.SubItems.Add(w["IsDiscount"].ToString());
                lvi.SubItems.Add(w["BuyNum"].ToString());
                string n = w["Subtotal"].ToString();
                double jje = double.Parse(n);
                lvi.SubItems.Add(jje.ToString("F2"));
                lvi.SubItems.Add(w["UserName"].ToString());
                lvi.SubItems.Add(w["UpdateTime"].ToString());
                lvi.SubItems.Add(w["TODID"].ToString());
                lvQD.Items.Add(lvi);
            }
            try
            {
                string sql5 = $" SELECT SUM(Subtotal) FROM TakeOutOrderDetails WHERE 1=1 AND TOIID='{bh}'AND IsDake='0'";
                string k = DBHpler.ExecuteScalar(sql5).ToString();
                double m = double.Parse(k);
                lblJE.Text = m.ToString("F2");
            }
            catch (Exception)
            {
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            paXG.Visible = true;
            if (lvQD.SelectedItems.Count == 0)
            {
                return;
            }
            lblMC.Text = lvQD.SelectedItems[0].SubItems[0].Text;
            txtSL.Text = lvQD.SelectedItems[0].SubItems[3].Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (lvQD.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvQD.SelectedItems[0].SubItems[7].Text;
            string sl = txtSL.Text;
            string j = lvSP.SelectedItems[0].SubItems[2].Text;
            double ex = double.Parse(j);
            double xe = double.Parse(sl);
            double je = ex * xe;
            paXG.Visible = false;
            string sql = $"UPDATE TakeOutOrderDetails SET BuyNum='{sl}',Subtotal='{je}' WHERE 1=1 AND TODID='{id}'";
            DBHpler.ExecuteNonQuery(sql);
            Lookingxx();
        }

        private void lvQD_DoubleClick(object sender, EventArgs e)
        {
            paXG.Visible = true;
            if (lvQD.SelectedItems.Count == 0)
            {
                return;
            }
            lblMC.Text = lvQD.SelectedItems[0].SubItems[0].Text;
            txtSL.Text = lvQD.SelectedItems[0].SubItems[3].Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TakeawayCheckoutFrom tj = new TakeawayCheckoutFrom();
            tj.id = txtDH.Text;
            tj.ShowDialog();
        }
    }
}
