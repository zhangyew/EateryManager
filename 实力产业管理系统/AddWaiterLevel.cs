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
    public partial class AddWaiterLevel : Form
    {
        public AddWaiterLevel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mc = txtDj.Text;
            string sql = $"  INSERT INTO WaiterType(WTName, IsDaks)VALUES('{mc}','0')";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("服务生等级添加成功！");
                if (WaiterLevel.waiterLevel != null)
                {
                    WaiterLevel.waiterLevel.Looking();
                }
                else
                {
                    MessageBox.Show("服务生等级添加失败！");
                }
            }

        }
    }
}
