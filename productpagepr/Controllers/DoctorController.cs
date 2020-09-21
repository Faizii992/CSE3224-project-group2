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
using static System.Net.WebRequestMethods;
using System.Dynamic;
using System.Configuration;
using System.Web.ModelBinding;
using System.Data.OleDb;
using System.Web.UI;
using Microsoft.AspNetCore.Http;


namespace productpagepr.Controllers
{
    public class DoctorController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
        // GET: Doctor

        // show doctor info in doctor's page
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select DoctorName,Speciality,ImgId, DoctorId from DoctorInfo";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            //cmd.Parameters.AddWithValue("@DoctorId", category);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return View(dt);
        }

        // show doc category list
        public ActionResult CategoryList()
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Select Category from Category group by Category";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            //cmd.Parameters.AddWithValue("@DoctorId", category);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return View(dt);
        }



        //for category wise selection
        public ActionResult CategoryIndex(string category)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT DoctorInfo.DoctorName as doc_name , DoctorInfo.Speciality as doc_spc, DoctorInfo.ImgId as doc_img, Category.Category as D_category, DoctorInfo.DoctorId as doc_id  FROM DoctorInfo INNER JOIN Category ON DoctorInfo.DoctorId = Category.DoctorId where Category.Category LIKE '%" + category + "%'  ";


            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);


            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return View(dt);
        }

        // for category wise heading
        public ActionResult CategoryHeading(string category)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT distinct Category.Category as D_category  FROM DoctorInfo INNER JOIN Category ON DoctorInfo.DoctorId = Category.DoctorId where Category.Category LIKE '%" + category + "%'  ";


            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);


            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return View(dt);
        }



        //admin panel and search
        public ActionResult AdminDoctorInfo(string searchString)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select DoctorName,Descriptions,Speciality,ImgId,Chamber1,Chamber2,Chamber3,Email,Phone,DoctorId from DoctorInfo" +
                " where DoctorName Like '%" + searchString + "%' " +
                "or Speciality Like '%" + searchString + "%' " +
                "or Descriptions Like '%" + searchString + "%' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }

        // admin panel and search
        public ActionResult AdminDoctorCategory(string searchString)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select DoctorId, Category, ID from Category" +
                " where Category Like '%" + searchString + "%' ";
                
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }



        //for creating a new record
        public ActionResult Create()
        {
            doctorModel ob = new doctorModel();
            return View(ob);
        }

        [HttpPost]
        public ActionResult Create(doctorModel ob, categoryModel ob2, HttpPostedFileBase file)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "INSERT into DoctorInfo(DoctorName,Descriptions,Speciality,Imgid,Chamber1,Chamber2,Chamber3,Phone,Email) " +
                "values(@name,@Description,@Speciality,@imgid,@Chamber1,@Chamber2,@Chamber3,@ContactNo,@Email)";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            cmd.Parameters.AddWithValue("@name", ob.name);
            cmd.Parameters.AddWithValue("@Description", ob.Description);
            cmd.Parameters.AddWithValue("@Speciality", ob.Speciality);

            SqlParameter chamber1 = cmd.Parameters.AddWithValue("@Chamber1", ob.Chamber1);
            if (ob.Chamber1 == null)
            {
                chamber1.Value = "NULL";
            }
            SqlParameter chamber2 = cmd.Parameters.AddWithValue("@Chamber2", ob.Chamber2);
            if (ob.Chamber2 == null)
            {
                chamber2.Value = "NULL";
            }
            SqlParameter chamber3 = cmd.Parameters.AddWithValue("@Chamber3", ob.Chamber3);
            if (ob.Chamber3 == null)
            {
                chamber3.Value = "NULL";
            }
            cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
            cmd.Parameters.AddWithValue("@Email", ob.Email);

            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/doctorimages/"), filename);
                file.SaveAs(imgpath);

            }
            cmd.Parameters.AddWithValue("@imgid", "/doctorimages/" + file.FileName);



            cmd.ExecuteNonQuery();
            con.Close();

            String q2 = "Select top 1 DoctorId from DoctorInfo order by DoctorId desc";
            SqlCommand cmd2 = new SqlCommand(q2, con);
            con.Open();
            int doctorid = Convert.ToInt32(cmd2.ExecuteScalar());


            cmd2.ExecuteNonQuery();
            con.Close();


            String q3 = "INSERT into Category(DoctorId,Category) " +
                "values(@doctorid,@category)";

            SqlCommand cmd3 = new SqlCommand(q3, con);
            con.Open();

            cmd3.Parameters.AddWithValue("@doctorid", doctorid);
            cmd3.Parameters.AddWithValue("@category", ob2.category);
            cmd3.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("AdminDoctorInfo");


        }


        //for deleting record
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from DoctorInfo where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            con.Close();

            String q1 = "Delete from Category where DoctorId=@id";
            con.Open();
            SqlCommand cmd1 = new SqlCommand(q1, con);
            cmd1.Parameters.AddWithValue("@id", id);

            cmd1.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("AdminDoctorInfo");
        }



        //for showing  info in edit page
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from DoctorInfo where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            doctorModel ob = new doctorModel();
            if (dt.Rows.Count == 1)
            {

                ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.name = dt.Rows[0][1].ToString();
                ob.Description = dt.Rows[0][2].ToString();
                ob.Speciality = dt.Rows[0][3].ToString();
                ob.Chamber1 = dt.Rows[0][5].ToString();
                ob.Chamber2 = dt.Rows[0][6].ToString();
                ob.Chamber3 = dt.Rows[0][7].ToString();
                ob.ContactNo = dt.Rows[0][8].ToString();
                ob.Email = dt.Rows[0][9].ToString();


            }


            return View(ob);
        }


        //for showing image in edit page

        public ActionResult EditImg(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from DoctorInfo where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            doctorModel ob = new doctorModel();
            if (dt.Rows.Count == 1)
            {

                ob.imgpath = dt.Rows[0][4].ToString();


            }

            ViewData["img"] = ob.imgpath;
            return View(ob);
            
        }

        // for editing info
        [HttpPost]
        public ActionResult Edit(doctorModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update DoctorInfo set Doctor=@name,description=@Description,speciality=@Speciality,chamber1=@Chamber1,chamber2=@Chamber2,chamber3=@Chamber3, phone=@ContactNo, email=@Email where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", ob.id);
            cmd.Parameters.AddWithValue("@name", ob.name);
            cmd.Parameters.AddWithValue("@Description", ob.name);
            cmd.Parameters.AddWithValue("@Speciality", ob.Speciality);
            cmd.Parameters.AddWithValue("@Chamber1", ob.Chamber1);
            cmd.Parameters.AddWithValue("@Chamber2", ob.Chamber2);
            cmd.Parameters.AddWithValue("@Chamber3", ob.Chamber3);
            cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
            cmd.Parameters.AddWithValue("@Email", ob.Email);




            cmd.ExecuteNonQuery();



            return RedirectToAction("AdminDoctorInfo");

        }


        //for editing image

        [HttpPost]
        public ActionResult EditImg(doctorModel ob, HttpPostedFileBase file)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update DoctorInfo set  imgid=@imgid where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", ob.id);


            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/doctorimages/"), filename);
                file.SaveAs(imgpath);

            }
            cmd.Parameters.AddWithValue("@imgid", "/doctorimages/" + file.FileName);

            ViewData["img"] = ob.imgpath;
            cmd.ExecuteNonQuery();
            return RedirectToAction("AdminDoctorInfo");

        }


        //for Detail View
        public ActionResult Details(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from DoctorInfo where DoctorId=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return View(dt);
        }


    }
}