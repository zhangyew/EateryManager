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
    public partial class VipzhuanzhangForm : Form
    {

        public VipzhuanzhangForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string id { set; get; }
        private void button11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string je = txtJE.Text;
            string kh = txtKH.Text;
            string mm = txtMM.Text;
            string bz = txtBZ.Text;

            if(string.IsNullOrEmpty(je)||
                string.IsNullOrEmpty(kh) ||
                string.IsNullOrEmpty(mm))
            {
                MessageBox.Show("请输入完整信息已确保转账成功！","提示信息");
                return;
            }
            //验证密码
            string sql1 = $"SELECT COUNT(*)FROM Vips WHERE 1=1 AND VipID='{id}'AND VipPhone='{mm}'";
            int x=Convert.ToInt32(DBHpler.ExecuteScalar(sql1));
            if (x == 0)
            {
                MessageBox.Show("密码输入错误，请重新输入！","提示信息");
                return;
            }
            //验证余额
            int yy = Convert.ToInt32(je);
            string kk = lblYE.Text;
            Double h = Double.Parse(kk);
            int ye = (int)h;
            int q = yy - ye;

            if (yy > ye)
            {
                MessageBox.Show("转账金额不能大于账户余额", "提示");
                return;
            }
            //开始转账
            string sql2 = $"UPDATE Vips SET VipBalance=VipBalance+'{je}' WHERE 1=1 AND VipID='{kh}'";
            DBHpler.ExecuteNonQuery(sql2);
            //减少转账余额并添加转账记录
            string sql3 = $" UPDATE Vips SET VipBalance=VipBalance-'{je}' WHERE 1=1 AND VipID='{id}'";
            DBHpler.ExecuteNonQuery(sql3);
            string bzz = "向("+kh+")转出"+je+"元"+bz;
            string sql4 = $" INSERT INTO TransactionDetails(VUID, TransactionType, Money, Balance, TransDate, Remark)VALUES('{id}','2','{je}','{q}',GETDATE(),'{bzz}')";
            DBHpler.ExecuteNonQuery(sql4);
            //添加收款人入账信息
            string sql5 = $" UPDATE Vips SET VipBalance=VipBalance+'{je}' WHERE 1=1 AND VipID='{kh}'";
            DBHpler.ExecuteNonQuery(sql5);
            string zz = id+"转入"+je+"元";
            string sql6 = $" INSERT INTO TransactionDetails(VUID, TransactionType, Money, Balance, TransDate, Remark)VALUES('{kh}','1','{je}','{q}',GETDATE(),'{zz}')";
            bool r= DBHpler.ExecuteNonQuery(sql6);
            if (r)
            {
                MessageBox.Show("转账成功！");
                Close();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VipzhuanzhangForm_Load(object sender, EventArgs e)
        {
            string sql = $"  SELECT VipID,VipName,VipBalance FROM Vips WHERE 1=1 AND VipID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                lblKH.Text = r["VipID"].ToString();
                lblXM.Text = r["VipName"].ToString();
                lblYE.Text = r["VipBalance"].ToString();
            }
        }

        private void txtJE_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKH_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
