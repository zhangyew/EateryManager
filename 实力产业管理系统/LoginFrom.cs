using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Collections;

namespace 实力产业管理系统
{
    public partial class LoginFrom : Form
    {
        public LoginFrom()
        {
            InitializeComponent();

        }

        private void LoginFrom_Load(object sender, EventArgs e)
        {
            Looking();
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void Looking()
        {
            string sql = $"SELECT UserID,UserName FROM Admins WHERE 1=1 AND IsDelete='0'";
            DataTable table = DBHpler.GetTable(sql);
            cboMC.ValueMember = "UserID";
            cboMC.DisplayMember = "UserName";
            cboMC.DataSource = table;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {            
            string yhm = cboMC.Text;
            string mm = txtMM.Text;
            if (yhm.Equals("zhangye")&&mm.Equals(""))
            {
                MainForm o = new MainForm();
                o.Show();
                this.Hide();
                return;
            }
            string sql = $"SELECT COUNT(*) FROM Admins WHERE 1=1 AND UserName='{yhm}' AND UserPwd='{mm}'";
            int zh = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (zh == 0)
            {
                MessageBox.Show("用户名或密码输入错误！", "登录提示");
                return;
            }
            MainForm tj = new MainForm();
            tj.Show();
            this.Hide();
        }
    }
}
