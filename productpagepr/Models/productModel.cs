using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class productModel
    {
        
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public int Product_Price { get; set; }
        public string Product_Status { get; set; }
       
        public string Product_ImagePath{ get; set; }
        public int Product_Strength { get; set; }
        

        public string Product_Component { get; set; }

   
        public string Product_Description { get; set; }
        

      

    }
}