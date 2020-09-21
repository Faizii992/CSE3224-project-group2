using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace productpagepr.Models
{
    public class cartModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string category { get; set; }
        public double totalprice { get; set; }
        public string userid { get; set; }

        


    }
}