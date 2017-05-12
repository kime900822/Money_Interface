using Money_Interface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Money_Interface.DBHelp
{
    public static class DB_Product
    {
        /// <summary>
        /// 插入产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int insertProduct(PRODUCT p)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                        cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_PRODUCT where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                        string id = string.Empty;
                        id = cmd.ExecuteScalar().ToString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            p.id = (Convert.ToInt64(id) + 1).ToString();
                        }
                        else
                        {
                            p.id = DateTime.Now.ToString("yyyyMMdd") + "0001";
                        }


                        cmd.CommandText = string.Format(@"insert into T_PRODUCT (ID,NAME,PRICE,UID) VALUES ('{0}',N'{1}',{2},'{3}')",
                      p.id,p.name,Convert.ToDecimal(p.price),p.uid);
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int deleteProduct(PRODUCT p)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"delete from T_PRODUCT where ID='{0}'",
                      p.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int modeProduct(PRODUCT p)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"update T_PRODUCT set NAME=N'{0}',PRICE={1} where ID='{2}' ",
                        p.name,Convert.ToDecimal(p.price),p.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }



        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns></returns>
        public static List<PRODUCT> getProduct(string uid)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_PRODUCT where UID='{0}'",uid);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToProduct(dt);



            }

        }


        public static List<PRODUCT> getProduct()
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                string sql = string.Format("select * from T_PRODUCT");
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToProduct(dt);



            }

        }


        public static PRODUCT C_Product(string id)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                    string sql = string.Format("select * from T_PRODUCT where ID='{0}'", id);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    if(dt.Rows.Count==1)
                        return DataToProduct(dt)[0];
                    else
                        return new PRODUCT();


            }

        }





        public static List<PRODUCT> DataToProduct(DataTable dt)
        {
            List<PRODUCT> list = new List<PRODUCT>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PRODUCT d = new PRODUCT {
                        id=dt.Rows[i]["ID"].ToString(),
                        name= dt.Rows[i]["NAME"].ToString(),
                        price = dt.Rows[i]["PRICE"].ToString(),
                        uid=dt.Rows[i]["UID"].ToString()
                    };
                    list.Add(d);
                }
            }
            return list;

        }
    }
}