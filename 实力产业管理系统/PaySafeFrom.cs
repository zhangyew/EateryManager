using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace 实力产业管理系统
{
    public partial class PaySafeFrom : Form
    {
        public PaySafeFrom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string id { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PaySafeFrom_Load(object sender, EventArgs e)
        {
            txtKH.Text = id;
            string sql = $"SELECT VipPhone FROM Vips WHERE 1=1 AND VipID='{id}'";
            string dh = DBHpler.ExecuteScalar(sql).ToString();
            string x = dh.ToString();
            string y = Regex.Replace(x, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
            lblsjh.Text = y;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 3; i++)
            //{
            string sj = txtsjh.Text;
            string sql = $"SELECT COUNT(VipPhone) FROM Vips WHERE 1=1 AND VipID='{id}' AND VipPhone='{sj}'";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (x == 0)
            {
                MessageBox.Show($"手机号输入错误，点击确定按钮退出界面", "风险提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (x == 1)
            {
            if (TakeawayCheckoutFrom.takeawayCheckoutFrom != null)
            {
                TakeawayCheckoutFrom.takeawayCheckoutFrom.AddVip();
            }
            }

            Close();
            //if (x == 0)
            //{
            //MessageBox.Show($"手机号输入错误，您还有{i}次机会", "风险提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //}
            // Close();
        }

        private void txtsjh_KeyPress(object sender, KeyPressEventArgs e)
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
        }
    }
}
