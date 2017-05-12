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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {

        [HttpPost]
        [Route("insertCustomer")]
        public HttpResponseMessage insertCustomer([FromBody]CUSTOMER c)
        {

            Result<CUSTOMER> ru = new Result<CUSTOMER>();
            int result = 0;

            try
            {
                result = DB_Customer.insertCustomer(c);

                if (result == 1)
                {
                    ru.code = "4000";
                    ru.success = "true";
                    ru.message = "插入成功！";
                }
                else
                {
                    ru.code = "4001";
                    ru.success = "false";
                    ru.message = "插入失败";
                }
            }
            catch (Exception e){
                ru.code = "4002";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }


        [HttpPost]
        [Route("modCustomer")]
        public HttpResponseMessage modCustomer([FromBody]CUSTOMER c)
        {
            Result<CUSTOMER> ru = new Result<CUSTOMER>();
            int result = 0;

            try
            {
                result = DB_Customer.modeCustomer(c);

                if (result == 1)
                {
                    ru.code = "4010";
                    ru.success = "true";
                    ru.message = "修改成功！";
                }
                else
                {
                    ru.code = "4011";
                    ru.success = "false";
                    ru.message = "修改失败";
                }
            }
            catch (Exception e) {
                ru.code = "4012";
                ru.success = "false";
                ru.message =e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("deleteCustomer")]
        public HttpResponseMessage deleteCustomer([FromBody]CUSTOMER c)
        {
            Result<CUSTOMER> ru = new Result<CUSTOMER>();
            int result = 0;

            try
            {
                result = DB_Customer.deleteCustomer(c);

                if (result == 1)
                {
                    ru.code = "4020";
                    ru.success = "true";
                    ru.message = "删除成功！";
                }
                else
                {
                    ru.code = "4021";
                    ru.success = "false";
                    ru.message = "删除失败";
                }
            }
            catch (Exception e) {
                ru.code = "4022";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryCustomer")]
        public HttpResponseMessage queryCustomer([FromBody]CUSTOMER c)
        {
            Result<CUSTOMER> ru = new Result<CUSTOMER>();
            List<CUSTOMER> lcustomer = new List<CUSTOMER>();

            try
            {
                lcustomer = DB_Customer.getCustomer(c.uid);

                if (lcustomer.Count > 0)
                {
                    ru.code = "4030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "4031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "4032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lcustomer;

            return Conn.toJson(ru);
        }

    }
}
