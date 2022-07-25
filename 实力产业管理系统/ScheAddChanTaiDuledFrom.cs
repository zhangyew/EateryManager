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
    public partial class ScheAddChanTaiDuledFrom : Form
    {
        public ScheAddChanTaiDuledFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 菜品编号
        /// </summary>
        public string id { get; set; }

        //菜品数组
        public string []ID = new string[50];
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScheAddChanTaiDuledFrom_Load(object sender, EventArgs e)
        {
            //绑定商品信息
            Looking();
            //右边点单商品信息
            LooingSP();
        }
        /// <summary>
        /// 右边点单商品信息
        /// </summary>
        private void LooingSP()
        {
            string sl= txtSL.Text;
            double sll = double.Parse(sl);
            //lvDD.Items.Clear();
            for (int i = 0; i < ID.Length; i++)
            {

            
            string sql = $"  SELECT A.GName,A.SalePrice,A.GoodsUnit,B.GTName FROM GoodsInfo A INNER JOIN GoodsType B ON B.GTID=A.GTID WHERE 1=1 AND GID='{ID[i]}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                string j = r["SalePrice"].ToString();
                double jg = double.Parse(j);
                lvi.Text = r["GName"].ToString();
                lvi.SubItems.Add(jg.ToString("f2"));
                lvi.SubItems.Add(sl.ToString());
                lvi.SubItems.Add(r["GoodsUnit"].ToString());
                double je = sll * jg;
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(r["GTName"].ToString());
                lvDD.Items.Add(lvi);
            }
            }
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
                int x = Convert.ToInt32(r["SalePrice"]);
                lvi.SubItems.Add(x.ToString("f2"));
                lvi.SubItems.Add(r["GoodsStock"].ToString());
                lvXX.Items.Add(lvi);
            }
            //加载餐台名称
            //string sql2 = $"SELECT TableName FROM Tables  WHERE 1=1 AND IsDelete='0' AND TableID='{id}'";
            //txtBH.Text = DBHpler.ExecuteScalar(sql2).ToString();
            //应用隔行变色
            ChangeListViewColor(lvXX);
           ChangeListViewColor(lvDD);
        }

        private void txtXM_TextChanged(object sender, EventArgs e)
        {
            Looking();
            lblJP.Text = "请输入简拼或编码";
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

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") <= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") <= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //获取菜品ID
            for (int i = 0; i < ID.Length; i++)
            {
                //获取点单数量
                int sl = Convert.ToInt32(txtSL.Text);
                if (lvXX.SelectedItems.Count == 0)
                {
                    return;
                }
                ID[i] = lvXX.SelectedItems[0].SubItems[0].Text;
                 
                //菜品库存减少对应数字
            string sql1 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock - '{sl}') WHERE 1 = 1 AND GID = '{ID[i]}'";
            bool r = DBHpler.ExecuteNonQuery(sql1);
            if (r)
            {
                Looking();
                LooingSP();
            }
            }
        }

        private void lvXX_DoubleClick(object sender, EventArgs e)
        {
            //获取点单数量
            
           
            //获取菜品ID
            for (int i = 0; i < ID.Length; i++)
            {
                int sl = Convert.ToInt32(txtSL.Text);
                if (lvXX.SelectedItems.Count == 0)
                {
                    return;
                }
                ID[i] = lvXX.SelectedItems[0].SubItems[0].Text;
                
                //菜品库存减少对应数字
                string sql1 = $"UPDATE GoodsInfo SET GoodsStock = (GoodsStock - '{sl}') WHERE 1 = 1 AND GID = '{ID[i]}'";
                bool r = DBHpler.ExecuteNonQuery(sql1);
                if (r)
                {
                    Looking();
                    LooingSP();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lvDD.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要删除的列");
                return;
            }
            //string xxx = lvDD.SelectedItems[0].SubItems[0].Text;
            for (int i = 0; i <ID.Length; i++)
            {
                lvDD.Items.Clear();

            }
        }
    }
}
