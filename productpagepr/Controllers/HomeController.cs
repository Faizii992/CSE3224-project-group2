using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace productpagepr.Controllers
{
   
    public class HomeController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
           

            SqlConnection con = new SqlConnection(constring);
            String q = "select TOP 3 * from Product";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);


            return View(dt);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}