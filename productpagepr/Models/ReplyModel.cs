using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace productpagepr.Models
{
    public class ReplyModel
    {
        [DataType(DataType.MultilineText)]
        public int Id { get; set; }
        [ForeignKey("Helpline")]
        public virtual int Msg_Id { get; set; }

        //public int Msg_Id { get; set; }
        public string Reply{ get; set; }
        public string UserId { get; set; }

        public List<HelplineModel> mesag { get; set; }

    }
}