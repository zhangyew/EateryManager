using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实力产业管理系统
{
    public partial class AddPTypeForm : Form
    {
        public AddPTypeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string mc = txtMC.Text;
            int sx = 0;
            if (rbtCP.Checked == true)
            {
                sx = 0;
            }else if (rbtJS.Checked == true)
            {
                sx = 1;
            }else if (rbtQT.Checked == true)
            {
                sx = 2;
            }

            string sql = $" INSERT INTO GoodsType (GTName, GTProperty,IsDask)VALUES('{mc}','{sx}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("商品类别添加成功!");
                if (ProductForm.productForm != null)
                {
                    ProductForm.productForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("商品类别添加失败!");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
