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
    public static class DB_Customer
    {

        /// <summary>
        /// 插入客户
        /// </summary>
        /// <param CUSTOMER="p"></param>
        /// <returns></returns>
        public static int insertCustomer(CUSTOMER c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_CUSTOMER where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
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


                        cmd.CommandText = string.Format(@"insert into T_CUSTOMER (ID,PIC,NAME,TEL,ADDRESS,UID) VALUES (@ID,@PIC,@NAME,@TEL,@ADDRESS,@UID)"  );
                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = c.id;
                        cmd.Parameters.Add("@PIC", SqlDbType.NVarChar).Value = c.pic;
                        cmd.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = c.name;
                        cmd.Parameters.Add("@TEL", SqlDbType.NVarChar).Value = c.tel;
                        cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = c.address;
                        cmd.Parameters.Add("@UID", SqlDbType.NVarChar).Value = c.uid;

                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int deleteCustomer(CUSTOMER c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"delete from T_CUSTOMER where ID='{0}'",
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
        public static int modeCustomer(CUSTOMER c)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"update T_CUSTOMER set PIC=@PIC,NAME=@NAME,TEL=@TEL,ADDRESS=@ADDRESS where ID=@ID ");
                        cmd.Parameters.Add("@PIC", SqlDbType.NVarChar).Value = c.pic;
                        cmd.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = c.name;
                        cmd.Parameters.Add("@TEL", SqlDbType.NVarChar).Value = c.tel;
                        cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = c.address;
                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = c.id;
                    return cmd.ExecuteNonQuery();

                }

            }

        }



        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns></returns>
        public static List<CUSTOMER> getCustomer(string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();

                    string sql = string.Format("select * from T_CUSTOMER where UID='{0}'", uid);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToCustomer(dt);


            }

        }




        public static CUSTOMER C_Customer(string id)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_CUSTOMER where ID='{0}'", id);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    if(dt.Rows.Count==1)
                        return DataToCustomer(dt)[0];
                    else
                        return new CUSTOMER();


            }

        }




        public static List<CUSTOMER> DataToCustomer(DataTable dt)
        {
            List<CUSTOMER> list = new List<CUSTOMER>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CUSTOMER d = new CUSTOMER
                    {
                        id = dt.Rows[i]["ID"].ToString(),
                        name = dt.Rows[i]["NAME"].ToString(),
                        tel = dt.Rows[i]["TEL"].ToString(),
                        uid = dt.Rows[i]["UID"].ToString(),
                        address = dt.Rows[i]["ADDRESS"].ToString(),
                        pic = dt.Rows[i]["PIC"].ToString()
                    };
                    list.Add(d);
                }
            }
            return list;

        }

    }
}