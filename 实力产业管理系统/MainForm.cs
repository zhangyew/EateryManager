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
    public partial class MainForm : Form
    {
        public static MainForm mainForm;
        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            VipManagerForm tj = new VipManagerForm();
            tj.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //绑定房间类型
                BinRoomType();
            }
            catch (Exception ex)
            {
                //错误提示
                MessageBox.Show("程序执行中发生错误，错误信息为：" + ex.Message, "程序发生错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        /// <summary>
        /// 餐台统计
        /// </summary>
        private void Looking()
        {
            string sql = $"SELECT COUNT(TableID) FROM Tables WHERE 1=1 AND IsDelete='0'";
            int zs = Convert.ToInt32(DBHpler.ExecuteScalar(sql));
            lblzd.Text = zs.ToString();
            string sql2 = $"SELECT COUNT(TableState) FROM Tables WHERE 1=1 AND IsDelete='0' AND TableState='0'";
            int kg = Convert.ToInt32(DBHpler.ExecuteScalar(sql2));
            lblkg.Text = kg.ToString();
            string sql3 = $"SELECT COUNT(TableState) FROM Tables WHERE 1=1 AND IsDelete='0' AND TableState='1'";
            int kgg = Convert.ToInt32(DBHpler.ExecuteScalar(sql3));
            lblky.Text = kgg.ToString();
            string sql4 = $"SELECT COUNT(TableState) FROM Tables WHERE 1=1 AND IsDelete='0' AND TableState='2'";
            lblyd.Text = DBHpler.ExecuteScalar(sql4).ToString();
            string sql5 = $"SELECT COUNT(TableState) FROM Tables WHERE 1=1 AND IsDelete='0' AND TableState='3'";
            lbldy.Text = DBHpler.ExecuteScalar(sql5).ToString();
            int szl = zs / kgg;
            lblszl.Text = szl.ToString() + "%";
        }
        #region
        /// <summary>
        /// 餐台状态
        /// </summary>
        private int TableState = -1;
        /// <summary>
        /// 显示模式
        /// </summary>
        private int VieMode = -1;
        /// <summary>
        /// 餐台编号
        /// </summary>
        private int TalbId = -1;
        /// <summary>
        /// lv的坐标
        /// </summary>
        public int Xpos,Ypos;
        
        #endregion



        /// <summary>
        ///绑定房间类型
        /// </summary>
        public void BinRoomType()
        {
            Looking();
            //清除所有控件
            pMax.Controls.Clear();
            //动态创建Tap控件
            TabControl tap = new TabControl();
            //将Tap控件添加到面板中
            pMax.Controls.Add(tap);
            //让Tap控件默认填充全部
            tap.Dock = DockStyle.Fill;

            //执行数据
            string sql = $"SELECT RTID, RTName, RTMin, RTIsDis, RTNum, IsDelete FROM RoomType UNION SELECT 0,'所有餐台',0,0,0,0";
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                //动态创建Tap选项卡
                TabPage page = new TabPage();
                page.Text = r["RTName"].ToString();
                page.Tag = r["RTID"].ToString();
                //将创建出来的选项卡添加到Tap控件中
                tap.TabPages.Add(page);
                //根据房间类型编号绑定餐台信息
                BinTables(Convert.ToInt32(r["RTID"]), page);
            }
            //关闭连接
            if (r != null) r.Close();
            //绑定Tabpage点击事件
            tap.Click += Page_Click;
        }

        /// <summary>
        /// 获取选中的TabControl的TabPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Click(object sender, EventArgs e)
        {
            TabControl page = sender as TabControl;
            if (page == null)
                return;
            //修改对应的Lab值
            label1.Text = "     " + page.TabPages[page.SelectedIndex].Text;
        }

        /// <summary>
        /// 根据房间类型编号绑定餐台信息
        /// </summary>
        /// <param name="rtid">房间编号</param>
        /// <param name="page">需要添加的选项卡</param>
        public void BinTables(int rtid, TabPage page)
        {
            ListView lvTable = new ListView();
            //绑定图片
            lvTable.LargeImageList = lvImg;
            lvTable.SmallImageList = ImgMin;
            //绑定右键菜单
            lvTable.ContextMenuStrip = cmsRight;
            //绑定点击事件
            lvTable.Click += LvTable_Click;
            //绑定双击事件
            lvTable.DoubleClick += LvTable_DoubleClick;
            //绑定鼠标移动事件
            lvTable.MouseMove += LvTable_MouseMove;
            //绑定鼠标点击事件
            lvTable.MouseDown += LvTable_MouseDown;

            //默认查询全部SQL语句
            string sql = $"  SELECT TableID, TableName, RTID, TableArea, TableState, ServerName, IsDelete FROM Tables where 1=1 AND IsDelete='0'";
            //不是查询全部
            if (rtid != 0)
            {
                sql += $" AND RTID='{rtid}'";
            }
            //判断是否需要过滤状态
            if (TableState != -1)
            {
                sql += $" AND TableState='{TableState}'";
            }

            //执行SQL语句
            SqlDataReader r = DBHpler.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = r["TableID"];
                lvi.Text = r["TableName"].ToString();
                lvi.ImageIndex = Convert.ToInt32(r["TableState"]);
                lvTable.Items.Add(lvi);
            }
            //关闭连接
            if (r != null) r.Close();

            //将创建出来的ListView控件添加到选项卡中
            page.Controls.Add(lvTable);
            lvTable.Dock = DockStyle.Fill;
            lvTable.View = View.LargeIcon;
            if (VieMode == -1)
                lvTable.View = View.LargeIcon;
            else
                lvTable.View = View.SmallIcon;

        }
        /// <summary>
        /// 判断是否有选中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTable_MouseDown(object sender, MouseEventArgs e)
        {
            if((sender as ListView).HitTest(Xpos, Ypos).Item != null)
            {
                //选中了相关数据
            }
            else
            {
                //没有选中相关数据
                lblJDSJ.Text = "";
                lblYYSJ.Text = "";
                lblJE.Text = "";
                tpsCT.Text = "";
                tpsPP.Text = "";
                tpsSL.Text = "";
                tpsZJE.Text = "";
                lvCTXX.Items.Clear();
            }


        }
        /// <summary>
        /// 鼠标移动时获取坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTable_MouseMove(object sender, MouseEventArgs e)
        {
            Ypos = e.Y;
            Xpos = e.X;
        }

        /// <summary>
        /// LV双击点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTable_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                int staus = (sender as ListView).SelectedItems[0].ImageIndex;//餐台状态
                TalbId = (int)(sender as ListView).SelectedItems[0].Tag;//获取点击餐台编号
                switch (staus)
                {
                    case 0:
                        //开单
                        CustomerBilling tj = new CustomerBilling();
                        tj.Id = TalbId.ToString();
                        tj.ShowDialog();
                        break;
                    case 1:
                        //增加消费
                        AddConsume g = new AddConsume();
                        g.id = TalbId.ToString();
                        g.ShowDialog();
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        AddStateFrom x = new AddStateFrom();
                        x.id = TalbId.ToString();
                        x.Show();
                        break;
                    default:
                        break;
                }
            }
        }

        



        /// <summary>
        /// 点击开单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            string sql = $"  SELECT TableState FROM Tables WHERE 1=1 AND TableID='{TalbId}'";
            int id = Convert.ToInt32(DBHpler.ExecuteScalar(sql));

            if (TalbId == -1)
            {
                TipsFrom xx = new TipsFrom();
                xx.ShowDialog();
                return;
            }
            if (id == 0)
            {
                CustomerBilling tj = new CustomerBilling();
                tj.Id = TalbId.ToString();
                tj.ShowDialog();
            }


        }
        /// <summary>
        /// 结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            string sql = $"  SELECT TableState FROM Tables WHERE 1=1 AND TableID='{TalbId}'";
            int id = Convert.ToInt32(DBHpler.ExecuteScalar(sql));

            if (TalbId == -1)
            {
                TipsFrom xx = new TipsFrom();
                xx.ShowDialog();
                return;
            }
            if (id == 1)
            {
                CheckoutFrom tj = new CheckoutFrom();
                tj.id = TalbId.ToString();
                tj.ShowDialog();
                return;
            }
            TipsFrom x = new TipsFrom();
            x.ShowDialog();
        }


        /// <summary>
        /// LV单击点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTable_Click(object sender, EventArgs e)
        {
            // 当用户点击LV控件之后，更改右键菜单的可用状态
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                cmsRight.Items[0].Enabled = false;//结账
                cmsRight.Items[1].Enabled = false;//消费
                cmsRight.Items[2].Enabled = false;//开单
                cmsRight.Items[3].Enabled = false;//预订
                cmsRight.Items[5].Enabled = false;//更换
                cmsRight.Items[6].Enabled = false;//状态

                int staus = (sender as ListView).SelectedItems[0].ImageIndex;//餐台状态
                TalbId = (int)(sender as ListView).SelectedItems[0].Tag;//获取点击餐台编号

                switch (staus)
                {
                    case 0:
                        cmsRight.Items[2].Enabled = true;//开单
                        cmsRight.Items[3].Enabled = true;//预订
                        cmsRight.Items[6].Enabled = true;//状态
                        break;
                    case 1:
                        cmsRight.Items[0].Enabled = true;//结账
                        cmsRight.Items[1].Enabled = true;//消费
                        cmsRight.Items[5].Enabled = true;//更换
                        break;
                    case 2:
                        cmsRight.Items[2].Enabled = true;//开单
                        cmsRight.Items[6].Enabled = true;//状态
                        break;
                    case 3:
                    case 4:
                    case 5:
                        cmsRight.Items[6].Enabled = true;//状态
                        break;
                }
            }

            //点击餐台将对应的信息显示在左边==================================================================================
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                TalbId = (int)(sender as ListView).SelectedItems[0].Tag;//获取点击餐台编号
                //MessageBox.Show($"{TalbId}");
                int staus = (sender as ListView).SelectedItems[0].ImageIndex;//餐台状态
                if (staus == 0)
                {
                    string ssql = $" SELECT B.RTMin FROM Tables A INNER JOIN RoomType B ON B.RTID=A.RTID WHERE TableID='{TalbId}'";
                    lblXF.Text = "￥" + DBHpler.ExecuteScalar(ssql).ToString();
                    lblJDSJ.Text = "";
                    lblYYSJ.Text = "";
                    lblJE.Text = "";
                    tpsCT.Text = "";
                    tpsPP.Text = "";
                    tpsSL.Text = "";
                    tpsZJE.Text = "";
                }
                lvCTXX.Items.Clear();
                switch (staus)
                {
                    case 0:
                        lblZT.Text = "可用";
                        break;
                    case 1:
                        lblZT.Text = "占用";
                        break;
                    case 2:
                        lblZT.Text = "预定";
                        break;
                    case 3:
                        lblZT.Text = "停用";
                        break;
                    case 4:
                        lblZT.Text = "脏台";
                        break;
                    case 5:
                        lblZT.Text = "结账未离店";
                        break;
                }
                //订单编号 and j.OIID='{Oiid}'
                //string sql2 = $"SELECT TOP 1 OIID FROM OrderInfo WHERE 1=1 AND TID='{TalbId}'ORDER BY OpenDate DESC";
                //string Oiid = DBHpler.ExecuteScalar(sql2).ToString();

                string sql = $" SELECT S.GName,S.SalePrice,X.BuyNum,X.Subtotal,X.UpdateTime,A.TrueName,F.RTMin,J.OpenDate,t.TableName FROM OrderInfo J INNER JOIN OrderDetails X ON X.OIID=J.OIID INNER JOIN GoodsInfo S ON S.GID=X.GID INNER JOIN Admins A ON A.UserID=J.UID INNER JOIN Tables T ON T.TableID=J.TID INNER JOIN RoomType F ON F.RTID=T.RTID WHERE 1=1 AND J.TID='{TalbId}' and  j.isDaks='0'and x.isDaks='0'";
                SqlDataReader r = DBHpler.ExecuteReader(sql);
                while (r != null && r.HasRows && r.Read())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "· " + r["GName"].ToString();
                    int o = Convert.ToInt32(r["SalePrice"]);
                    lvi.SubItems.Add("￥" + o.ToString("f2"));
                    lvi.SubItems.Add(1.00.ToString());
                    lvi.SubItems.Add(r["BuyNum"].ToString());
                    int x = Convert.ToInt32(r["Subtotal"]);
                    lvi.SubItems.Add("￥" + x.ToString("f2"));
                    lvi.SubItems.Add(r["UpdateTime"].ToString());
                    lvi.SubItems.Add("*");
                    lvi.SubItems.Add(r["TrueName"].ToString());
                    string sj = r["OpenDate"].ToString();
                    lblJDSJ.Text = sj;

                    lvCTXX.Items.Add(lvi);
                    int dx = Convert.ToInt32(r["RTMin"]);
                    lblXF.Text = "￥" + dx.ToString("f2");
                    string sql2 = $"  SELECT OIID FROM OrderInfo WHERE 1=1 AND TID='{TalbId}'";
                    string Oiid = DBHpler.ExecuteScalar(sql2).ToString();
                    string sql5 = $"  SELECT sum(Subtotal) FROM OrderDetails WHERE 1=1 AND OIID='{Oiid}'";
                    int zy = Convert.ToInt32(DBHpler.ExecuteScalar(sql5));
                    int zje = (zy + dx);
                    lblJE.Text = zy.ToString("f2");
                    tpsZJE.Text = "总消费额：" + zje.ToString("f2");
                    tpsCT.Text = r["TableName"].ToString() + "餐台";
                    string sql4 = $" SELECT sum(BuyNum) FROM OrderDetails WHERE 1=1 AND OIID='{Oiid}' and isDaks='0'";
                    tpsSL.Text = "消费数量：" + DBHpler.ExecuteScalar(sql4).ToString();
                    string sql6 = $" SELECT count(GID) FROM OrderDetails WHERE 1=1 AND OIID='{Oiid}'and isDaks='0'";
                    tpsPP.Text = "消费清单如下 品牌数量：" + DBHpler.ExecuteScalar(sql6).ToString();
                    //使用时间
                    string _sql = $"SELECT DATEDIFF(MI,OpenDate,GETDATE()) FROM OrderInfo   WHERE 1=1 AND IsDaks='0' AND TID='{TalbId}'";
                    double kssj = Convert.ToInt32(DBHpler.ExecuteScalar(_sql));
                    double js = kssj / 60;
                    lblYYSJ.Text = js.ToString("f2");
                }
            }

            //没有选中餐台时禁用按钮
            if ((sender as ListView).SelectedItems.Count < 0)
            {
                TalbId = (int)(sender as ListView).SelectedItems[0].Tag;//获取点击餐台编号

                lblJDSJ.Text = "";
                lblYYSJ.Text = "";
                lblJE.Text = "";
                tpsCT.Text = "";
                tpsPP.Text = "";
                tpsSL.Text = "";
                tpsZJE.Text = "";
                lvCTXX.Items.Clear();


            }
        }


        #region
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (TableState == -1)
                return;
            TableState = -1;
            BinRoomType();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (TableState == 0)
                return;
            TableState = 0;
            BinRoomType();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (TableState == 1)
                return;
            TableState = 1;
            BinRoomType();
        }

        private void 显示脏台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VieMode == -1)
                return;
            VieMode = -1;
            BinRoomType();

        }

        private void 小图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VieMode == 0)
                return;
            VieMode = 0;
            BinRoomType();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (TableState == -1)
                return;
            TableState = -1;
            BinRoomType();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            //if (TableState == -1)
            //    return;
            //TableState = -1;
            BinRoomType();
        }
        #endregion
        private void 顾客开单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerBilling tj = new CustomerBilling();
            tj.Id = TalbId.ToString();
            tj.ShowDialog();
            //MessageBox.Show(tj.id);
        }

        /// <summary>
        /// 右下角时间显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void teJZSJ_Tick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblsjian.Text = date;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SystemSettingsFrom tj = new SystemSettingsFrom();
            tj.ShowDialog();
        }

        private void 增加消费ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddConsume g = new AddConsume();
            g.id = TalbId.ToString();
            g.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ProductForm tj = new ProductForm();
            tj.ShowDialog();
        }
        private void 宾客结账F5EndToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CheckoutFrom tj = new CheckoutFrom();
            tj.id = TalbId.ToString();
            tj.ShowDialog();
        }


        private void 桌台状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStateFrom tj = new AddStateFrom();
            tj.id = TalbId.ToString();
            tj.Show();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            TakeawayFrom tj = new TakeawayFrom();
            tj.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            TakeawayLookFrom tj = new TakeawayLookFrom();
            tj.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ScheduledFrom tj = new ScheduledFrom();
            tj.ShowDialog();
        }

        /// <summary>
        /// 定时器每隔一分钟扫描预订信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void teSM_Tick(object sender, EventArgs e)
        {
            //判断是否存在预订的餐台信息
            string checkOrderCount = $"SELECT count(0) FROM ReserveOrder WHERE Status = 0 AND DATEDIFF(MINUTE,GETDATE(),EstimateDate) <= 30 AND DATEDIFF(MINUTE,GETDATE(),EstimateDate) >= 0";
            int check = Convert.ToInt32(DBHpler.ExecuteScalar(checkOrderCount));

            if (check == 0)
                return;

            //获取30分钟内的数据
            string getOrder = $"SELECT ROID,TID FROM ReserveOrder WHERE Status = 0 AND DATEDIFF(MINUTE,GETDATE(),EstimateDate) <= 30 AND DATEDIFF(MINUTE,GETDATE(),EstimateDate) >= 0";
            SqlDataReader r = DBHpler.ExecuteReader(getOrder);
            while (r != null && r.HasRows && r.Read())
            {
                string id = r["ROID"].ToString();
                string tid = r["TID"].ToString();
                //更改餐台状态为预订
                string _sql4 = $" UPDATE Tables SET TableState='2' WHERE 1=1 AND TableID='{tid}'";
                DBHpler.ExecuteNonQuery(_sql4);
                //修改预订状态为：准备中
                string _sql2 = $"UPDATE ReserveOrder SET Status='3' WHERE 1=1 AND ROID='{id}'";
                DBHpler.ExecuteNonQuery(_sql2);
            }
            if (r != null)
                r.Close();
            BinRoomType();


        }
        /// <summary>
        /// 每隔5分钟扫描超过预订时间未到的信息并进行右下角弹窗提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void teTC_Tick(object sender, EventArgs e)
        {
            string _sql5 = $" SELECT EstimateDate FROM ReserveOrder A INNER JOIN Tables B ON B.TableID=A.TID WHERE 1=1 AND B.TableState='0' AND A.Status='3'";
            string cg = DBHpler.ExecuteScalar(_sql5).ToString();
            DateTime dt = Convert.ToDateTime(cg);//预订时间
            DateTime timeA = DateTime.Now;	//获取当前时间
            TimeSpan tss = dt - timeA;  //计算时间差
            int sjj = Convert.ToInt32(tss.Minutes.ToString());//转int类型进行比较
            if (sjj < 0)
            {
                //TimingFrom tj = new TimingFrom();
                //// teSM.Enabled = false;
                //tj.ShowDialog();
            }
        }
    }
}