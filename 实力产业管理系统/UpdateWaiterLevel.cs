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
    public partial class UpdateWaiterLevel : Form
    {
        public string id { get; set; }
        public UpdateWaiterLevel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateWaiterLevel_Load(object sender, EventArgs e)
        {
            string sql = $"  SELECT WTID,WTName FROM WaiterType WHERE 1=1 AND IsDaks='0'AND WTID='{id}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["WTID"].ToString();
                txtDj.Text = r["WTName"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtDj.Text;
            string sql = $" UPDATE  WaiterType SET WTName='{mc}'where 1=1 AND  WTID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("服务生等级修改成功！");
                if (WaiterLevel.waiterLevel != null)
                {
                    WaiterLevel.waiterLevel.Looking();
                }
                else
                {
                    MessageBox.Show("服务生等级修改失败！");
                }
            }
        }
    }
}
