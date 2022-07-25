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
    public partial class CheckoutFrom : Form
    {
        public static CheckoutFrom checkoutFrom;
        /// <summary>
        /// 餐台编号
        /// </summary>
        public string id { get; set; }

        public CheckoutFrom()
        {
            InitializeComponent();
            checkoutFrom = this;
        }


        /// <summary>
        /// 打折确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            zhang();
        }
        /// <summary>
        /// 手动打折
        /// </summary>
        public void zhang()
        {
            try
            {
                lblDZ.Text = txtDZ.Text;
            }
            catch (Exception)
            {
            }

            pDZ.Visible = false;

            //更新打折后的信息
            string k = txtDZ.Text;

            lvXX.Items.Clear();
            string sql = $" SELECT t.TableName,S.GName,S.SalePrice,X.BuyNum,X.Subtotal,X.UpdateTime,A.TrueName,F.RTMin,t.TableName,j.OIID,s.IsDiscount FROM OrderInfo J INNER JOIN OrderDetails X ON X.OIID=J.OIID INNER JOIN GoodsInfo S ON S.GID=X.GID INNER JOIN Admins A ON A.UserID=J.UID INNER JOIN Tables T ON T.TableID=J.TID INNER JOIN RoomType F ON F.RTID=T.RTID WHERE 1=1 AND J.TID='{id}' and x.isDaks='0'";
            SqlDataReader x = DBHpler.ExecuteReader(sql);
            while (x != null && x.HasRows && x.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = x["TableName"].ToString();
                txtBH.Text = x["TableName"].ToString();
                lvi.SubItems.Add(x["GName"].ToString());
                int je = Convert.ToInt32(x["SalePrice"]);
                lvi.SubItems.Add(je.ToString("f2"));
                int y;
                int zdjd;
                int y2;
                int ys;
                int m;
                int yh;
                //判断商品是否参与折扣
                int d = Convert.ToInt32(x["IsDiscount"]);
                if (d == 1)
                    lvi.SubItems.Add("1");
                else
                    lvi.SubItems.Add(txtDZ.Text);
                //计算打折后单价与打折金额和应收金额

                string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}'";
                int zy = Convert.ToInt32(DBHpler.ExecuteScalar(sql5));
                string sql8 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}'";
                int zy1 = Convert.ToInt32(DBHpler.ExecuteScalar(sql5));
                double _dz = double.Parse(k);

                y = Convert.ToInt32(x["SalePrice"]);
                zdjd = (int)(y * _dz);
                lvi.SubItems.Add(zdjd.ToString("f2"));
                lvi.SubItems.Add((je - zdjd).ToString("f2"));
                lvi.SubItems.Add(x["BuyNum"].ToString());
                y2 = Convert.ToInt32(x["BuyNum"]);
                ys = (int)(zdjd * y2);
                lvi.SubItems.Add(ys.ToString("f2"));
                m = (int)(zy * _dz);
                lblSS.Text = m.ToString("f2");
                yh = zy1 - m;
                lblYH.Text = yh.ToString("f2");

                lvi.SubItems.Add("*");
                lvi.SubItems.Add(x["UpdateTime"].ToString());
                lvi.SubItems.Add(x["TrueName"].ToString());
                lvXX.Items.Add(lvi);
                lblDH.Text = x["OIID"].ToString();
                txtBZ.Text = " :" + x["TrueName"].ToString() + " 手动打折 " + lblDZ.Text;
                string z = txtYHZF.Text;
                string sql6 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}'";
                int zyy = Convert.ToInt32(DBHpler.ExecuteScalar(sql6));
                int yys;

                yys = (int)(Convert.ToInt32(zyy) * _dz);
                if (!string.IsNullOrEmpty(z))
                {
                    int zf = Convert.ToInt32(z);
                    int zq = zf - yys;
                    lblZQ.Text = zq.ToString("f2");
                }

            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckoutFrom_Load(object sender, EventArgs e)
        {
            cboZF.SelectedIndex = 0;
            //加载餐台对应信息
            Looking();
        }

        public void Looking()
        {
            //绑定隐藏信息
            lvYC.Items.Clear();
            string xx = $"SELECT VipID,VipName,B.VGName,A.VipSex,A.VipPhone FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE 1=1 AND A.IsDelete='0'";
            SqlDataReader r = DBHpler.ExecuteReader(xx);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r["VipID"].ToString();
                lvi.SubItems.Add(r["VipName"].ToString());
                lvi.SubItems.Add(r["VGName"].ToString());
                lvi.SubItems.Add(r["VipSex"].ToString());
                lvi.SubItems.Add(r["VipPhone"].ToString());

                lvYC.Items.Add(lvi);
            }
            //根据餐台号订单号查询对应信息
            lvXX.Items.Clear();


            string sql2 = $"SELECT TOP 1 OIID FROM OrderInfo WHERE 1=1 AND TID='{id}'ORDER BY OpenDate DESC";
            
            string Oiid = DBHpler.ExecuteScalar(sql2).ToString();
            string sql = $" SELECT t.TableName,S.GName,S.SalePrice,X.BuyNum,X.Subtotal,X.UpdateTime,A.TrueName,F.RTMin,t.TableName,j.OIID,s.IsDiscount FROM OrderInfo J INNER JOIN OrderDetails X ON X.OIID=J.OIID INNER JOIN GoodsInfo S ON S.GID=X.GID INNER JOIN Admins A ON A.UserID=J.UID INNER JOIN Tables T ON T.TableID=J.TID INNER JOIN RoomType F ON F.RTID=T.RTID WHERE 1=1 AND J.OIID='{Oiid}' and x.isDaks='0'";
            SqlDataReader x = DBHpler.ExecuteReader(sql);
            while (x != null && x.HasRows && x.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = x["TableName"].ToString();
                txtBH.Text = x["TableName"].ToString();
                lvi.SubItems.Add(x["GName"].ToString());
                int je = Convert.ToInt32(x["SalePrice"]);
                lvi.SubItems.Add(je.ToString("f2"));

                //判断商品是否参与折扣
                int d = Convert.ToInt32(x["IsDiscount"]);
                if (d == 1)
                {
                    lvi.SubItems.Add("1");
                }
                else
                {
                    lvi.SubItems.Add(txtDZ.Text);

                }
                lvi.SubItems.Add(je.ToString("f2"));
                lvi.SubItems.Add("0.00");
                lvi.SubItems.Add(x["BuyNum"].ToString());
                int y2 = Convert.ToInt32(x["Subtotal"]);
                lvi.SubItems.Add(y2.ToString("f2"));
                lvi.SubItems.Add("*");
                lvi.SubItems.Add(x["UpdateTime"].ToString());
                lvi.SubItems.Add(x["TrueName"].ToString());

                lvXX.Items.Add(lvi);
                lblDH.Text = x["OIID"].ToString();
                string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}' and isDaks='0'";
                int zy = Convert.ToInt32(DBHpler.ExecuteScalar(sql5));
                lblYS.Text = zy.ToString("f2");
                lblJE.Text = zy.ToString("f2");
                lblSS.Text = zy.ToString("f2");
                lblHJ.Text = zy.ToString("f2");
                txtYHZF.Text = zy.ToString();
            }

            try
            {
                //菜品
                string hh = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{lblDH.Text}' AND B.GTID BETWEEN 2 AND 4  AND A.isDaks='0'";
                int cp = Convert.ToInt32(DBHpler.ExecuteScalar(hh));
                lblCP.Text = cp.ToString("f2");
                //酒水
                string sqll = $"  SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{lblDH.Text}' AND B.GTID='5' AND A.isDaks='0'";
                int js = Convert.ToInt32(DBHpler.ExecuteScalar(sqll));
                lblJS.Text = js.ToString("f2");
                //其他
                string sqkl = $" SELECT SUM(A.Subtotal) FROM OrderDetails A INNER JOIN GoodsInfo B ON B.GID=A.GID  WHERE 1=1 AND OIID='{lblDH.Text}' AND B.GTID='1' AND A.isDaks='0'";
                int qt = Convert.ToInt32(DBHpler.ExecuteScalar(sqkl));
                lblQT.Text = qt.ToString("f2");


            }
            catch (Exception)
            {
            }

            //应用隔行变色
            ChangeListViewColor(lvXX);
            ChangeListViewColor(lvYC);
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

        /// <summary>
        /// 点击确定将选择的值输入到文本框中，显示会员号对应信息并隐藏控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (lvYC.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvYC.SelectedItems[0].SubItems[0].Text;
            txtHY.Text = id;
            pVIP.Visible = false;
            string sql = $"  SELECT VipID,VipName,B.VGName,A.VipBalance FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE 1=1 AND A.IsDelete='0' AND VipID='{id}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                lblKH.Text = id;
                lblXM.Text = r["VipName"].ToString();
                lblDJ.Text = r["VGName"].ToString();
                lblYE.Text = r["VipBalance"].ToString();
                lbljf.Text = "0";
            }
        }
        /// <summary>
        /// 找钱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYHZF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string z = txtYHZF.Text;
                double zf = double.Parse(z);
                string zy = lblSS.Text;
                double ss = double.Parse(zy);
                double zq = zf - ss;
                lblZQ.Text = zq.ToString("f2");
            }
            catch (Exception)
            {
            }

            //string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}'";

            //int ys;
            //try
            //{
            //    if (!string.IsNullOrEmpty(z))
            //    {
            //        int zf = Convert.ToInt32(z);
            //        int zq = zf - zy;
            //        lblZQ.Text = zq.ToString("f2");
            //    }
            //}
            //catch (Exception)
            //{
            //}
            //string kk = txtDZ.Text;
            //try
            //{
            //    double _dz = double.Parse(kk);
            //    ys = (int)(Convert.ToInt32(zy) * _dz);
            //    if (!string.IsNullOrEmpty(z))
            //    {
            //        int zf = Convert.ToInt32(z);
            //        int zq = zf - ys;
            //        lblZQ.Text = zq.ToString("f2");
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label41_Click(object sender, EventArgs e)
        {
            SignTheOrderFrom tj = new SignTheOrderFrom();
            tj.je = lblDH.Text;
            tj.ShowDialog();
        }

        /// <summary>
        /// 宾客结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label42_Click(object sender, EventArgs e)
        {
            string x = lblSS.Text;
            double je = double.Parse(x);
            string z = txtYHZF.Text;
            string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{lblDH.Text}'";
            int zy = Convert.ToInt32(DBHpler.ExecuteScalar(sql5));
            //判断输入余额
            try
            {
                if (!string.IsNullOrEmpty(z))
                {
                    int zf = Convert.ToInt32(z);
                    int zq = (int)(zf - je);
                    if (zq < 0)
                    {
                        MessageBox.Show("付款金额不足,不能结账！若要抹零请直接修改实收金额。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (zq > 100)
                    {
                        MessageBox.Show("找钱金额大于100了耶!");
                        return;
                    }
                }
            }
            catch
            {
            }
            //添加会员消费记录和补充对应的订单记录表(选择会员)
            if (lvYC.SelectedItems.Count > 0)
            {
                string ye = lblYE.Text;
                double j = double.Parse(ye);
                double xy = j - je;
                string id = lvYC.SelectedItems[0].SubItems[0].Text;
                //添加会员消费清单
                string bz = "消费账单:" + lblDH.Text;
                string sql = $" INSERT INTO TransactionDetails (VUID, TransactionType, Money, Balance, Remark)VALUES('{id}','3','{x}','{xy}','{bz}')";
                DBHpler.ExecuteNonQuery(sql);
                //减少用户余额
                string sqll = $"  UPDATE Vips SET VipBalance=(VipBalance-'{x}') WHERE 1=1 AND VipID='{id}'";
                DBHpler.ExecuteNonQuery(sqll);
                //补充信息并逻辑删除信息
                string sql21 = $"UPDATE OrderInfo SET VUID='{id}', CloseDate=GETDATE(),OrderTotal='{je}',IsDaks='1' WHERE OIID='{lblDH.Text}'";
                DBHpler.ExecuteNonQuery(sql21);
                //修改餐台状态
                string sq22l = $"UPDATE Tables SET TableState='0'WHERE 1=1 AND TableID='{id}'";
                DBHpler.ExecuteNonQuery(sq22l);
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            //判断是否先结后走
            if (cbXJBZ.Checked == true)
            {
                //修改餐台状态
                string sql = $"UPDATE Tables SET TableState='5'WHERE 1=1 AND TableID='{id}'";
                DBHpler.ExecuteNonQuery(sql);
                //补充信息并逻辑删除信息
                string sql10 = $"UPDATE OrderInfo SET CloseDate=GETDATE(),OrderTotal='{je}',IsDaks='1' WHERE OIID='{lblDH.Text}' ";
                DBHpler.ExecuteNonQuery(sql10);
                if (MainForm.mainForm != null)
                {
                    MainForm.mainForm.BinRoomType();
                }
            }
            //直接结账(普通用户 非会员)
            //补充信息并逻辑删除信息
            string sql1 = $"UPDATE OrderInfo SET CloseDate=GETDATE(),OrderTotal='{je}',IsDaks='1' WHERE OIID='{lblDH.Text}'";
            DBHpler.ExecuteNonQuery(sql1);
            //修改餐台状态
            string sq2l = $"UPDATE Tables SET TableState='0'WHERE 1=1 AND TableID='{id}'";
            DBHpler.ExecuteNonQuery(sq2l);
            if (MainForm.mainForm != null)
            {
                MainForm.mainForm.BinRoomType();
            }
            this.Close();
            Close();
        }
        /// <summary>
        /// 控制隐藏面板
        /// 数字验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void label33_Click(object sender, EventArgs e)
        {
            pDZ.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pDZ.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pVIP.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            pVIP.Visible = false;
        }
        private void CheckoutFrom_Click(object sender, EventArgs e)
        {
            pVIP.Visible = false;
            pDZ.Visible = false;
        }

        private void txtHY_Click(object sender, EventArgs e)
        {
            pVIP.Visible = true;
        }

        private void lvYC_DoubleClick(object sender, EventArgs e)
        {
            if (lvYC.SelectedItems.Count == 0)
            {
                return;
            }
            string id = lvYC.SelectedItems[0].SubItems[0].Text;
            txtHY.Text = id;
            pVIP.Visible = false;
            string sql = $"  SELECT VipID,VipName,B.VGName,A.VipBalance FROM Vips A INNER JOIN VipGrade B ON B.VGID=A.VGID WHERE 1=1 AND A.IsDelete='0' AND VipID='{id}'";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                lblKH.Text = id;
                lblXM.Text = r["VipName"].ToString();
                lblDJ.Text = r["VGName"].ToString();
                lblYE.Text = r["VipBalance"].ToString();
                lbljf.Text = "0";
            }
            pVIP.Visible = false;
        }

        private void txtDZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //点只能出现一次的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //点不能出现在最前面的判断
            if (e.KeyChar == 46 && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和点的判断
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                e.Handled = true;
            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
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
        #endregion
    }
}
