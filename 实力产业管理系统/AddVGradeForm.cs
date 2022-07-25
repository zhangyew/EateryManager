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
    public partial class AddVGradeForm : Form
    {
        public AddVGradeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string dj = txtDJ.Text;

            if (string.IsNullOrEmpty(mc)||
                string.IsNullOrEmpty(dj))
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }

            string sql = $" INSERT INTO VipGrade (VGName, VGDiscount, IsDelete)VALUES('{mc}','{dj}','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("等级添加成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking2();
                }
            }
            else
            {
                MessageBox.Show("等级添加失败！");
            }
        }
    }
}
