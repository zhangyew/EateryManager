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
    public partial class VipManagerForm : Form
    {
        public static VipManagerForm vipManagerForm;
        public VipManagerForm()
        {
            InitializeComponent();
            vipManagerForm = this;
        }

        //窗体加载事件
        private void VipManagerForm_Load(object sender, EventArgs e)
        {
            //基本信息
            Looking();
            //消费信息
            Lookxiaofei();
            //转账信息
            Lookzhuangzhan();
        }

        private void Lookzhuangzhan()
        {
            string lx = "";
            string bh = txtBH.Text;
            string sql = $"  SELECT A.VUID,B.VipName,A.Money,A.TransDate,A.TransactionType FROM TransactionDetails A INNER JOIN Vips B ON B.VipID=A.VUID WHERE 1=1  AND A.TransactionType BETWEEN 1 AND 2" ;
            if (bh != null)
            {
                sql += $"AND A.VUID LIKE '%{bh}%'";
            }
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            lvZZ.Items.Clear();
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["VUID"].ToString();
                lvi.SubItems.Add(r["VipName"].ToString());
                double je = double.Parse(r["Money"].ToString());
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(r["TransDate"].ToString());
                string d = r["TransactionType"].ToString();
                switch (d)
                {
                    case "1":
                        lx = "转入";
                        break;
                        case "2":
                        lx = "转出";
                        break;
                    default:
                        break;
                }
                lvi.SubItems.Add(lx.ToString());
                lvZZ.Items.Add(lvi);
            }

        }
        private void Lookxiaofei()
        {
            string xm = txtXM.Text;
            string sql2 = $"  SELECT B.VipID,B.VipName,A.OIID,A.VUID,A.OrderTotal,A.OpenDate,A.CloseDate FROM OrderInfo A INNER JOIN Vips B ON B.VipID=A.VUID WHERE 1=1 AND A.IsDaks=1";
            if (xm != null)
            {
                sql2 += $" AND B.VipID LIKE '%{xm}%' OR B.VipName LIKE '%{xm}%'";
            }
            lvZD.Items.Clear();
            SqlDataReader r= DBHpler.ExecuteReader(sql2);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["VipID"].ToString();
                lvi.SubItems.Add(r["VipName"].ToString());
                lvi.SubItems.Add(r["OIID"].ToString());
                lvi.SubItems.Add(r["VUID"].ToString());
                double je = double.Parse(r["OrderTotal"].ToString());
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(r["OpenDate"].ToString());
                lvi.SubItems.Add(r["CloseDate"].ToString());
                lvZD.Items.Add(lvi);
            }
            

        }

        public void Looking()
        {
            lvVIP.Items.Clear();
            string tj = txtTJ.Text;
            string sql = $"SELECT a.VipID,a.VipName,a.VipSex,b.VGName,b.VGDiscount,a.VipPhone,a.VipStartDate,a.VipEndDate,a.VipBalance FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE a.IsDelete='0'and B.IsDelete=0 and A.VipName LIKE'%{tj}%'or A.VipID like'%{tj}%'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["VipID"].ToString();
                lvi.SubItems.Add(r["VipName"].ToString());
                lvi.SubItems.Add(r["VipSex"].ToString());
                lvi.SubItems.Add(r["VGName"].ToString());
                lvi.SubItems.Add(r["VGDiscount"].ToString());
                lvi.SubItems.Add(r["VipPhone"].ToString());
                lvi.SubItems.Add(r["VipStartDate"].ToString());
                lvi.SubItems.Add(r["VipEndDate"].ToString());
                lvi.SubItems.Add(r["VipBalance"].ToString());
                lvVIP.Items.Add(lvi);
            }
            //应用隔行变色
            ChangeListViewColor(lvHYXF);
            ChangeListViewColor(lvVIP);
            ChangeListViewColor(lvZD);
            ChangeListViewColor(LVxf);
            ChangeListViewColor(lvZZ);

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


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("是否删除该管理员消息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.No == r)
            {
                return;
            }
            if (lvVIP.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要操作的会员信息!", "提示信息");
                return;
            }
            string id = lvVIP.SelectedItems[0].SubItems[0].Text;
            string sql = $" UPDATE Vips SET IsDelete='1'WHERE VipID='{id}'";
            bool x = DBHpler.ExecuteNonQuery(sql);
            if (x)
            {
                MessageBox.Show("删除成功！");
                Looking();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (lvVIP.SelectedItems.Count == 0)
            {
                return;
            }
            UpdateVipForm tj = new UpdateVipForm();
            string id = lvVIP.SelectedItems[0].SubItems[0].Text;
            tj.id = id;
            tj.ShowDialog();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            AddVipForm tj = new AddVipForm();
            tj.ShowDialog();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Looking();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"SELECT a.VipID as'会员编号',a.VipName as'会员姓名',a.VipSex as'性别',b.VGName as'会员等级',b.VGDiscount as'会员折扣率',a.VipPhone as'联系电话',a.VipStartDate as'办理日期',a.VipEndDate as'到期日期',a.VipBalance as'余额' FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE a.IsDelete='0'and 1=1";
                DataTable table = DBHpler.GetTable(sql);
                Export.ExportInfo(table);
                MessageBox.Show("数据导出成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序执行过程中发生错误，错误信息：" + ex.Message, "程序发生错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 加载会员相关消费信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvVIP_Click(object sender, EventArgs e)
        {
           
            if (lvVIP.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要操作的会员信息!", "提示信息");
                return;
            }
            string bh = lvVIP.SelectedItems[0].SubItems[0].Text;
            //  SELECT * FROM Vips V INNER JOIN TransactionDetails X ON X.VUID=V.VipID INNER JOIN OrderInfo D ON V.VipID=D.VUID   WHERE 1=1
            lvHYXF.Items.Clear();
            string sql = $"  SELECT D.OpenDate,D.CloseDate,V.VipID,X.Remark,X.Money FROM Vips V INNER JOIN TransactionDetails X ON X.VUID=V.VipID INNER JOIN OrderInfo D ON V.VipID=D.VUID INNER JOIN Tables C ON C.TableID=D.TID   WHERE 1=1 AND V.VipID='{bh}' and X.TransactionType='3'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r!=null&&r.HasRows&&r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["OpenDate"].ToString();
                lvi.SubItems.Add(r["CloseDate"].ToString());
                double je = double.Parse(r["Money"].ToString());
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add("已完成");
                lvi.SubItems.Add(r["Remark"].ToString());
                lvHYXF.Items.Add(lvi);
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (lvVIP.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要操作的会员信息!","提示信息");
                return;
            }
            string bh = lvVIP.SelectedItems[0].SubItems[0].Text;
            VipzhuanzhangForm tj = new VipzhuanzhangForm();
            tj.id = bh;
            tj.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (lvVIP.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要操作的会员信息!", "提示信息");
                return;
            }
            string bh = lvVIP.SelectedItems[0].SubItems[0].Text;
            VipChonZhiForm tj = new VipChonZhiForm();
            tj.id = bh;
            tj.ShowDialog();
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            Lookxiaofei();
        }

        private void lvZD_Click(object sender, EventArgs e)
        {
            if (lvZD.SelectedItems.Count == 0)
            {
                return;
            }
                string id = lvZD.SelectedItems[0].SubItems[0].Text;
            string bh = lvZD.SelectedItems[0].SubItems[2].Text;
            LVxf.Items.Clear();
            string sql = $"  SELECT A.OIID,D.GName,D.GID,C.BuyNum,C.Subtotal,E.TableName,F.UserName FROM OrderInfo A INNER JOIN Vips B ON B.VipID=A.VUID INNER JOIN OrderDetails C ON C.OIID=A.OIID INNER JOIN GoodsInfo D ON D.GID=C.GID INNER JOIN Tables E ON E.TableID=A.TID INNER JOIN Admins F ON F.UserID=A.UID WHERE 1=1 AND B.VipID='{id}' AND A.OIID='{bh}'";
            SqlDataReader r= DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {

                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["OIID"].ToString();
                lvi.SubItems.Add(r["GName"].ToString());
                lvi.SubItems.Add(r["GID"].ToString());
                lvi.SubItems.Add(r["BuyNum"].ToString());
                double je = double.Parse(r["Subtotal"].ToString());
                lvi.SubItems.Add(je.ToString("f2j"));
                lvi.SubItems.Add(r["TableName"].ToString());
                lvi.SubItems.Add(r["UserName"].ToString());
                LVxf.Items.Add(lvi);
            }
        }
    }
}
