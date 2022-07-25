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
    public partial class AddRoomForm : Form
    {
        public string ID { get; set; }
        public AddRoomForm()
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
            string xf = txtXF.Text;
            string rs = txtRS.Text;
            int zk = 1;
            if (rbtZK.Checked == true)
            {
                zk = 0;
            }
            string sql = $"INSERT INTO RoomType(RTName,RTMin,RTNum,RTIsDis)VALUES('{mc}','{xf}','{rs}','{zk}')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("房间类型增加成功！");
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
                MessageBox.Show("房间类型增加失败！");
            }
        }
    }
}
