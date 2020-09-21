using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class doctorModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }

        public string Speciality { get; set; }
        public string imgpath { get; set; }

        public string Chamber1 { get; set; }

        public string Chamber2 { get; set; }

        public string Chamber3 { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }
    }
}