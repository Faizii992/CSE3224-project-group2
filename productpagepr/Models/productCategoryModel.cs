using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class productCategoryModel
    {
        public int ProductCategory_ID { get; set; }
        public int Product_ID { get; set; }
        public string Product_Category { get; set; }
    }
}