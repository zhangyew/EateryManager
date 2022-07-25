using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace 实力产业管理系统
{
    class DBHpler
    {
        private static string conte = @"server=.;database=EateryDB;user=sa;pwd=cssl#123";

        /// <summary>
        /// 查询单个值
        /// </summary>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection con = new SqlConnection(conte);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接错误，错误代码为{ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return null;
        }

        /// <summary>
        /// 增删改
        /// </summary>
        public static bool ExecuteNonQuery(string sql)
        {
            SqlConnection con = new SqlConnection(conte);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接错误，错误代码为{ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        /// <summary>
        /// 查询一行
        /// </summary>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(conte);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接错误，错误代码为{ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 查询一张表
        /// </summary>
        public static DataTable GetTable(string sql)
        {
            SqlConnection con = new SqlConnection(conte);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接错误，错误代码为{ex.Message}");
            }

            return null;
        }
    }
}
