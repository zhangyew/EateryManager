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
    public partial class AddAdminForm : Form
    {
        public AddAdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string yhm = txtYHM.Text;
            string pwd = txtPWD.Text;
            string pwd2 = txtPWD2.Text;
            string name = txtName.Text;

            int bm = cboBM.SelectedIndex;
            int zt = 0;
            if (cboZT.Text.Equals("可用"))
            {
                zt = '1';
            }
            else
            {
                zt = '1';
            }
            if(string.IsNullOrEmpty(yhm)||
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
            string sql2 = $"SELECT COUNT(UserName)FROM Admins WHERE 1=1 AND IsDelete=0 AND UserName='{yhm}'";
            int x=Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            if (x > 0)
            {
                MessageBox.Show("该用户名已存在！");
                return;
            }
            string sql = $"INSERT INTO Admins (UserName, UserPwd, TrueName,DID,Status, IsDelete)VALUES('{yhm}','{pwd}','{name}','{bm}','{zt}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("操作员添加成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking1();
                }
                txtName.Clear();
                txtPWD.Clear();
                txtPWD2.Clear();
                txtYHM.Clear();
            }
            else
            {
                MessageBox.Show("操作员添加失败！");
            }

        }

        private void AddAdminForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT DID,DName FROM Department where 1=1 UNION SELECT 0,'请选择' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboBM.DisplayMember = "DName";
            cboBM.ValueMember = "DID";
            cboBM.DataSource = table;
        }
    }
}
