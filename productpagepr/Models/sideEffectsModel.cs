using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class sideEffectsModel
    {
   

            public int SideEffect_ID { get; set; }
        public int SideEffect_Percentage { get; set; }
        public int Product_ID { get; set; }
        public string SideEffect { get; set; }
    }
}