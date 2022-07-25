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
    public partial class SystemForm : Form
    {
        public static SystemForm systemForm;
       
        public SystemForm()
        {
            InitializeComponent();
            systemForm = this;
        }

        private void SystemForm_Load(object sender, EventArgs e)
        {
            string sql1 = $"  SELECT DID,DName FROM Department where 1=1 UNION SELECT 0,'默认部门' ";
            DataTable table = DBHpler.GetTable(sql1);
            cboDJ.DisplayMember = "DName";
            cboDJ.ValueMember = "DID";
            cboDJ.DataSource = table;
            Looking1();
            Looking2();
            Looking3();
            // 应用隔行变色
            ChangeListViewColor(lvVIP);
            ChangeListViewColor(lvGLY);
            ChangeListViewColor(lvBM);
        }
        /// <summary>
        /// 隔行变色  
        /// </summary> 
        /// <param name="listView">需要隔行变色的ListView控件名称</param>
        private void ChangeListViewColor(ListView listView)
        {
            listView.OwnerDraw = true;
            listView.DrawColumnHeader += ListView_DrawColumnHeader;
            listView.DrawSubItem += ListView_DrawSubItem;
        }

        /// <summary>
        /// 绘制表头[避免表头消失]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawText(TextFormatFlags.Default);
        }

        /// <summary>
        /// 绘制隔行变色列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                //选中时的颜色
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(198, 219, 181)), e.Bounds);
            }
            else
            {
                //其他列执行隔行变色
                if (e.ItemIndex % 2 == 0)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 255, 231)), e.Bounds);
                else
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), e.Bounds);
            }
            e.DrawText(TextFormatFlags.Default);
        }
        public void Looking3()
        {
            lvBM.Items.Clear();
            string sql = $" SELECT DID,DName FROM Department WHERE 1=1 AND IsDaks=0";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["DID"].ToString();
                lvi.SubItems.Add(r["DName"].ToString());
                lvBM.Items.Add(lvi);
            }

        }

        public void Looking2()
        {
            lvVIP.Items.Clear();
            string sql = $" SELECT VGID,VGName,VGDiscount FROM VipGrade WHERE 1=1 AND IsDelete=0";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["VGID"].ToString();
                lvi.SubItems.Add(r["VGName"].ToString());
                lvi.SubItems.Add(r["VGDiscount"].ToString());
                lvVIP.Items.Add(lvi);
            }
        }

        public void Looking1()
        {
            lvGLY.Items.Clear();
            string sql = $"  SELECT  A.UserID,A.UserName,A.TrueName,B.DName,A.Status FROM Admins A INNER JOIN Department B ON B.DID=A.DID  WHERE 1=1 AND IsDelete=0";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["UserID"].ToString();
                lvi.SubItems.Add(r["UserName"].ToString());
                lvi.SubItems.Add(r["TrueName"].ToString());
                lvi.SubItems.Add(r["DName"].ToString());
                string x = Convert.ToInt32(r["Status"])==0?"可用":"停用";
                lvi.SubItems.Add(x);
                lvGLY.Items.Add(lvi);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddAdminForm tj = new AddAdminForm();
            tj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvGLY.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateAdminForm tj = new UpdateAdminForm();
            string id = lvGLY.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该管理员消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvGLY.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvGLY.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE Admins SET IsDelete='1'WHERE UserID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking1();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddVGradeForm tj = new AddVGradeForm();
            tj.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (lvGLY.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateVGradeForm tj = new UpdateVGradeForm();
            string id = lvVIP.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该会员消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvVIP.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvVIP.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE VipGrade SET IsDelete='1'WHERE VGID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking2();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请重新启动软件设置将会生效！","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddDepartment tj = new AddDepartment();
            tj.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (lvBM.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateDepartment tj = new UpdateDepartment();
            string id = lvBM.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该部门消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvBM.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvBM.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE Department SET IsDaks='1'WHERE DID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking1();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MainForm.mainForm != null)
                MainForm.mainForm.lblTT.Text =txtBT.Text;
        }
    }
}
