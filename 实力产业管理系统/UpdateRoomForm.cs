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
    public partial class UpdateRoomForm : Form
    {
        public  string ID { get; set; }
        public UpdateRoomForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateRoomForm_Load(object sender, EventArgs e)
        {
            Looking();
        }

        private void Looking()
        {
            string sql = $"SELECT RTName,RTMin,RTNum,RTIsDis FROM RoomType WHERE 1=1 AND RTID='{ID}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtMC.Text =r["RTName"].ToString();
                txtQY.Text = r["RTMin"].ToString();
                txtXF.Text = r["RTNum"].ToString();
                int x = Convert.ToInt32(r["RTIsDis"]);
                if (x == 0)
                {
                    rbtZK.Checked = true;
                }
                r.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string rs = txtQY.Text;
            string xf = txtXF.Text;
            int zk = 0;
            if (rbtZK.Checked == false)
            {
                zk = 1;
            }
            string sql = $"UPDATE RoomType SET RTName='{mc}',RTMin='{xf}',RTNum='{rs}',RTIsDis='{zk}' WHERE 1=1 AND RTID='{ID}'";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("编辑成功！");
                if (RoomForm.roomForm != null)
                {
                    RoomForm.roomForm.Looking();
                }
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            else
            {
                MessageBox.Show("编辑失败！");
            }
        }

       
    }
}
