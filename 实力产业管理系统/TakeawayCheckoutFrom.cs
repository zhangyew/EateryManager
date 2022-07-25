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
    public partial class TakeawayCheckoutFrom : Form
    {
        public static TakeawayCheckoutFrom takeawayCheckoutFrom;
        public TakeawayCheckoutFrom()
        {
            InitializeComponent();
            takeawayCheckoutFrom = this;
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public string id { get; set; }

        public int Y { get; set; }
        /// <summary>
        /// 窗体加载时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TakeawayCheckoutFrom_Load(object sender, EventArgs e)
        {
            cboZF.SelectedIndex = 0;
            Looking();
            ChangeListViewColor(lxXX);
        }
        #region
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// 隔行变色   应用隔行变色
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
        private void txtYHZF_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") <= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            //数字0不能出现在最前面的判断
            if (e.KeyChar == 48 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }

        private void txtKH_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDh_KeyPress(object sender, KeyPressEventArgs e)
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
        /// <summary>
        /// 加载对应信息
        /// </summary>
        private void Looking()
        {
            lblDH.Text = id;
            //查询价格
            try
            {
                string _sql = $" SELECT SUM(Subtotal) FROM TakeOutOrderDetails WHERE 1=1 AND IsDake='0' AND TOIID='{id}'";
                string je = DBHpler.ExecuteScalar(_sql).ToString();
                double jg = double.Parse(je);
                lblSS.Text = jg.ToString("f2");
                lblsy.Text = jg.ToString("f2");
                lblYS.Text = jg.ToString("f2");
            }
            catch (Exception)
            {
            }

            lxXX.Items.Clear();
            string sql = $"  SELECT B.GName,A.BuyNum,A.Subtotal,A.UpdateTime FROM TakeOutOrderDetails A INNER JOIN GoodsInfo B ON A.GID=B.GID WHERE 1=1 AND TOIID='{id}' AND A.IsDake='0' AND B.IsDaks='0'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["GName"].ToString();
                lvi.SubItems.Add(r["BuyNum"].ToString());
                string k = r["Subtotal"].ToString();
                double m = double.Parse(k);
                lvi.SubItems.Add(m.ToString("f2"));
                lvi.SubItems.Add(r["UpdateTime"].ToString());
                lxXX.Items.Add(lvi);
            }


        }

        private void cboZF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZF.SelectedIndex == 1)
            {
                paHY.Visible = true;
            }
            else
            {
                paHY.Visible = false;
                Looking();
                txtName.Text = "";
                txtDh.Text = "";
                txtBZ.Text = "";
                txtDZ.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paHY.Visible = false;
        }

        private void TakeawayCheckoutFrom_Click(object sender, EventArgs e)
        {
            paHY.Visible = false;
        }
        public string dzk { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            string kh = txtKH.Text;
            string sql = $"   SELECT COUNT(VipID) FROM Vips WHERE 1=1 AND VipID='{kh}' AND IsDelete='0'";
            int ss = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (ss == 0)
            {
                MessageBox.Show("没有此会员或会员卡以停用，查询会员输入是否有误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult u = MessageBox.Show("系统监测到当前支付环境存在风险，点击确定进行安全验证！", "风险提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == u)
            {
                paHY.Visible = false;
            }
            if (DialogResult.Yes == u)
            {
                PaySafeFrom x = new PaySafeFrom();
                x.id = txtKH.Text;
                x.ShowDialog();
                paHY.Visible = false;
            }
            paHY.Visible = false;
        }


        public void AddVip()
        {
            string kh = txtKH.Text;
            string _sql = $" SELECT A.VipName,A.VipPhone,b.VGName,B.VGDiscount FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE 1=1 AND A.VipID='{kh}'";
            SqlDataReader r = DBHpler.ExecuteReader(_sql);
            if (r != null && r.HasRows && r.Read())
            {
                txtName.Text = r["VipName"].ToString();
                txtDh.Text = r["VipPhone"].ToString();
                txtBZ.Text = ": " + r["VipName"].ToString() + " 先生是尊贵的 " + r["VGName"].ToString() + " 用户，请尽快完成配送!";
                string zk = r["VGDiscount"].ToString();
                dzk = r["VGDiscount"].ToString();
                try
                {
                    double dz = double.Parse(zk);
                    string _sql1 = $" SELECT SUM(Subtotal) FROM TakeOutOrderDetails WHERE 1=1 AND IsDake='0' AND TOIID='{id}'";
                    string je = DBHpler.ExecuteScalar(_sql1).ToString();
                    double jg = double.Parse(je);
                    lblSS.Text = (jg * dz).ToString("f2");

                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 找钱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYHZF_TextChanged(object sender, EventArgs e)
        {
            string zq = txtYHZF.Text;
            string y = lblSS.Text;
            try
            {
                double z = double.Parse(zq);
                double m = double.Parse(y);
                int x = Convert.ToInt32(z - m);
                if (x > 100)
                {
                    MessageBox.Show("找钱金额大于100元了耶!");
                    return;
                }
                lblZQ.Text = (z - m).ToString("");
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 外卖结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label14_Click(object sender, EventArgs e)
        {
            string mc = txtName.Text;
            string dh = txtDh.Text;
            string dz = txtDZ.Text;
            string bz = txtBZ.Text;
            string je = lblSS.Text;
            string id = lblDH.Text;
            if (string.IsNullOrEmpty(mc) ||
                string.IsNullOrEmpty(dh) ||
                string.IsNullOrEmpty(dz) ||
                string.IsNullOrEmpty(bz)
                )
            {
                MessageBox.Show("请输入订单完整信息！");
                return;
            }
            //判断是否为会员结账
            string kh = txtKH.Text;
            string sql = $"   SELECT COUNT(VipID) FROM Vips WHERE 1=1 AND VipID='{kh}' AND IsDelete='0'";
            int ss = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            if (ss > 0)
            {
                string o = lblSS.Text;
                double x = double.Parse(o);
                //查询会员卡余额
                string _sql = $" SELECT VipBalance FROM Vips WHERE 1=1 AND  VipID='{kh}' AND IsDelete='0'";
                int g = Convert.ToInt32(DBHpler.ExecuteScalar(_sql));
                double xy = (x - g);
                //添加会员消费清单
                string bz2 = "外卖账单:" + lblDH.Text;
                string sql2 = $" INSERT INTO TransactionDetails (TDID,VUID, TransactionType, Money, Balance, Remark)VALUES('{id}','{kh}','3','{o}','{xy}','{bz2}')";
                DBHpler.ExecuteNonQuery(sql2);
                //减少用户余额
                string sqll = $"  UPDATE Vips SET VipBalance=(VipBalance-'{o}') WHERE 1=1 AND VipID='{kh}'";
                DBHpler.ExecuteNonQuery(sqll);
                //补充订单信息
                string _sqll = $"UPDATE TakeOutOrderInfo SET VUID='{kh}',CloseDate=(GETDATE()),Discount='{dzk}',OrderTotal='{je}',Remart='{bz}',Name='{mc}',Phone='{dh}',Address='{dz}',IsDaks='1' WHERE 1=1 AND TOIID='{id}'";
                bool r = DBHpler.ExecuteNonQuery(_sqll);
                if (r)
                {
                    MessageBox.Show("骑士正在快马加鞭的向您赶来，请耐心等候");
                    Close();
                    return;
                }
            }
            //补充订单信息
            string _qll = $"UPDATE TakeOutOrderInfo SET CloseDate=(GETDATE()),Discount='1',OrderTotal='{je}',Remart='{bz}',Name='{mc}',Phone='{dh}',Address='{dz}',IsDaks='1' WHERE 1=1 AND TOIID='{id}'";
            bool b = DBHpler.ExecuteNonQuery(_qll);
            if (b)
            {
                MessageBox.Show("骑士正在快马加鞭的向您赶来，请耐心等候");
                Close();
            }

        }
        /// <summary>
        /// 获取用户电话，查询是否有过外卖记录，并将其基本信息填入对应文本框中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDh_TextChanged(object sender, EventArgs e)
        {
            string mc = txtDh.Text;

            string _sql = $"SELECT COUNT(Name) FROM TakeOutOrderInfo WHERE 1=1 AND Phone='{mc}' AND IsDaks='1'";
            int x = Convert.ToInt32(DBHpler.ExecuteScalar(_sql));
            if (x >= 1)
            {
                DialogResult g = MessageBox.Show("检测到该用户为回头客，是否使用上次外卖地址与基本信息", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DialogResult.Yes == g)
                {
                    string sql = $"SELECT Name,Phone,Address FROM TakeOutOrderInfo WHERE 1=1 AND Phone LIKE '%{mc}%' AND IsDaks='1' ORDER BY CloseDate DESC";
                    SqlDataReader r = DBHpler.ExecuteReader(sql);
                    if (r != null && r.Read() && r.HasRows)
                    {
                        txtName.Text = r["Name"].ToString();
                        txtDh.Text = r["Phone"].ToString();
                        txtDZ.Text = r["Address"].ToString();
                    }
                }
                if (DialogResult.No == g)
                {
                    return;
                }
            }

        }
    }
}
