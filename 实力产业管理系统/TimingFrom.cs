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
    public partial class TimingFrom : Form
    {
        public TimingFrom()
        {
            InitializeComponent();
        }

        private void TimingFrom_Load(object sender, EventArgs e)
        {
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - 190;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - 85;
            this.SetDesktopLocation(x, y);
        }

        private void TimingFrom_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TimingFrom_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
