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
    public partial class AddWaiterFrom : Form
    {
        public AddWaiterFrom()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddWaiterFrom_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT WTID,WTName FROM WaiterType where 1=1 UNION SELECT 0,'所有服务生' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboDJ.DisplayMember = "WTName";
            cboDJ.ValueMember = "WTID";
            cboDJ.DataSource = table;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //获取名称
            string name = txtXM.Text;
            //获取简拼
            txtJP.Text = ConvertPinYin.GetSpells(name).ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xm = txtXM.Text;
            string dh = txtDh.Text;
            string xb = cboXB.Text;
            string jp = txtJP.Text;
            int dj = cboDJ.SelectedIndex;

            if(string.IsNullOrEmpty(xm)||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(xb) ||
                string.IsNullOrEmpty(jp))
            {
                MessageBox.Show("请输入完整信息后再提交保存！");
                return;
            }

            string sql = $"INSERT INTO WaiterInfo(WName, WTID, WNameSimple, Sex, Phone, IsDaks)VALUES('{xm}','{dj}','{jp}','{xb}','{dh}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("服务生信息添加成功！");
                if (EmployeeInForm.employeeInForm != null)
                {
                    EmployeeInForm.employeeInForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("服务生信息添加失败！");
            }
        }
    }
}
