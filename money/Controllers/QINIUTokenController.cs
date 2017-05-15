using money.Models;
using Money_Interface.DBHelp;
using Money_Interface.Models;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace money.Controllers
{
    [RoutePrefix("api/QINIUToken")]
    public class QINIUTokenController : ApiController
    {
        [HttpPost]
        [Route("uploadToken")]
        public HttpResponseMessage uploadToken()
        {
            Result<string> r = new Result<string>();
            List<string> s = new List<string>();
            try
            {
                string AK = ConfigurationManager.AppSettings.Get("AK");
                string SK = ConfigurationManager.AppSettings.Get("SK");
                Mac mac = new Mac(AK, SK);
                Auth auth = new Auth(mac);
                string bucket = "cris";
                string saveKey = Conn.GetTimeStamp() + ".png";
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = bucket;
                // 上传策略有效期(对应于生成的凭证的有效期)          
                putPolicy.SetExpires(3600);
                Qiniu.JSON.JsonHelper.JsonSerializer = new AnotherJsonSerializer();
                Qiniu.JSON.JsonHelper.JsonDeserializer = new AnotherJsonDeserializer();
                string jstr = putPolicy.ToJsonString();
                string token = Auth.CreateUploadToken(mac, jstr);
                s.Add(token);
                r.code = "0000";
                r.success = "true";
                r.message = "请求成功";
            }
            catch (Exception e) {
                r.code = "0001";
                r.success = "false";
                r.message = e.Message;

            }
            r.data = s;
            r.systemTime = Conn.GetTimeStamp();
            return Conn.toJson(r);


        }
    }
}
