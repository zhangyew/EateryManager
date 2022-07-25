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
    public partial class AddStateFrom : Form
    {
        public AddStateFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 餐台编号
        /// </summary>
        public  string id { get; set; }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddStateFrom_Load(object sender, EventArgs e)
        {
            //查询对应餐台信息
            string sql =$"SELECT TableName,TableState FROM Tables WHERE 1=1 AND TableID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if(r!=null && r.HasRows && r.Read())
            {
                lblCT.Text =r["TableName"].ToString();
                string zt =r["TableState"].ToString();
                switch (zt)
                {
                    case "0":
                        lblZT.Text ="可用";
                        break;
                    case "1":
                        lblZT.Text = "占用";
                        break;
                    case "2":
                        lblZT.Text = "预定";
                        break;
                    case "3":
                        lblZT.Text = "停用";
                        break;
                    case "4":
                        lblZT.Text = "脏台";
                        break;
                    case "5":
                        lblZT.Text = "结账未走";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 修改为可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE Tables SET TableState='0'WHERE 1=1 AND TableID='{id}'";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            Close();
        }
        /// <summary>
        /// 修改为预定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE Tables SET TableState='2'WHERE 1=1 AND TableID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            Close();

        }
        /// <summary>
        /// 修改为停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE Tables SET TableState='3'WHERE 1=1 AND TableID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            Close();

        }
        /// <summary>
        /// 修改为脏台
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE Tables SET TableState='4'WHERE 1=1 AND TableID='{id}'";
            bool r = DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            Close();
        }
    }
}
