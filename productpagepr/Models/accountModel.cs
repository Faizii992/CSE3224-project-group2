using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace productpagepr.Models
{
    public class accountModel
    {
        public int id { get; set; }
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string address { get; set; }
        public string email { get; set; }

        public string phone { get; set; }

        
        public string password { get; set; }

        public string imgpath { get; set; }




    }
}