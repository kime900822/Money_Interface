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
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {

        [HttpPost]
        [Route("insertProduct")]
        public HttpResponseMessage insertProduct([FromBody]PRODUCT p)
        {

            Result<PRODUCT> ru = new Result<PRODUCT>();
            int result = 0;

            try
            {

                result = DB_Product.insertProduct(p);

                if (result == 1)
                {
                    ru.code = "3000";
                    ru.success = "true";
                    ru.message = "插入成功！";
                }
                else
                {
                    ru.code = "3001";
                    ru.success = "false";
                    ru.message = "插入失败";
                }
            }
            catch (Exception e) {
                ru.code = "3002";
                ru.success = "false";
                ru.message = e.Message;

            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("modProduct")]
        public HttpResponseMessage modProduct([FromBody]PRODUCT p)
        {
            Result<PRODUCT> ru = new Result<PRODUCT>();
            int result = 0;

            try
            {
                result = DB_Product.modeProduct(p);

                if (result == 1)
                {
                    ru.code = "3010";
                    ru.success = "true";
                    ru.message = "修改成功！";
                }
                else
                {
                    ru.code = "3011";
                    ru.success = "false";
                    ru.message = "修改失败";
                }
            }
            catch (Exception e) {
                ru.code = "3012";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("deleteProduct")]
        public HttpResponseMessage deleteProduct([FromBody]PRODUCT p)
        {
            Result<PRODUCT> ru = new Result<PRODUCT>();
            int result = 0;
            try
            {
                result = DB_Product.deleteProduct(p);

                if (result == 1)
                {
                    ru.code = "3020";
                    ru.success = "true";
                    ru.message = "删除成功！";
                }
                else
                {
                    ru.code = "3021";
                    ru.success = "false";
                    ru.message = "删除失败";
                }

            }
            catch (Exception e) {
                ru.code = "3022";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryProduct")]
        public HttpResponseMessage queryProduct([FromBody]PRODUCT p)
        {
            Result<PRODUCT> ru = new Result<PRODUCT>();
            List<PRODUCT> lproduct = new List<PRODUCT>();

            try
            {
                lproduct = DB_Product.getProduct(p.uid);

                if (lproduct.Count > 0)
                {
                    ru.code = "3030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "3031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "3032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lproduct;

            return Conn.toJson(ru);
        }


        [HttpPost]
        [Route("queryProduct_date")]
        public HttpResponseMessage queryProduct_date([FromBody]PRODUCT_DATE_PARAMETER  p)
        {
            Result<PRODUCT> ru = new Result<PRODUCT>();
            List<PRODUCT> lproduct = new List<PRODUCT>();

            try
            {
                lproduct = DB_Product.getProduct(p.date,p.type);

                if (lproduct.Count > 0)
                {
                    ru.code = "3030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "3031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e)
            {
                ru.code = "3032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lproduct;

            return Conn.toJson(ru);
        }


        [HttpPost]
        [Route("queryProductAll")]
        public HttpResponseMessage queryProductAll()
        {
            Result<PRODUCT> ru = new Result<PRODUCT>();
            List<PRODUCT> lproduct = new List<PRODUCT>();

            try
            {
                lproduct = DB_Product.getProduct();

                if (lproduct.Count > 0)
                {
                    ru.code = "3030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "3031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e)
            {
                ru.code = "3032";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lproduct;

            return Conn.toJson(ru);
        }
    }
}
