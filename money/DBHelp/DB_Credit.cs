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
    public static class DB_Credit
    {

        /// <summary>
        /// 插入产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int insertCredit(CREDIT c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                        cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_CREDIT where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                        string id = string.Empty;
                        id = cmd.ExecuteScalar().ToString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            c.id = (Convert.ToInt64(id) + 1).ToString();
                        }
                        else
                        {
                            c.id = DateTime.Now.ToString("yyyyMMdd") + "0001";
                        }


                        cmd.CommandText = string.Format(@"insert into T_CREDIT (ID,ACCOUNTTYPE,AMOUNT,PAYTYPE,DATE,PID,CID,UID,COUNT) VALUES ('{0}',N'{1}',{2},N'{3}','{4}','{5}','{6}','{7}',{8})",
                      c.id,c.accounttype, Convert.ToDecimal(c.amount), c.paytype,c.date,c.pid,c.cid,c.uid,Convert.ToInt32(c.count));
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int deleteCredit(CREDIT c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"delete from T_CREDIT where ID='{0}'",
                      c.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int modeCredit(CREDIT c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"update T_CREDIT set ACCOUNTTYPE=N'{0}',AMOUNT={1},PAYTYPE=N'{2}',DATE='{3}',PID='{4}',CID='{5}',COUNT={6} where ID='{7}' ",c.accounttype,Convert.ToDecimal(c.amount),c.paytype,c.date,c.pid,c.cid,Convert.ToInt32(c.count),c.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }


        public static string getTotalAmount(string type,string inout,string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sqlwhere = "";
                    if (type == "month")
                    {
                        sqlwhere += string.Format(@" WHERE SUBSTRING(DATE,1,7) = '{0}'", DateTime.Now.ToString("yyyy-MM"));
                    }
                    else {
                        sqlwhere += string.Format(@" WHERE SUBSTRING(DATE,1,4) = '{0}'", DateTime.Now.ToString("yyyy"));
                    }
                    sqlwhere += string.Format(" AND ACCOUNTTYPE='{0}' AND UID='{1}'", inout,uid);

                    cmd.CommandText = string.Format(@"select SUM(AMOUNT) FROM T_CREDIT {0}",sqlwhere);
                    return cmd.ExecuteScalar().ToString();

                }

            }

        }


        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns></returns>
        public static List<CREDIT> getCredit(string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_CREDIT where UID='{0}'", uid);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToCredit(dt);



            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static List<CREDIT> getCredit_query(CREDIT_QUERY c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                string sql = "";
                if (c.type == "date")
                {
                    sql = string.Format("select * from T_CREDIT where UID='{0}' AND DATE='{1}' ", c.uid, c.date);
                }
                else {
                    sql = string.Format("select A.* from T_CREDIT A LEFT JOIN T_CUSTOMER B ON A.CID=B.ID where A.UID='{0}' AND B.NAME='{1}' ", c.uid, c.uname);
                }
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToCredit(dt);



            }

        }


        public static List<CREDIT> DataToCredit(DataTable dt)
        {
            List<CREDIT> list = new List<CREDIT>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CREDIT d = new CREDIT
                    {
                        id = dt.Rows[i]["ID"].ToString(),
                        accounttype = dt.Rows[i]["ACCOUNTTYPE"].ToString(),
                        amount =dt.Rows[i]["AMOUNT"].ToString(),
                        paytype = dt.Rows[i]["PAYTYPE"].ToString(),
                        date = dt.Rows[i]["DATE"].ToString(),
                        pid = dt.Rows[i]["PID"].ToString(),
                        cid = dt.Rows[i]["CID"].ToString(),
                        uid = dt.Rows[i]["UID"].ToString(),
                        count= dt.Rows[i]["COUNT"].ToString(),
                        customer=DB_Customer.C_Customer(dt.Rows[i]["CID"].ToString()),
                        product=DB_Product.C_Product(dt.Rows[i]["PID"].ToString())
                    };
                    list.Add(d);
                }
            }
            return list;

        }
    }
}