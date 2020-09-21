using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class paymentModel
    {
        public int id { get; set; }
      
        public string orderid { get; set; }
        public string customerid { get; set; }
        public string datetime { get; set; }
        
        public double amount { get; set; }
        public double totalamount { get; set; }
        public double deliverycharge { get; set; }

    }
}