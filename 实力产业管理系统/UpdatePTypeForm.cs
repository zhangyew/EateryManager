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
    public partial class UpdatePTypeForm : Form
    {
        public string id { get; set; }
        public UpdatePTypeForm()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdatePTypeForm_Load(object sender, EventArgs e)
        {
            Looking();
        }

        private void Looking()
        {
            string sql = $"  SELECT GTID, GTName, GTProperty FROM GoodsType WHERE 1=1 AND GTID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["GTID"].ToString();
                txtMC.Text = r["GTName"].ToString();
                int x = Convert.ToInt32(r["GTProperty"]);
                if (x == 0)
                {
                    rbtCP.Checked = true;
                }else if (x == 1)
                {
                    rbtJS.Checked = true;
                }else if (x == 2)
                {
                    rbtQT.Checked = true;
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string mc = txtMC.Text;
            int sx = 0;
            if (rbtCP.Checked == true)
            {
                sx = 0;
            }
            else if (rbtJS.Checked == true)
            {
                sx = 1;
            }
            else if (rbtQT.Checked == true)
            {
                sx = 2;
            }

            string sql = $" UPDATE  GoodsType SET GTName='{mc}', GTProperty='{sx}'WHERE 1=1 AND GTID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("商品类别修改成功!");
                if (ProductForm.productForm != null)
                {
                    ProductForm.productForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("商品类别修改失败!");
            }
        }
    }
}
