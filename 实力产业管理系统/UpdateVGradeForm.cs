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
    public partial class UpdateVGradeForm : Form
    {
        public string id { get; set; }
        public UpdateVGradeForm()
        {
            InitializeComponent();
        }

        private void UpdateVGradeForm_Load(object sender, EventArgs e)
        {
            Looking();
        }

        private void Looking()
        {
            string sql = $" SELECT VGID,VGName,VGDiscount FROM VipGrade WHERE 1=1 AND IsDelete=0 and VGID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtDJ.Text = r["VGName"].ToString();
                txtMC.Text = r["VGDiscount"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string dj = txtDJ.Text;
            string sql = $" UPDATE VipGrade SET VGName='{mc}',VGDiscount='{dj}'WHERE IsDelete=0 and VGID='{id}'";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("会员等级更改成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking2();
                }
            }
            else
            {
                MessageBox.Show("会员等级更改失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
