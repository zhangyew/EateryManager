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
    public partial class ProductForm : Form
    {
        public static ProductForm productForm;
        public ProductForm()
        {
            InitializeComponent();
            productForm = this;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT GTID,GTName FROM GoodsType where 1=1 and IsDask=0  UNION SELECT 0,'所有项目' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboLB.DisplayMember = "GTName";
            cboLB.ValueMember = "GTID";
            cboLB.DataSource = table;
            Looking();
            Looking2();
            //应用隔行变色
            ChangeListViewColor(lvLB);
            ChangeListViewColor(lvSP);
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
            lvSP.Items.Clear();
            string py = txtPY.Text;
            int lb = cboLB.SelectedIndex;
            string sql = $"SELECT A.GID,A.GName,A.SalePrice,A.BasePrice,B.GTName,A.GoodsUnit,A.GoodsStock,A.IsDiscount FROM GoodsInfo A INNER JOIN GoodsType B ON B.GTID=A.GTID WHERE 1=1 AND A.IsDaks='0'";
            if (lb != 0)
            {
                sql += $" and A.GTID='{lb}'";
            }
            if (py != null)
            {
                sql += $" AND A.GNameSimple LIKE'%{py}%' ";
            }
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["GID"].ToString();
                lvi.SubItems.Add(r["GName"].ToString());
                lvi.SubItems.Add(r["SalePrice"].ToString());
                lvi.SubItems.Add(r["BasePrice"].ToString());
                lvi.SubItems.Add(r["GTName"].ToString());
                lvi.SubItems.Add(r["GoodsUnit"].ToString());
                lvi.SubItems.Add(r["GoodsStock"].ToString());
                string x =Convert.ToInt32(r["IsDiscount"])==0?"Yes":"No";
                lvi.SubItems.Add(x);
                lvSP.Items.Add(lvi);

            }

        }

        public void Looking()
        {
            lvLB.Items.Clear();
            string sql = $"SELECT GTID, GTName, GTProperty FROM GoodsType WHERE 1=1 AND IsDask='0'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["GTID"].ToString();
                lvi.SubItems.Add(r["GTName"].ToString());

                int x = Convert.ToInt32(r["GTProperty"]);
                string y = "";
                if (x == 0)
                {
                    y = "菜品类";
                }else if (x == 1)
                {
                    y = "酒水类";
                }else if (x == 2)
                {
                    y = "其他类";
                }
                lvi.SubItems.Add(y);
                lvLB.Items.Add(lvi);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPTypeForm tj = new AddPTypeForm();
            tj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvLB.SelectedItems.Count == 0)
            {
                return;
            }
            UpdatePTypeForm tj = new UpdatePTypeForm();
            string id = lvLB.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该管理员消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvLB.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvLB.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE GoodsType SET IsDask='1'WHERE GTID='{id}'";
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

        private void cboLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Looking();
            Looking2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddProductForm tj = new AddProductForm();
            tj.ShowDialog();
        }

        private void txtPY_TextChanged(object sender, EventArgs e)
        {
            Looking2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lvSP.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateProductForm tj = new UpdateProductForm();
            string id = lvSP.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"SELECT A.GID as'编号',A.GName as'菜品名称',A.SalePrice as'销售价格',A.BasePrice as'成本价格',B.GTName as'类别',A.GoodsUnit as'单位',A.GoodsStock as'库存',A.IsDiscount as'是否折扣' FROM GoodsInfo A INNER JOIN GoodsType B ON B.GTID=A.GTID WHERE 1=1 AND A.IsDaks='0'";
                DataTable table = DBHpler.GetTable(sql);
                Export.ExportInfo(table);
                MessageBox.Show("数据导出成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序执行过程中发生错误，错误信息："+ex.Message, "程序发生错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该管理员消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvSP.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvSP.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE GoodsInfo SET IsDaks='1'WHERE GID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking2();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }
    }
}
