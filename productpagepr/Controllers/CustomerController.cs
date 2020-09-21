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
    public class CustomerController : Controller
    {
        // GET: Customer


        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
        // shows customer info in ssubmission page
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select CustomerId,OrderId,Name,Address,Email,Phone,Prescription1,Prescription2,Prescription3,convert(varchar, Date_Time, 9),DATENAME(dw, Date_Time) as Day,ID from CustomerTable ORDER BY Date_Time desc";

            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return View(dt);

        }


        // used to set status for an order
        public ActionResult message()
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select CustomerId,OrderId,Name,Address,Email,Phone,Prescription1,Prescription2,Prescription3,convert(varchar, Date_Time, 9),DATENAME(dw, Date_Time) as Day,ID,OrderStatus, AcceptDate from CustomerTable order by AcceptDate desc";

            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return View(dt);

        }

        // show customer table in admin panel and search
        public ActionResult AdminCustomerInfo(string searchString)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select CustomerId,OrderId,Name,Address,Email,Phone,Prescription1,Prescription2,Prescription3,convert(varchar, Date_Time, 9),DATENAME(dw, Date_Time) as Day,OrderStatus,ID from CustomerTable" +
                " where OrderId Like '%" + searchString + "%' " +
                "or CustomerId Like '%" + searchString + "%' " +
                "or Name Like '%" + searchString + "%' " +
                "or Address Like '%" + searchString + "%' " +
                "or Email Like '%" + searchString + "%' " +
                "or Phone Like '%" + searchString + "%' " +
                "order by Date_Time desc";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return View(dt);

        }

        // view 1st prescription
        public ActionResult ViewPrescription1(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from CustomerTable where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            customerModel ob = new customerModel();
            if (dt.Rows.Count == 1)
            {
                ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());

                ob.imgpath = dt.Rows[0][7].ToString();

            }

            ViewData["img"] = ob.imgpath;
            return View(ob);


        }

        /*
        [HttpPost]
        public ActionResult ViewPrescription1(customerModel ob, HttpPostedFileBase file)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Select Prescription1 from CustomerTable  where ID= @id";



            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", ob.tableid);
            
            ViewData["img"] = "/orderimages/" + file.FileName;

          
            cmd.ExecuteNonQuery();
            return View(ob);
        }
        */
        // view 2nd prescription
        public ActionResult ViewPrescription2(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from CustomerTable where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            customerModel ob = new customerModel();
            if (dt.Rows.Count == 1)
            {
                ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());

                ob.imgpath = dt.Rows[0][8].ToString();

            }

            ViewData["img"] = ob.imgpath;
            return View(ob);


        }

        
        // view 3rd prescription
        public ActionResult ViewPrescription3(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from CustomerTable where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            customerModel ob = new customerModel();
            if (dt.Rows.Count == 1)
            {
                ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());

                ob.imgpath = dt.Rows[0][9].ToString();

            }

            ViewData["img"] = ob.imgpath;
            return View(ob);


        }

    
        //delete a record
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from CustomerTable where id=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("AdminCustomerInfo");
        }

        //update accept

        public ActionResult Accept(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            DateTime now = DateTime.Now;
            String message = "Your Order for the mentioned Order No. has been accepted. We will deliver your products as soon as possible. Thank You for staying with us.";
            String q = "Update Customertable set OrderStatus = @orderstatus, AcceptDate = @acceptdate where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@orderstatus", message);
            cmd.Parameters.AddWithValue("@acceptdate", now);

            cmd.ExecuteNonQuery();

            return RedirectToAction("AdminCustomerInfo");

        }

        // Update reject
        public ActionResult Reject(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            DateTime now = DateTime.Now;
            String message = "Sorry.Your Order for the mentioned Order No. has been rejected due to some issues. The issues may include prescription authenticities, avaiability of products etc. Please Call: 01712871677, 01720041460 for further informations.";
            String q = "Update CustomerTable set OrderStatus = @orderstatus, AcceptDate = @acceptdate where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@orderstatus", message);
            cmd.Parameters.AddWithValue("@acceptdate", now);
            cmd.ExecuteNonQuery();

            return RedirectToAction("AdminCustomerInfo");

        }





    }
}