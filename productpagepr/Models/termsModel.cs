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
    public class termsModel
    {

        public int term_id { get; set; }
        public string terms_head{ get; set; }
        public string terms{ get; set; }

    }
}