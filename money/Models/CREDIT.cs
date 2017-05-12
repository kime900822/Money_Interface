using Money_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace money.Models
{
    public class CREDIT
    {
        public string id { get; set; }
        public string accounttype { get; set; }
        public string amount { get; set; }
        public string count { get; set; }
        public string paytype { get; set; }
        public string date { get; set; }
        public string pid { get; set; }
        public string cid { get; set; }
        public string uid { get; set; }
        public CUSTOMER customer { get; set; }
        public PRODUCT product { get; set; }

    }
}