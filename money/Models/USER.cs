using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Money_Interface.Models
{
    public class USER
    {

        public string telephone { get; set; }

        public string pass_word { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public decimal balance { get; set; }
        public decimal month_in { get; set; }
        public decimal year_in { get; set; }
        public decimal month_out { get; set; }
        public decimal year_out { get; set; }

    }

    public class Result<T> {
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string systemTime { get; set; }
        public List<T> data { get; set; }


    }
}