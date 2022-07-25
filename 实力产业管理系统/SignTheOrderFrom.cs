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
    public partial class SignTheOrderFrom : Form
    {
        public SignTheOrderFrom()
        {
            InitializeComponent();
        }
        public string je { get; set; }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SignTheOrderFrom_Load(object sender, EventArgs e)
        {

            Looking();
        }

        private void Looking()
        {

            string hh = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{je}'";
            int cp = Convert.ToInt32(DBHpler.ExecuteScalar(hh));
            lblj.Text = cp.ToString("f2");
            lble.Text = cp.ToString("f2");
        }

        private void rbtJE_CheckedChanged(object sender, EventArgs e)
        {
            lblBH.Text = "签单扣减金额[&F8]:";
        }

        private void rbtZK_CheckedChanged(object sender, EventArgs e)
        {
            lblBH.Text = "签单打折比例[&F8]:";
            lblzs.Text = "注：0.8即为8折,0为不打折";
        }

        private void rbtZj_CheckedChanged(object sender, EventArgs e)
        {
            lblBH.Text = "最终收取金额[&F8]:";
        }
        private void txtDZ_TextChanged(object sender, EventArgs e)
        {
            if (rbtZK.Checked == true)
            {
                string hh = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{je}'";
                int cp = Convert.ToInt32(DBHpler.ExecuteScalar(hh));
                string dz = txtDZ.Text;
                int x;
                int yhh;
                try
                {
                    double _dz = double.Parse(dz);
                    x = (int)(cp * _dz);
                    lblyhh.Text = x.ToString("f2");
                    yhh = (int)(cp - x);
                    lblsjyh.Text = yhh.ToString("f2");
                }
                catch (Exception)
                {
                }      
              
            }
            if (rbtJE.Checked == true)
            {
                string hh = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{je}'";
                int cp = Convert.ToInt32(DBHpler.ExecuteScalar(hh));

                try
                {
                    int dz = Convert.ToInt32(txtDZ.Text);
                    int zk = (int)(cp - dz);
                    lblyhh.Text = zk.ToString("f2");
                    lblsjyh.Text = dz.ToString("f2");
                }
                catch (Exception)
                {
                }

            }
            if (rbtZj.Checked == true)
            {
                try
                {
                    string hh = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{je}'";
                    int cp = Convert.ToInt32(DBHpler.ExecuteScalar(hh));
                    lblyhh.Text = txtDZ.Text;
                    string k = txtDZ.Text;
                    int x = Convert.ToInt32(k);
                    lblsjyh.Text = (cp - x).ToString("f2");
                }
                catch (Exception)
                {
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (CheckoutFrom.checkoutFrom != null)
                CheckoutFrom.checkoutFrom.txtBZ.Text = ": " + txtBH.Text + "优惠" + lblsjyh.Text + "元: " + txtSM.Text;
                CheckoutFrom.checkoutFrom.lblYH.Text = lblsjyh.Text;
                CheckoutFrom.checkoutFrom.lblSS.Text = lblyhh.Text;
            Close();
        }

        private void txtDZ_KeyPress(object sender, KeyPressEventArgs e)
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
        }
    }
}
