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
    public partial class AddDepartment : Form
    {
        public AddDepartment()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string sql = $"  INSERT INTO Department (DName, IsDaks)VALUES('{mc}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("部门信息添加成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking3();
                }
            }
            else
            {
                MessageBox.Show("部门信息添加失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
