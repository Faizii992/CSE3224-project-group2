using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace productpagepr.Models
{
    public class orderModel
    {
        public int id { get; set; }

        public string userid { get; set; }
        public string orderid { get; set; }

        public string productname { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        public double totalprice { get; set; }
        public string datetm { get; set; }
        public string day { get; set; }
        //public DateTime? SelectedStartDate { get; set; }

        /*
        public string prescription1 { get; set; }
        public string prescription2 { get; set; }
        public string prescription3 { get; set; }
        public int customerid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }

        public string phone { get; set; }*/


    }

    internal struct NewStruct
    {
        public object Item1;
        public object Item2;

        public NewStruct(object item1, object item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            return obj is NewStruct other &&
                   EqualityComparer<object>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<object>.Default.Equals(Item2, other.Item2);
        }

        public override int GetHashCode()
        {
            int hashCode = -1030903623;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Item1);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Item2);
            return hashCode;
        }

        public void Deconstruct(out object item1, out object item2)
        {
            item1 = Item1;
            item2 = Item2;
        }

        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}