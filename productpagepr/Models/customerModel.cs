using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace productpagepr.Models
{
    public class customerModel
    {
        
        public int id { get; set; }
        

        
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string address { get; set; }
        public string email { get; set; }

        public string phone { get; set; }

        public string orderid { get; set; }
        
        public string imgpath { get; set; }

        public string datetm { get; set; }
        public string day { get; set; }
        public int tableid { get; set; }
        public string customerid { get; set; }
        public string orderstatus { get; set; }
        public string acceptdate { get; set; }

    }
}