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
    public partial class ScheAddDuledFrom : Form
    {
        public ScheAddDuledFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 房间编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheAddDuledFrom_Load(object sender, EventArgs e)
        {
            //加载信息
            Looking();
            //动态生成餐台树状图
            BindRoot();
        }

        private void Looking()
        {
            //生成随机订单号
            //string unm = $"YD{DateTime.Now.ToString("yyyyMMdd")}{new Random().Next(1000, 10000)}";
        }
        #region
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

      

        
        private void txtRS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") <= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") <= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }
        private void txtDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") <= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") <= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }
        
        #endregion


        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 添加预定信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            //生成随机订单号
            string unm = $"YD{DateTime.Now.ToString("yyyyMMdd")}{new Random().Next(1000, 10000)}";

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
            if (id == null)
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
            string sql = $"INSERT INTO ReserveOrder(ROID, TID, Name, PeopleNum, Phone, EstimateDate, UpdateDate, Status, Remark, UID)VALUES('{unm}','{id}','{xm}','{rs}','{dh}','{rq}',(GETDATE()),'0','{bz}','1')";
           bool r= DBHpler.ExecuteNonQuery(sql);
            if (r)
            {
                MessageBox.Show("预定信息添加成功");
                if (ScheduledFrom.scheduledFrom != null)
                {
                    ScheduledFrom.scheduledFrom.Looking();
                }
                MainForm tj = new MainForm();
                //tj.SJ = rq;
                //tj.YDid = unm;
                //tj.YDct = id;

            }
        }

        private void tvFJ_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //餐台编号
            id = e.Node.Tag.ToString();
        }

       
    }
}
