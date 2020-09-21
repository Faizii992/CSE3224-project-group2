using productpagepr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace productpagepr.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

        // show payment table in admin panel
        public ActionResult Index(string searchString)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select CustomerId,OrderId,Amount,DeliveryCharge,TotalPrice,ReceivedDate,PaymentId from Payment" +
                " where OrderId Like '%" + searchString + "%' " +
                "or CustomerId Like '%" + searchString + "%' " +
                "or PaymentId Like '%" + searchString + "%' " +
                "or ReceivedDate Like '%" + searchString + "%'" +
                "order by ReceivedDate  ";
                
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return View(dt);

        }

        //delete a record
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from Payment where PaymentId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        
        // update the received date
        //[HttpPost]
        public ActionResult ReceivedDate(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            DateTime now = DateTime.Now;
            String q = "Update Payment set ReceivedDate = @receiveddate where PaymentId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@receiveddate", now);
            
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");

        }
    }
}