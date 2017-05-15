using Microsoft.AspNet.Identity;
using Money_Interface.DBHelp;
using Money_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Money_Interface.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage login([FromBody]USER u) {
            Result<USER> ru = new Result<USER>();

            USER ou = new USER();
            int result = 0;
            List<USER> lu = new List<USER>();

            try
            {
                result = DB_User.Login(u, out ou);
                lu.Add(ou);

                if (result == 1)
                {
                    ru.code = "2000";
                    ru.success = "true";
                    ru.message = "登录成功！";
                }
                else
                {
                    ru.code = "2001";
                    ru.success = "false";
                    ru.message = "登录失败";
                }
            }
            catch (Exception e) {
                ru.code = "2002";
                ru.success = "false";
                ru.message = e.Message;
            }

            
            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lu;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage register([FromBody]USER u)
        {
            Result<USER> ru = new Result<USER>();
            int result = 0;
            string id;
            List<USER> lu = new List<USER>();


            try
            {
                result = DB_User.register(u,out id);

                if (result == 1)
                {
                    ru.code = "2010";
                    ru.success = "true";
                    ru.message = "注册成功！";
                    u.id = id;
                }
                else if (result == -1)
                {
                    ru.code = "2013";
                    ru.success = "false";
                    ru.message = "用户名已存在";
                }
                else
                {
                    ru.code = "2011";
                    ru.success = "false";
                    ru.message = "注册失败";
                }
            }
            catch (Exception e) {
                ru.code = "2012";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lu;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryUser")]
        public HttpResponseMessage queryUser()
        {
            Result<USER> ru = new Result<USER>();
            List<USER> luser = new List<USER>();

            try
            {
                luser = DB_User.getUser();

                if (luser.Count > 0)
                {
                    ru.code = "2020";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "2021";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "2022";
                ru.success = "false";
                ru.message = e.Message;
            }
 
            ru.systemTime = Conn.GetTimeStamp();
            ru.data = luser ;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("modUser")]
        public HttpResponseMessage modUser([FromBody]USER u)
        {
            Result<USER> ru = new Result<USER>();
            int result = 0;

            try
            {
                result = DB_User.modUser(u);
                if (result == 1)
                {
                    ru.code = "2030";
                    ru.success = "true";
                    ru.message = "修改成功！";
                }
                else if (result == -1) {
                    ru.code = "2033";
                    ru.success = "false";
                    ru.message = "此手机号已注册过";
                }
                else
                {
                    ru.code = "2031";
                    ru.success = "false";
                    ru.message = "修改失败";
                }
            }
            catch (Exception e) {
                ru.code = "2032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("deleteUser")]
        public HttpResponseMessage deleteUser([FromBody]USER u)
        {
            Result<USER> ru = new Result<USER>();
            int result = 0;

            try
            {
                result = DB_User.deleteUser(u);
                if (result == 1)
                {
                    ru.code = "2040";
                    ru.success = "true";
                    ru.message = "删除成功！";
                }
                else
                {
                    ru.code = "2041";
                    ru.success = "false";
                    ru.message = "删除失败";
                }
            }
            catch (Exception e) {
                ru.code = "2042";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

    }
}
