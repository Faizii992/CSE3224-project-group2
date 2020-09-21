using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace productpagepr.Models
{
    public class AboutUsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public string Position { get; set; }

        public string Department { get; set; }

        public string Workplace { get; set; }
        public string imgid { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }


    }
}