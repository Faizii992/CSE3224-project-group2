using productpagepr.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI;

namespace productpagepr.Controllers
{
    public class HelplineController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

        // GET: Helpline
        public ActionResult Index()
        {
          

            HelplineModel ob = new HelplineModel();
            ob.Faqs = GetFaqs();
            return View(ob);



        }

        private static List<FAQModel> GetFaqs()
        {
            List<FAQModel> faqs = new List<FAQModel>();
            string query = "SELECT * FROM FAQ";
            string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            faqs.Add(new FAQModel
                            {
                                Qsn_no = (int)sdr["Qsn_no"],
                                Question = sdr["Question"].ToString(),
                                Answer = sdr["Answer"].ToString(),
                            });
                        }
                    }
                    con.Close();
                    return faqs;
                }
            }
        }

        [HttpPost]
        public ActionResult Index(HelplineModel ob, HttpPostedFileBase file)
        {
            ViewBag.Message = "Message Sent Successfully ...";

            SqlConnection con = new SqlConnection(constring);

            String q = "INSERT into Helpline(Name, Email,Phone,Subject,Message,imgid,Prescription,UserId ) " +
                "values(@Name,@Email,@Phone,@Subject,@Message,@imgid,@Prescription,@UserId)";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            cmd.Parameters.AddWithValue("@Name", ob.Name);
            if (ob.Email != null)
            {
                cmd.Parameters.AddWithValue("@Email", ob.Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Phone", ob.Phone);

            cmd.Parameters.AddWithValue("@Subject", ob.Subject);
            cmd.Parameters.AddWithValue("@Message", ob.Message);
            cmd.Parameters.AddWithValue("@Prescription", ob.Prescription);


            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/Prescriptions/"), filename);
                file.SaveAs(imgpath);

            }
            if (file != null)
            {
                cmd.Parameters.AddWithValue("@imgid", "/Prescriptions/" + file.FileName);

            }
            else
            {
                cmd.Parameters.AddWithValue("@imgid", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(Session["userId"] as string)) { 
                cmd.Parameters.AddWithValue("@UserId", Session["userId"].ToString());

            }
            else
            {
                cmd.Parameters.AddWithValue("@UserId", DBNull.Value);

            }

             cmd.ExecuteNonQuery();
            return RedirectToAction("Index");




        }

        public ActionResult FaqPartial()
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "select * from Faqs";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return PartialView(dt);

        }

        public ActionResult CreateFaq()
        {
            FAQModel ob = new FAQModel();
            return View();
        }

        [HttpPost]
        public ActionResult CreateFaq(FAQModel ob, HttpPostedFileBase file)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "INSERT into FAQs(Question,Answer  ) " +
                "values(@Question,@Answer )";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            //cmd.Parameters.AddWithValue("@Qsn_no", ob.Qsn_no);

            cmd.Parameters.AddWithValue("@Question", ob.Question);
            cmd.Parameters.AddWithValue("@Answer", ob.Answer);


            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");


        }

        public ActionResult FAQTable()
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "select * from FAQs";
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }

        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from FAQs where Qsn_no=@Qsn_no";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Qsn_no", id);

            cmd.ExecuteNonQuery();

            return RedirectToAction("FaqTable");
        }


        public ActionResult EditFaq(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from FAQs where Qsn_no=@Qsn_no";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Qsn_no", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
           
            FAQModel ob = new FAQModel();
            if (dt.Rows.Count == 1)
            {

                ob.Qsn_no= Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.Question = dt.Rows[0][1].ToString();
                ob.Answer = dt.Rows[0][2].ToString();
                


            }


            return View(ob);
        }
        [HttpPost]
        public ActionResult EditFaq(FAQModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update FAQs set Question=@ Question,Answer=@Answer where Qsn_no=@Qsn_no";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Qsn_no", ob.Qsn_no);
            cmd.Parameters.AddWithValue("@Question", ob.Question);
            cmd.Parameters.AddWithValue("@Answer", ob.Answer);



            cmd.ExecuteNonQuery();



            return RedirectToAction("FaqTable");

        }



    }
}