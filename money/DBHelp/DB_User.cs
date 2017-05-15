using Money_Interface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Money_Interface.DBHelp
{
    public static class DB_User
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int Login(USER u,out USER ou)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                ou = new USER();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"select count(*) from T_USER where TELEPHONE='{0}' AND PASS_WORD='{1}'",
                          u.telephone, u.pass_word);
                        int result =Convert.ToInt16(cmd.ExecuteScalar());
                        if (result == 1)
                        {
                            string sql = string.Format(@"select * from T_USER where TELEPHONE='{0}' AND PASS_WORD='{1}'",
                            u.telephone, u.pass_word);
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                            da.Fill(dt);
                            ou= DataToUser(dt)[0];
                        }
                        return result;


                }

            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int register(USER u, out string oid) {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                oid = null;
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(ID) from T_USER where SUBSTRING(ID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                    string id = string.Empty;
                    id = cmd.ExecuteScalar().ToString();
                    if (!string.IsNullOrEmpty(id))
                    {
                        u.id = (Convert.ToInt64(id) + 1).ToString();
                    }
                    else
                    {
                        u.id = DateTime.Now.ToString("yyyyMMdd") + "0001";
                    }


                    oid = id;
                    cmd.CommandText = string.Format(@"select count(*) from T_USER where TELEPHONE='{0}'",
                     u.telephone);

                        if (Convert.ToInt16(cmd.ExecuteScalar().ToString()) > 0) {
                            return -1;
                        }

                        cmd.CommandText = string.Format(@"insert into T_USER (TELEPHONE,PASS_WORD,PHONE,NAME,ID) VALUES ('{0}','{1}',N'{2}',N'{3}',{4})",
                      u.telephone, u.pass_word,u.phone,u.name,u.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int modUser(USER u)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    if (!string.IsNullOrEmpty(u.telephone)) {
                        cmd.CommandText = string.Format(@"select count(*) from T_USER where TELEPHONE='{0}'", u.telephone);

                        if (Convert.ToInt16(cmd.ExecuteScalar().ToString()) > 0)
                        {
                            return -1;
                        }
                    }
                    //  cmd.CommandText = string.Format(@"update T_USER set PASS_WORD='{0}',BALANCE={1},PHONE=N'{2}',NAME='{3}',MONTH_IN={4},YEAR_IN={5},MONTH_OUT={6},YEAR_OUT={7}, TELEPHONE='{8}' where id='{9}' ",
                    //u.pass_word, Convert.ToDecimal(u.balance),u.phone,u.name,Convert.ToDecimal(u.month_in), Convert.ToDecimal(u.month_out), Convert.ToDecimal(u.year_in), Convert.ToDecimal(u.year_out), u.telephone,u.id);
                    cmd.CommandText = string.Format(@"update T_USER set PASS_WORD='{0}', TELEPHONE='{1}' where id='{2}' ",u.pass_word, u.telephone, u.id);
                    return cmd.ExecuteNonQuery();

                }

            }

        }






        public static int deleteUser(USER u)
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                        cmd.CommandText = string.Format(@"delete T_USER where ID='{0}' ",
                      u.id);
                        return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 获取USER
        /// </summary>
        /// <returns></returns>
        public static List<USER> getUser()
        {
            using (SqlConnection conn = new SqlConnection(Conn.connString))
            {
                conn.Open();


                        string sql = string.Format("select * from T_USER");
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        da.Fill(dt);
                        return DataToUser(dt);



            }

        }




        public static List<USER> DataToUser(DataTable dt)
        {
            List<USER> list = new List<USER>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    USER d = new USER
                    {
                        telephone = dt.Rows[i]["TELEPHONE"].ToString(),
                        pass_word = dt.Rows[i]["PASS_WORD"].ToString(),
                        balance= Convert.ToDecimal(dt.Rows[i]["BALANCE"].ToString()),
                        phone= dt.Rows[i]["PHONE"].ToString(),
                        name= dt.Rows[i]["NAME"].ToString(),
                        month_in = Convert.ToDecimal(dt.Rows[i]["MONTH_IN"].ToString()),
                        year_in = Convert.ToDecimal(dt.Rows[i]["YEAR_IN"].ToString()),
                        month_out = Convert.ToDecimal(dt.Rows[i]["MONTH_OUT"].ToString()),
                        year_out = Convert.ToDecimal(dt.Rows[i]["YEAR_OUT"].ToString()),
                        id= dt.Rows[i]["ID"].ToString(),
                    };
                    list.Add(d);
                }
            }
            return list;

        }

    }
}