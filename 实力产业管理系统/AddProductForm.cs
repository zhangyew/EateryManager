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
    public partial class AddProductForm : Form
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT GTID,GTName FROM GoodsType where 1=1 and IsDask=0  UNION SELECT 0,'所有项目' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboLB.DisplayMember = "GTName";
            cboLB.ValueMember = "GTID";
            cboLB.DataSource = table;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 当文本发生变更之后获取简拼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //获取名称
            string name= txtMC.Text;
            //获取简拼
            txtJP.Text = ConvertPinYin.GetSpells(name).ToUpper();

        }

        /// <summary>
        /// 数字有效性验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCB_KeyPress(object sender, KeyPressEventArgs e)
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
            else if (rbtNO.Checked == true){
                x = 1;
            }
            if(string.IsNullOrEmpty(mc)||
                string.IsNullOrEmpty(cb) ||
                string.IsNullOrEmpty(kc) ||
                string.IsNullOrEmpty(py) ||
                string.IsNullOrEmpty(xs) ||
                string.IsNullOrEmpty(dw))
            {
                MessageBox.Show("请完善信息后再保存！");
                return;
            }

            string sql = $"  INSERT INTO GoodsInfo(GName, GNameSimple, GTID, GoodsUnit, SalePrice, BasePrice, GoodsStock, IsDiscount, IsDaks)VALUES" +
                $"('{mc}','{py}','{lb}','{dw}','{xs}','{cb}','{kc}','{x}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("商品信息添加成功！");
                if (ProductForm.productForm != null)
                {
                    ProductForm.productForm.Looking2();
                }
            }
            else
            {
                MessageBox.Show("商品信息添加失败！");
                return;
            }
        }
    }
}
