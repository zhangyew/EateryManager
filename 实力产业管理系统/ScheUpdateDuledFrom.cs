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
    public partial class ScheUpdateDuledFrom : Form
    {
        public ScheUpdateDuledFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 预定ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 餐台编号
        /// </summary>
        public string ct { get; set; }
        private void ScheUpdateDuledFrom_Load(object sender, EventArgs e)
        {
            Looing();
            BindRoot();
        }
        /// <summary>
        ///添加父节点的方法
        /// </summary>
        private void BindRoot()
        {
            string sql = $"SELECT * FROM RoomType WHERE 1=1 AND IsDelete='0'";
            DataTable r = DBHpler.GetTable(sql);
            DataRow[] rows = r.Select();//取根
            foreach (DataRow dRow in rows)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Tag = dRow;
                rootNode.Text = dRow["RTName"].ToString();
                tvFJ.Nodes.Add(rootNode);
                BindChildAreas(rootNode);//调用添加子节点的方法
            }
        }
        /// <summary>
        /// 添加子节点的方法递归绑定子区域
        /// </summary>
        /// <param name="fNode"></param>
        private void BindChildAreas(TreeNode fNode)
        {
            string sql = $" SELECT * FROM Tables WHERE 1=1 AND IsDelete='0' and TableState='0'";
            DataTable r = DBHpler.GetTable(sql);
            DataRow dr = (DataRow)fNode.Tag;//父节点数据关联的数据行 
            int fAreaId = (int)dr["RTID"]; //父节点ID";
            DataRow[] rows1 = r.Select("RTID =" + fAreaId);//子区域
            if (rows1.Length == 0) //递归终止，区域不包含子区域时
            {
                return;
            }
            foreach (DataRow dRow in rows1)
            {
                TreeNode node = new TreeNode();
                node.Tag = dRow["TableID"].ToString();
                node.Text = dRow["TableName"].ToString();
                //添加子节点
                fNode.Nodes.Add(node);
            }
        }
        private void Looing()
        {
            string sql = $"SELECT Name,PeopleNum,Phone,EstimateDate,Remark FROM ReserveOrder WHERE 1=1 AND ROID='{id}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtXM.Text = r["Name"].ToString();
                txtDH.Text = r["Phone"].ToString();
                txtRS.Text = r["PeopleNum"].ToString();
                txtBZ.Text = r["Remark"].ToString();
                rbtSJ.Value = DateTime.Now;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dh = txtDH.Text;
            string xm = txtXM.Text;
            string rs = txtRS.Text;
            string bz = txtBZ.Text;

            if (string.IsNullOrEmpty(xm) ||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(rs) ||
                string.IsNullOrEmpty(bz))
            {
                MessageBox.Show("请输入基本信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ct == null)
            {
                MessageBox.Show("请选择需要预定的餐台", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //三天后的时间
            string sj = DateTime.Now.Date.AddDays(3).ToString("yyyy-MM-dd HH:mm");
            //当前时间
            string dq = DateTime.Now.Date.AddDays(0).ToString("yyyy-MM-dd HH:mm");
            //预定时间
            string rq = rbtSJ.Value.ToString("yyyy-MM-dd HH:mm");
            DateTime dt1 = Convert.ToDateTime(rq);
            DateTime dt2 = Convert.ToDateTime(sj);
            DateTime dt3 = Convert.ToDateTime(dq);
            if (dt1 > dt2)
            {
                MessageBox.Show("预定时间不能超过3天", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (dt1 < dt3)
            {
                MessageBox.Show("预定时间不能晚于当前时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //添加预定信息
            string sql = $"UPDATE ReserveOrder SET Name='{xm}',PeopleNum='{rs}',Phone='{dh}',EstimateDate='{rq}',Remark='{bz}',TID='{ct}' WHERE 1=1 AND ROID='{id}'";
            
                bool r = DBHpler.ExecuteNonQuery(sql);
                if (r)
                {
                    MessageBox.Show("预定信息修改成功");
                    if (ScheduledFrom.scheduledFrom != null)
                    {
                        ScheduledFrom.scheduledFrom.Looking();
                    }
                }
                else
                {
                    MessageBox.Show("请选择需要预定的餐台", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
            
        }

        private void tvFJ_AfterSelect(object sender, TreeViewEventArgs e)
        {
           ct= e.Node.Tag.ToString();
        }
    }
}
