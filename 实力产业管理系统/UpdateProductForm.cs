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
    public partial class UpdateProductForm : Form
    {
        public string id { get; set; }
        public UpdateProductForm()
        {
            InitializeComponent();
        }

        private void UpdateProductForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT GTID,GTName FROM GoodsType where 1=1 and IsDask=0  UNION SELECT 0,'所有项目' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboLB.DisplayMember = "GTName";
            cboLB.ValueMember = "GTID";
            cboLB.DataSource = table;
            looking();
        }

        private void looking()
        {
            string sql = $"  SELECT A.GID,A.GName,A.SalePrice,A.BasePrice,A.GTID,A.GoodsUnit,A.GoodsStock,A.IsDiscount FROM GoodsInfo A INNER JOIN GoodsType B ON B.GTID=A.GTID WHERE 1=1 AND A.IsDaks='0'and A.GID='{id}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["GID"].ToString();
                txtMC.Text = r["GName"].ToString();
                txtXS.Text = r["SalePrice"].ToString();
                txtCB.Text = r["BasePrice"].ToString();
                cboLB.SelectedIndex = Convert.ToInt32(r["GTID"]);
                cboDW.Text = r["GoodsUnit"].ToString();
                txtKC.Text = r["GoodsStock"].ToString();
                int x = Convert.ToInt32(r["IsDiscount"]);
                if (x == 0)
                {
                    rbtYES.Checked = true;
                }
                else
                {
                    rbtNO.Checked = true;
                }

            }



        }

        private void txtMC_TextChanged(object sender, EventArgs e)
        {
            //获取名称
            string name = txtMC.Text;
            //获取简拼
            txtJP.Text = ConvertPinYin.GetSpells(name).ToUpper();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string cb = txtCB.Text;
            string kc = txtKC.Text;
            string py = txtJP.Text;
            string xs = txtXS.Text;
            string dw = cboDW.Text;
            int lb = cboLB.SelectedIndex;
            int x = 0;
            if (rbtYES.Checked == true)
            {
                x = 0;
            }
            else if (rbtNO.Checked == true)
            {
                x = 1;
            }
            if (string.IsNullOrEmpty(mc) ||
                string.IsNullOrEmpty(cb) ||
                string.IsNullOrEmpty(kc) ||
                string.IsNullOrEmpty(py) ||
                string.IsNullOrEmpty(xs) ||
                string.IsNullOrEmpty(dw))
            {
                MessageBox.Show("请完善信息后再保存！");
                return;
            }

            string sql = $"  UPDATE GoodsInfo SET GName='{mc}', GNameSimple='{py}', GTID='{lb}', GoodsUnit='{dw}', SalePrice='{xs}', BasePrice='{cb}', GoodsStock='{kc}', IsDiscount='{x}', IsDaks='0' where 1=1 and GID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("商品信息修改成功！");
                if (ProductForm.productForm != null)
                {
                    ProductForm.productForm.Looking2();
                }
            }
            else
            {
                MessageBox.Show("商品信息修改失败！");
                return;
            }
        }
    }
}
