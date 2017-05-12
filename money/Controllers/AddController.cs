using money.DBHelp;
using money.Models;
using Money_Interface.DBHelp;
using Money_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace money.Controllers
{
    [RoutePrefix("api/Add")]
    public class AddController : ApiController
    {

        [HttpPost]
        [Route("insertAdd")]
        public HttpResponseMessage insertAd([FromBody]ADD a)
        {

            Result<ADD> ru = new Result<ADD>();
            int result = 0;
            string msg = "";
            try
            {
                result = DB_Add.insertAdd(a,out msg);

                if (result == 1)
                {
                    ru.code = "6000";
                    ru.success = "true";
                    ru.message = "插入成功！";
                }
                else
                {
                    if (msg == "")
                    {
                        ru.code = "6001";
                        ru.success = "false";
                        ru.message = "插入失败";
                    }
                    else {
                        ru.code = "6002";
                        ru.success = "false";
                        ru.message = msg;
                    }

                }
            }
            catch (Exception e) {
                ru.code = "6002";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryAdd")]
        public HttpResponseMessage queryAdd([FromBody]ADD a)
        {
            Result<ADD> ru = new Result<ADD>();
            List<ADD> ladd = new List<ADD>();

            try
            {
                ladd = DB_Add.getAdd(a.uid);

                if (ladd.Count > 0)
                {
                    ru.code = "6030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "6031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "6032";
                ru.success = "false";
                ru.message = e.Message ;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = ladd;

            return Conn.toJson(ru);
        }

    }
}
