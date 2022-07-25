using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Collections;

namespace 实力产业管理系统
{
    public partial class ExcessiveFrom : Form
    {
        public ExcessiveFrom()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBT.Text = "进行运行环境配置 。。。";
        }

        private void Trjz_Tick(object sender, EventArgs e)
        {

            LoginFrom tj = new LoginFrom();
            tj.Show();
            Trjz.Enabled = false;
            this.Hide();
        }
    }
}
