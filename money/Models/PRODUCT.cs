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
        public string date { get; set; }
        public string type { get; set; }

    }
}