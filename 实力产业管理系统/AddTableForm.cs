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
    public partial class AddTableForm : Form
    {
        public AddTableForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTableForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT RTID,RTName FROM RoomType where IsDelete=0";
            DataTable table = DBHpler.GetTable(sql1);
            cboLX.DisplayMember = "RTName";
            cboLX.ValueMember = "RTID";
            cboLX.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtMC.Text;
            string qy = txtQY.Text;
            int lx = cboLX.SelectedIndex;

            string sql2 = $"  SELECT COUNT(TableName) FROM Tables WHERE IsDelete=0 AND TableName='{mc}'";
            int x=Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            if (x > 0)
            {
                MessageBox.Show("该餐桌名称以重复，亲重新输入名称！");
                return;
            }

            string sql = $" INSERT INTO Tables ( TableName, RTID, TableArea, TableState, IsDelete)VALUES('{mc}','{(lx+1)}','{qy}','0','0')";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("餐台增加成功！");
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
                MessageBox.Show("餐台增加失败！");
            }

        }
    }
}
