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
    public class AdminOrderController : Controller
    {
        // GET: AdminOrder

        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";


        // show order table in admin panel
        public ActionResult AdminOrderInfo(string searchString)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select CustomerId,OrderId,ProductName,Category,Price,Quantity,TotalPrice,convert(varchar, Date_Time, 9),DATENAME(dw, Date_Time) as Day,id from OrderItem  " +
                " where OrderId Like '%" + searchString + "%' " +
                "or CustomerId Like '%" + searchString + "%' " +
                "or ProductName Like '%" + searchString + "%' " +
                "order by Date_Time desc";

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
            String q = "Delete from OrderItem where id=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            return RedirectToAction("AdminOrderInfo");
        }
    }
}