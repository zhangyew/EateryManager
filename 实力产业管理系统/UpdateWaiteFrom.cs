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
    public partial class UpdateWaiteFrom : Form
    {
        public string id { get; set; }
        public UpdateWaiteFrom()
        {
            InitializeComponent();
        }

        private void UpdateWaiteFrom_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT WTID,WTName FROM WaiterType where 1=1 UNION SELECT 0,'所有服务生' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboDJ.DisplayMember = "WTName";
            cboDJ.ValueMember = "WTID";
            cboDJ.DataSource = table;
            Looking();
        }

        private void Looking()
        {
            string sql = $"SELECT a.WID,a.WName,a.WNameSimple,b.WTID,a.Sex,a.Phone FROM WaiterInfo A INNER JOIN WaiterType B ON B.WTID=A.WID WHERE 1=1 and IsDaks='0' AND A.WID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["WID"].ToString();
                txtDh.Text = r["Phone"].ToString();
                txtXM.Text = r["WName"].ToString();
                cboXB.Text = r["Sex"].ToString();
                cboDJ.SelectedIndex = Convert.ToInt32(r["WTID"]);
            }

        }

        private void txtXM_TextChanged(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(xm) ||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(xb) ||
                string.IsNullOrEmpty(jp))
            {
                MessageBox.Show("请输入完整信息后再提交保存！");
                return;
            }

            string sql = $"UPDATE WaiterInfo SET WName='{xm}', WTID='{dj}', WNameSimple='{jp}', Sex='{xb}', Phone='{dh}' WHERE 1=1 AND WID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("服务生信息修改成功！");
                if (EmployeeInForm.employeeInForm != null)
                {
                    EmployeeInForm.employeeInForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("服务生信息修改失败！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
