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
    public partial class AddVipForm : Form
    {
        public AddVipForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string xm = txtXM.Text;
            string dh = txtDH.Text;
            int dj = cboDJ.SelectedIndex;
            string xb = cboXB.Text;
            string rq = dtpRQ.Value.ToString("yyyy-MM-dd");
            if(
                string.IsNullOrEmpty(xm) ||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(xb) ||
                string.IsNullOrEmpty(rq))
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }
            //string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //int dq = 0;
            //dq = int.Parse(date);
            //int sr = 0;
            //sr = int.Parse(rq);
            //if (sr<dq)
            //{
            //    MessageBox.Show("到期时间不能晚于开卡时间！");
            //    return;
            //}

            string sql = $" INSERT INTO Vips(VipName, VipSex, VGID, VipIntegral, VipPhone, VipBalance,VipEndDate,IsDelete)VALUES('{xm}','{xb}','{(dj)}','0','{dh}','0','{rq}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("会员添加成功！");
                if (VipManagerForm.vipManagerForm != null)
                {
                    VipManagerForm.vipManagerForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("会员添加失败！");
            }


        }

        private void AddVipForm_Load(object sender, EventArgs e)
        {
            string sql = $"  SELECT VGID,VGName FROM VipGrade WHERE 1=1 AND IsDelete=0";
            DataTable table= DBHpler.GetTable(sql);
            cboDJ.ValueMember = "VGID";
            cboDJ.DisplayMember = "VGName";
            cboDJ.DataSource = table;

            string sql2 = $"SELECT MAX(VipID)FROM Vips WHERE 1=1";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            txtBH.Text = (x + 1).ToString();

        }
    }
}
