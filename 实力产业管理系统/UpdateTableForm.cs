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
    public partial class UpdateTableForm : Form
    {
        public string id;
        public UpdateTableForm()
        {
            InitializeComponent();
        }

        private void UpdateTableForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT RTID,RTName FROM RoomType where IsDelete=0";
            DataTable table = DBHpler.GetTable(sql1);
            cboLX.DisplayMember = "RTName";
            cboLX.ValueMember = "RTID";
            cboLX.DataSource = table;
            looking();
        }

        private void looking()
        {
            string sql = $"  SELECT TableName,RTID,TableArea FROM Tables WHERE 1=1 AND TableID='{id}'AND IsDelete=0";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtMC.Text = r["TableName"].ToString();
                txtQY.Text = r["TableArea"].ToString();
                cboLX.SelectedIndex =Convert.ToInt32(r["RTID"])-1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string qy = txtQY.Text;
            int lx = cboLX.SelectedIndex;
            string sql = $"  UPDATE Tables SET TableName='{mc}',RTID='{(lx+1)}',TableArea='{qy}' WHERE TableID='{id}'AND IsDelete=0 ";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("餐台修改成功！");
                if (RoomForm.roomForm != null)
                {
                    RoomForm.roomForm.Looking2();
                }
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            else
            {
                MessageBox.Show("餐台修改失败！");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
