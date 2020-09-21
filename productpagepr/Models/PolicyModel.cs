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
   public  class PolicyModel
    {
        public int policy_id { get; set; }
        public string policyHead { get; set; }

        public string policies { get; set; }
    }

}