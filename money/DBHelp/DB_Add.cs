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
    public static class DB_Add
    {
        public static int insertAdd(ADD a,out string msg)
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
                        cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_ADD where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
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


                        cmd.CommandText = string.Format(@"insert into T_ADD (ID,AMOUNT,TYPE,UID) VALUES ('{0}','{1}',N'{2}','{3}')",
                  a.id, a.amount, a.type, a.uid);
                        int r = cmd.ExecuteNonQuery();
                        if (r == 1)
                        {
                            cmd.CommandText = string.Format(@"UPDATE T_USER SET BALANCE=BALANCE+{0} WHERE TELEPHONE='{1}' ", Convert.ToDecimal(a.amount), a.uid);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }

                        return r;

                    }
                    catch (Exception e) {               
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
        public static int deleteAdd(ADD a)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"delete from T_ADD where ID='{0}'",
                      a.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }




        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns></returns>
        public static List<ADD> getAdd(string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_ADD where UID='{0}'", uid);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToAdd(dt);


            }

        }

        public static List<ADD> DataToAdd(DataTable dt)
        {
            List<ADD> list = new List<ADD>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ADD a = new ADD
                    {
                        id = dt.Rows[i]["ID"].ToString(),
                        amount = dt.Rows[i]["AMOUNT"].ToString(),
                        type = dt.Rows[i]["TYPE"].ToString(),
                        uid = dt.Rows[i]["UID"].ToString()
                    };
                    list.Add(a);
                }
            }
            return list;

        }







    }
}