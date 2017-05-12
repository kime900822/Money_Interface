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
    [RoutePrefix("api/Ad")]
    public class AdController : ApiController
    {

        [HttpPost]
        [Route("insertAd")]
        public HttpResponseMessage insertAd([FromBody]AD a)
        {

            Result<AD> ru = new Result<AD>();
            int result = 0;
            string msg = "";
            try
            {
                result = DB_Ad.insertAd(a,out msg);

                if (result == 1)
                {
                    ru.code = "5000";
                    ru.success = "true";
                    ru.message = "插入成功！";
                }
                else
                {
                    if (msg == "")
                    {
                        ru.code = "5001";
                        ru.success = "false";
                        ru.message = "插入失败";
                    }
                    else {
                        ru.code = "5002";
                        ru.success = "false";
                        ru.message = msg;
                    }
                    
                }
            }
            catch (Exception e) {
                ru.code = "5002";
                ru.success = "false";
                ru.message = e.Message;
            }
            
            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("modAd")]
        public HttpResponseMessage modAd([FromBody]AD a)
        {
            Result<AD> ru = new Result<AD>();
            int result = 0;

            try
            {
                result = DB_Ad.modeAd(a);

                if (result == 1)
                {
                    ru.code = "5010";
                    ru.success = "true";
                    ru.message = "修改成功！";
                }
                else
                {
                    ru.code = "5011";
                    ru.success = "false";
                    ru.message = "修改失败";
                }
            }
            catch(Exception e) {
                ru.code = "5012";
                ru.success = "false";
                ru.message = e.Message;
            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("deleteAd")]
        public HttpResponseMessage deleteAd([FromBody]AD a)
        {
            Result<AD> ru = new Result<AD>();
            int result = 0;
            try
            {
                result = DB_Ad.deleteAd(a);

                if (result == 1)
                {
                    ru.code = "5020";
                    ru.success = "true";
                    ru.message = "删除成功！";
                }
                else
                {
                    ru.code = "5021";
                    ru.success = "false";
                    ru.message = "删除失败";
                }

            }
            catch (Exception e) {
                ru.code = "5022";
                ru.success = "false";
                ru.message = e.Message;

            }

            ru.systemTime = Conn.GetTimeStamp();
            ru.data = null;

            return Conn.toJson(ru);
        }

        [HttpPost]
        [Route("queryAd")]
        public HttpResponseMessage queryAd()
        {
            Result<AD> ru = new Result<AD>();
            List<AD> lad = new List<AD>();

            try
            {
                lad = DB_Ad.getAd();

                if (lad.Count > 0)
                {
                    ru.code = "5030";
                    ru.success = "true";
                    ru.message = "获取成功！";
                }
                else
                {
                    ru.code = "5031";
                    ru.success = "false";
                    ru.message = "无数据";
                }
            }
            catch (Exception e) {
                ru.code = "5032";
                ru.success = "false";
                ru.message = e.Message;
            }
            
            ru.systemTime = Conn.GetTimeStamp();
            ru.data = lad;

            return Conn.toJson(ru);
        }

    }
}
