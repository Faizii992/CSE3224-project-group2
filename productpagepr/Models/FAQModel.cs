using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class FAQModel
    {
        public int Qsn_no { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
       

    }
}