using money.Models;
using Money_Interface.DBHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace money.DBHelp
{
    public static class DB_Ad
    {

        /// <summary>
        /// 插入产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int insertAd(AD a,out string msg)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                msg = "";
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    try
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_AD where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                        string id = string.Empty;
                        id = cmd.ExecuteScalar().ToString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            a.id = (Convert.ToInt64(id) + 1).ToString();
                        }
                        else
                        {
                            a.id = DateTime.Now.ToString("yyyyMMdd") + "0001";
                        }

                        int r = cmd.ExecuteNonQuery();
                        if (r == 1)
                        {
                            cmd.CommandText = string.Format(@"UPDATE T_USER set BALANCE=BALANCE-10 where TELEPHONE='{0}')", a.uid);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                        return r;
                    }
                    catch(Exception e) {
                        transaction.Rollback();
                        msg = e.Message;
                        return 0;
                    }



                }

            }

        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int deleteAd(AD a)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
    
                        cmd.CommandText = string.Format(@"delete from T_AD where ID='{0}'",
                      a.id);
                        return cmd.ExecuteNonQuery();
                }

            }

        }

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int modeAd(AD a)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"update T_AD set TITLE=N'{0}',CONTENT=N'{1}' where ID='{2}' ",
                        a.title,a.content, a.id);
                        return cmd.ExecuteNonQuery();
                }

            }

        }



        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns></returns>
        public static List<AD> getAd(string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_AD where UID='{0}'", uid);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToAd(dt);



            }

        }

        public static List<AD> getAd()
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                string sql = string.Format("select * from T_AD");
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToAd(dt);



            }

        }

        public static List<AD> DataToAd(DataTable dt)
        {
            List<AD> list = new List<AD>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AD a = new AD
                    {
                        id = dt.Rows[i]["ID"].ToString(),
                        title = dt.Rows[i]["TITLE"].ToString(),
                        content = dt.Rows[i]["CONTENT"].ToString(),
                        uid = dt.Rows[i]["UID"].ToString()
                    };
                    list.Add(a);
                }
            }
            return list;

        }

    }
}