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
    [RoutePrefix("api/Credit")]
    public class CreditController : ApiController
    {
        [HttpPost]
        [Route("insertCredit")]
        public HttpResponseMessage insertCredit([FromBody]CREDIT c)
        {

            Result<CREDIT> ru = new Result<CREDIT>();
            int result = 0;
            try
            {
                result = DB_Credit.insertCredit(c);

                if (result == 1)
                {
                    ru.code = "7000";
                    ru.success = "true";
                    ru.message = "插入成功！";
                }
                else
                {
                    ru.code = "7001";
                    ru.success = "false";
                    ru.message = "插入失败";
                }

            }
            catch (Exception e) {
                ru.code = "7002";
                ru.success = "false";
                ru.message = e.Message;
            }

   
            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }


        [HttpPost]
        [Route("modCredit")]
        public HttpResponseMessage modCredit([FromBody]CREDIT c)
        {
            Result<CREDIT> ru = new Result<CREDIT>();
            int result = 0;
            try
            {

                result = DB_Credit.modeCredit(c);

                if (result == 1)
                {
                    ru.code = "7010";
                    ru.success = "true";
                    ru.message = "修改成功！";
                }
                else
                {
                    ru.code = "7011";
                    ru.success = "false";
                    ru.message = "修改失败";
                }

            }
            catch (Exception e) {
                ru.code = "7012";
                ru.success = "false";
                ru.message = e.Message;

            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("deleteCredit")]
        public HttpResponseMessage deleteCredit([FromBody]CREDIT c)
        {
            Result<CREDIT> ru = new Result<CREDIT>();
            int result = 0;

            try
            {
                result = DB_Credit.deleteCredit(c);

                if (result == 1)
                {
                    ru.code = "7020";
                    ru.success = "true";
                    ru.message = "删除成功！";
                }
                else
                {
                    ru.code = "7021";
                    ru.success = "false";
                    ru.message = "删除失败";
                }
            }
            catch (Exception e) {
                ru.code = "7022";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryCredit")]
        public HttpResponseMessage queryCredit([FromBody]CREDIT c)
        {
            Result<CREDIT> ru = new Result<CREDIT>();
            List<CREDIT> lcustomer = new List<CREDIT>();

            try
            {
                lcustomer = DB_Credit.getCredit(c.uid);

                if (lcustomer.Count > 0)
                {
                    ru.code = "7030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "7031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "7032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lcustomer;

            return Conn.toJson(ru);
        }



        [HttpPost]
        [Route("queryCredit_date")]
        public HttpResponseMessage queryCredit_date([FromBody]CREDIT_QUERY c)
        {
            Result<CREDIT> ru = new Result<CREDIT>();
            List<CREDIT> lcustomer = new List<CREDIT>();

            try
            {
                lcustomer = DB_Credit.getCredit_query(c);

                if (lcustomer.Count > 0)
                {
                    ru.code = "7040";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "7041";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e)
            {
                ru.code = "7042";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lcustomer;

            return Conn.toJson(ru);
        }
    }
}
