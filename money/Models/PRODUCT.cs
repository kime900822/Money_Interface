using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Money_Interface.Models
{
    public class PRODUCT
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string uid { get; set; }

    }

    public class PRODUCT_DATE_PARAMETER {
        /// <summary>
        /// 日期 yyyy-MM-dd
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 类型 month 月，year 年
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string uid { get; set; }

    }



    public class PRODUCT_DATE_R
    {
        public string name { get; set; }
        public string price { get; set; }
        public string uid { get; set; }
        public string num { get; set; }

    }
}