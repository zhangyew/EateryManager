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
    public partial class UpdateDepartment : Form
    {
        public string id { get; set; }
        public UpdateDepartment()
        {
            InitializeComponent();
        }

        private void UpdateDepartment_Load(object sender, EventArgs e)
        {
            string sql = $" SELECT DID,DName FROM Department WHERE 1=1 AND IsDaks=0 AND DID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["DID"].ToString();
                txtMC.Text = r["DName"].ToString();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string sql = $"UPDATE Department SET DName='{mc}'where 1=1 and DID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("部门信息修改成功！");
                if (SystemForm.systemForm != null)
                {
                    SystemForm.systemForm.Looking3();
                }
            }
            else
            {
                MessageBox.Show("部门信息修改失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
