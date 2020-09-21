using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace productpagepr.Models
{
    public class HelplineModel
    {
        [DataType(DataType.MultilineText)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Your name")]
        public string Name { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        [EmailAddress(ErrorMessage = "Follow the pattern 'xyz@gmail.com'")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a 11 digit valid phone number. ")]

        public string Phone { get; set; }
        public string Subject { get; set; }

        public string Message { get; set; }
        public string imgid { get; set; }

        public string Prescription { get; set; }
        public string UserId { get; set; }

        public List<FAQModel> Faqs { get; set; }

    }
}