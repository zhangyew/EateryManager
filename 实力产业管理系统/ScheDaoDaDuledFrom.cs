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
    public partial class ScheDaoDaDuledFrom : Form
    {
        public ScheDaoDaDuledFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 餐台编号
        /// </summary>
        public string id{ get; set; }
        private void ScheDaoDaDuledFrom_Load(object sender, EventArgs e)
        {
            Looking();
          
            lvXX.HideSelection = false;
            lvXX.Items[0].Selected = true;
        }

        private void Looking()
        {
            
            lvXX.Items.Clear();
            string sql = $" SELECT A.Name,B.TableName,D.RTName,A.Phone,A.EstimateDate,A.Remark,B.TableID,a.ROID FROM ReserveOrder A INNER JOIN Tables B ON B.TableID=A.TID INNER JOIN Admins C ON C.UserID=A.UID INNER JOIN RoomType D ON D.RTID=B.RTID WHERE 1=1 AND a.ROID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r!=null&&r.Read()&&r.HasRows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["Name"].ToString();
                lvi.SubItems.Add(r["TableName"].ToString());
                lvi.SubItems.Add(r["RTName"].ToString());
                lvi.SubItems.Add(r["Phone"].ToString());
                lvi.SubItems.Add(r["EstimateDate"].ToString());
                lvi.SubItems.Add(r["Remark"].ToString());
                lvi.SubItems.Add(r["TableID"].ToString());
                lvi.SubItems.Add(r["ROID"].ToString());
                lvXX.Items.Add(lvi);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvXX.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvXX.SelectedItems[0].SubItems[6].Text;
            string bh = lvXX.SelectedItems[0].SubItems[7].Text;
            string sql = $" UPDATE ReserveOrder SET Status='1' WHERE 1=1 AND ROID='{bh}'";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                if (ScheduledFrom.scheduledFrom != null)
                    ScheduledFrom.scheduledFrom.Looking();
            }
            CustomerBilling tj = new CustomerBilling();
            tj.Id = id;
            this.Close();
            tj.ShowDialog();
        }
    }
}
