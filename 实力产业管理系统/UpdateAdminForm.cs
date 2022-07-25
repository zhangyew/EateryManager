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
    public partial class UpdateAdminForm : Form
    {
        public string id { get; set; }
        public UpdateAdminForm()
        {
            InitializeComponent();
        }

        private void UpdateAdminForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT DID,DName FROM Department where 1=1 UNION SELECT 0,'请选择' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboBM.DisplayMember = "DName";
            cboBM.ValueMember = "DID";
            cboBM.DataSource = table;
            looking();
        }

        private void looking()
        {
            string sql = $"SELECT UserName,UserPwd,TrueName,DID,Status FROM Admins WHERE 1=1 AND IsDelete=0 AND UserID='{id}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtYHM.Text = r["UserName"].ToString();
                txtPWD.Text = r["UserPwd"].ToString();
                txtPWD2.Text = r["UserPwd"].ToString();
                txtName.Text = r["TrueName"].ToString();
                cboBM.SelectedIndex = Convert.ToInt32(r["DID"]);
                int x = Convert.ToInt32(r["Status"]);
                if (x == 0)
                {
                    cboZT.Text = "可用";
                }
                else
                {
                    cboZT.Text = "停用";
                }
            }

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            string yhm = txtYHM.Text;
            string pwd = txtPWD.Text;
            string pwd2 = txtPWD2.Text;
            string name = txtName.Text;
            int bm = cboBM.SelectedIndex;
            int zt = 0;
            if (cboZT.Text.Equals("可用"))
            {
                zt = 0;
            }
            else
            {
                zt = 1;
            }
            if (string.IsNullOrEmpty(yhm) ||
                string.IsNullOrEmpty(pwd) ||
                string.IsNullOrEmpty(pwd2) ||
                string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }
            if (!pwd.Equals(pwd2))
            {
                MessageBox.Show("两次密码不一致，请重新输入!");
                return;
            }

            string sql = $" UPDATE Admins SET UserName='{yhm}',UserPwd='{pwd}',TrueName='{name}',DID='{bm}',Status='{zt}' WHERE 1=1 AND IsDelete=0 AND UserID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("操作员修改成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking1();
                }
            }
            else
            {
                MessageBox.Show("操作员修改失败！");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
