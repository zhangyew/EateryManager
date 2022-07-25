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
    public partial class TipsFrom : Form
    {
        public string zy { get; set; }
        public TipsFrom()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Looking();
        }

        private void Looking()
        {
            string ct = txtCT.Text;
            string sql = $"  SELECT TableID,TableState FROM Tables WHERE 1=1 AND TableName='{ct}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                if (Convert.ToInt32(r["TableState"]) == 0)
                {
                    CustomerBilling tj = new CustomerBilling();
                    tj.Id = r["TableID"].ToString();
                    this.Close();
                    tj.Show();
                    return;
                }
                if (Convert.ToInt32(r["TableState"]) != 0)
                {
                    TipsFrom tj = new TipsFrom();
                    this.Close();
                    tj.Show();
                    return;
                }
            }
        }
    }
}
