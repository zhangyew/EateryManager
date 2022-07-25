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
    public partial class UpdateVipForm : Form
    {
        public string id { get; set; }
        public UpdateVipForm()
        {
            InitializeComponent();
        }

        private void UpdateVipForm_Load(object sender, EventArgs e)
        {
            string sql = $"  SELECT VGID,VGName FROM VipGrade WHERE 1=1 AND IsDelete=0";
            DataTable table = DBHpler.GetTable(sql);
            cboDJ.ValueMember = "VGID";
            cboDJ.DisplayMember = "VGName";
            cboDJ.DataSource = table;

            string sql2 = $"SELECT MAX(VipID)FROM Vips WHERE 1=1";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            txtBH.Text = (x + 1).ToString();
            Looking();
        }

        private void Looking()
        {
            string sql = $"SELECT VipID,VipName,VipSex,VGID,VipPhone,VipEndDate FROM Vips WHERE IsDelete='0' AND VipID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtBH.Text = r["VipID"].ToString();
                txtXM.Text = r["VipName"].ToString();
                txtDH.Text = r["VipPhone"].ToString();
                cboDJ.SelectedIndex = Convert.ToInt32(r["VGID"]);
                if (r["VipSex"].ToString().Equals("男"))
                {
                    cboXB.SelectedIndex = 0;
                }
                else if (r["VipSex"].ToString().Equals("女"))
                {
                    cboXB.SelectedIndex = 1;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string xm = txtXM.Text;
            string xb = cboXB.Text;
            string dh = txtDH.Text;
            int dj = cboDJ.SelectedIndex;
            string rq = dtpRQ.Value.ToString("yyyy-MM-dd");
            if (
                string.IsNullOrEmpty(xm) ||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(xb) ||
                string.IsNullOrEmpty(rq))
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }
            string sql = $"UPDATE Vips SET VipName='{xm}',VGID='{dj}',VipPhone='{dh}',VipSex='{xb}',VipStartDate='{rq}'WHERE IsDelete='0' AND VipID='{id}'";
            bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("会员信息修改成功！");
                if (VipManagerForm.vipManagerForm != null)
                {
                    VipManagerForm.vipManagerForm.Looking();
                }
            }
            else
            {
                MessageBox.Show("会员信息修改失败！");
                return;
            }
        }
    }
}
