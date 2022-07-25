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
    public partial class AddConsume : Form
    {
        /// <summary>
        /// 餐台编号
        /// </summary>
        public string id { get; set; }
        public AddConsume()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddConsume_Load(object sender, EventArgs e)
        {
            //绑定商品信息
            Looking();
            //动态生成树状菜单视图
            BindRoot();
            //加载所有点单商品信息
            LookingSP();
        }
        /// <summary>
        ///添加父节点的方法
        /// </summary>
        private void BindRoot()
        {
            string sql = $"SELECT * FROM GoodsType WHERE 1=1 AND IsDask='0'";
            DataTable r = DBHpler.GetTable(sql);
            DataRow[] rows = r.Select();//取根
            foreach (DataRow dRow in rows)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Tag = dRow;
                rootNode.Text = dRow["GTName"].ToString();
                tvXX.Nodes.Add(rootNode);
                BindChildAreas(rootNode);//调用添加子节点的方法
            }
        }
        /// <summary>
        /// 添加子节点的方法递归绑定子区域
        /// </summary>
        /// <param name="fNode"></param>
        private void BindChildAreas(TreeNode fNode)
        {
            string sql = $" SELECT * FROM GoodsInfo WHERE 1=1 AND IsDaks='0'";
            DataTable r = DBHpler.GetTable(sql);
            DataRow dr = (DataRow)fNode.Tag;//父节点数据关联的数据行 
            int fAreaId = (int)dr["GTID"]; //父节点ID";
            DataRow[] rows1 = r.Select("GTID =" + fAreaId);//子区域
            if (rows1.Length == 0) //递归终止，区域不包含子区域时
            {
                return;
            }
            foreach (DataRow dRow in rows1)
            {
                TreeNode node = new TreeNode();
                node.Tag = dRow;
                node.Text = dRow["GName"].ToString()+" ￥"+Convert.ToInt32(dRow["SalePrice"]).ToString("f2");

                //添加子节点
                fNode.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 双击TV控件加菜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvXX_DoubleClick(object sender, EventArgs e)
        {
            //string gid = tvXX.SelectedImageIndex.ToString();
            //MessageBox.Show(gid);
            //int sex = Convert.ToInt32(tvXX.SelectedNode.Tag);
            //MessageBox.Show(sex.ToString());

        }
        private void Looking()
        {
           
            //绑定商品信息
            string mc = txtXM.Text;
            lvXX.Items.Clear();
            string sql = $"  SELECT GID,GName,GoodsUnit,SalePrice,GoodsStock FROM GoodsInfo WHERE 1=1 AND IsDaks='0'";
            if (mc != null)
            {
                sql += $" AND GName LIKE '%{mc}%' or GNameSimple LIKE '%{mc}%'";
            }
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["GID"].ToString();
                lvi.SubItems.Add(r["GName"].ToString());
                lvi.SubItems.Add(r["GoodsUnit"].ToString());
                int x =Convert.ToInt32(r["SalePrice"]);
                lvi.SubItems.Add(x.ToString("f2"));
                lvi.SubItems.Add(r["GoodsStock"].ToString());
                lvXX.Items.Add(lvi);
            }
            //加载餐台名称
            string sql2 = $"SELECT TableName FROM Tables  WHERE 1=1 AND IsDelete='0' AND TableID='{id}'";
            txtBH.Text= DBHpler.ExecuteScalar(sql2).ToString();
            //应用隔行变色
            ChangeListViewColor(lvXX);
            ChangeListViewColor(lvDD);
        }

        /// <summary>
        /// 隔行变色   应用隔行变色
        /// </summary> ChangeListViewColor(lvXX);
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
        private void txtXM_TextChanged(object sender, EventArgs e)
        {
            Looking();
            lblJP.Text = "请输入简拼或编码";
        }

        private void lvXX_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
             string s=lvXX.SelectedItems[0].SubItems[1].Text;
            lblXM.Text ="("+s+")";
        }

        private void tvXX_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string x = tvXX.SelectedNode.Text;
            lblXM.Text = "(" + x + ")";
        }
        /// <summary>
        /// 增加点菜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //获取点单数量
            int sl =Convert.ToInt32(txtSL.Text);
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            //获取菜品ID
            string Gid = lvXX.SelectedItems[0].SubItems[0].Text;
            //获取菜品价格
            string x =lvXX.SelectedItems[0].SubItems[3].Text;
            //string转Double
            Double mySalary = Double.Parse(x);
            //Double转int
            int jgg = (int)mySalary;
            //菜品库存减少对应数字
            string sql1 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock - '{sl}') WHERE 1 = 1 AND GID = '{Gid}'";
            bool r= DBHpler.ExecuteNonQuery(sql1);
            if (r)
            {
                Looking();
            }
            int zjg = sl * jgg;
            //查询订单编号
            string sql2 = $"SELECT TOP 1 OIID FROM OrderInfo WHERE 1=1 AND TID='{id}'ORDER BY OpenDate DESC";
            string Oiid = DBHpler.ExecuteScalar(sql2).ToString();
            //增加一条点菜记录
            string sql3 = $"INSERT INTO OrderDetails(OIID, GID, BuyNum, Subtotal, Status,isDaks)VALUES('{Oiid}','{Gid}','{sl}','{zjg}','1','0')";
            bool o= DBHpler.ExecuteNonQuery(sql3);
            if (o)
            {
                LookingSP();
            }


        }
        /// <summary>
        /// 加载所有点单商品信息
        /// </summary>
        private void LookingSP()
        {
            lvDD.Items.Clear();
            //查询订单编号
            //string sql2 = $"  SELECT OIID FROM OrderInfo WHERE 1=1 AND TID='{id}'";
            string sql2 = $" SELECT TOP 1 OIID FROM OrderInfo WHERE 1=1 AND TID='{id}'ORDER BY OpenDate DESC";
            string Oiid = DBHpler.ExecuteScalar(sql2).ToString();

            string sql = $" SELECT A.Status,B.GName,B.SalePrice,B.IsDiscount,A.BuyNum,B.GoodsUnit,A.Subtotal,A.UpdateTime,C.GTName,A.ODID,a.GID " +
                $"FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID INNER JOIN GoodsType C ON C.GTID=B.GTID " +
                $"WHERE 1=1 AND OIID='{Oiid}'AND a.isDaks='0' ";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            string xx = "";
            int bh = 0;
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
               //获取序号
                for (int i = 0; i < 100;)
                {
                    bh++;
                    lvi.Text = bh.ToString();
                    break;
                }
                int x = Convert.ToInt32(r["Status"]);
                if (x == 0)
                {
                    xx = "准备中";
                }else if (x == 1)
                {
                    xx = "已上";
                }else if (x == 2)
                {
                    xx = "退菜";
                }
                lvi.SubItems.Add(xx);
                lvi.SubItems.Add(r["GName"].ToString());
                int jg= Convert.ToInt32(r["SalePrice"]);
                lvi.SubItems.Add(jg.ToString("f2"));
                string zk = Convert.ToInt32(r["IsDiscount"])==0?"是":"否";
                lvi.SubItems.Add(zk);
                lvi.SubItems.Add(r["BuyNum"].ToString());
                lvi.SubItems.Add(r["GoodsUnit"].ToString());
                int je = Convert.ToInt32(r["Subtotal"]);
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(r["UpdateTime"].ToString());
                lvi.SubItems.Add(r["GTName"].ToString());
                lvi.SubItems.Add(r["ODID"].ToString());
                lvi.SubItems.Add(r["GID"].ToString());
                lvDD.Items.Add(lvi);
               
            }
            string sql4 = $" SELECT sum(BuyNum) FROM OrderDetails WHERE 1=1 AND OIID='{Oiid}' and isDaks='0'";
            lblSL.Text= DBHpler.ExecuteScalar(sql4).ToString();
            string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{Oiid}' and isDaks='0'";
            string zy =DBHpler.ExecuteScalar(sql5).ToString();
            try
            {
                double dd = double.Parse(zy);
                lblJE.Text = dd.ToString("f2");
                lblHJJE.Text = dd.ToString("f2");
            }
            catch (Exception)
            {
            }
           
        }
        private void lvDD_Click(object sender, EventArgs e)
        {
            if (lvDD.SelectedItems.Count == 0)
                return;
            string bz = lvDD.SelectedItems[0].SubItems[2].Text;
            lblBZ.Text = "“"+bz+"”的备注：<即起>";
        }
        /// <summary>
        /// 双击LV控件加菜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvXX_DoubleClick(object sender, EventArgs e)
        {
             //获取点单数量
            int sl =Convert.ToInt32(txtSL.Text);
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            //获取菜品ID
            string Gid = lvXX.SelectedItems[0].SubItems[0].Text;
            //获取菜品价格
            string x =lvXX.SelectedItems[0].SubItems[3].Text;
            //string转Double
            Double mySalary = Double.Parse(x);
            //Double转int
            int jgg = (int)mySalary;
            //菜品库存减少对应数字
            string sql1 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock - '{sl}') WHERE 1 = 1 AND GID = '{Gid}'";
            bool r= DBHpler.ExecuteNonQuery(sql1);
            if (r)
            {
                Looking();
            }
            int zjg = sl * jgg;
            //查询订单编号
            string sql2 = $"SELECT TOP 1 OIID FROM OrderInfo WHERE 1=1 AND TID='{id}'ORDER BY OpenDate DESC";
            string Oiid =DBHpler.ExecuteScalar(sql2).ToString();
            //增加一条点菜记录
            string sql3 = $"INSERT INTO OrderDetails(OIID, GID, BuyNum, Subtotal, Status,isDaks)VALUES('{Oiid}','{Gid}','{sl}','{zjg}','1','0')";
            bool o= DBHpler.ExecuteNonQuery(sql3);
            if (o)
            {
                LookingSP();
            }
        }

        /// <summary>
        /// 退菜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label13_Click_1(object sender, EventArgs e)
        {
            if (lvDD.SelectedItems.Count == 0)
            {
                return;
            }
            string ODID = lvDD.SelectedItems[0].SubItems[10].Text;
            string GID = lvDD.SelectedItems[0].SubItems[11].Text;
            string sl = lvDD.SelectedItems[0].SubItems[5].Text;
            string sql = $"UPDATE OrderDetails SET isDaks='1'WHERE ODID='{ODID}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                string sql2 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock + '{sl}') WHERE 1 = 1 AND GID = '{GID}'";
                DBHpler.ExecuteNonQuery(sql2);
                LookingSP();
                Looking();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckoutFrom tj = new CheckoutFrom();
            tj.id = id;
            tj.ShowDialog();
            Close();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") <= 0))
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

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本操作只保存信息不打印后厨，确定吗?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
