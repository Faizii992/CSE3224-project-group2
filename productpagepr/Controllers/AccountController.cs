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
    public class AccountController : Controller
    {

        //string constring = @"Data Source=DESKTOP-CIV7264\SQLEXPRESS;Initial Catalog=LifeServeBD_DB;Integrated Security=True";

        // GET: Account
        //for showing info in the account

        [Authorize] //only currently logged in user can go to their Account
        [HttpGet]
        public ActionResult Index()
        {


            /* using (MyDatabaseEntities dc = new MyDatabaseEntities())
             {
                 //loginuser.UserID = Convert.ToInt32(Session["UserID"]);
                  if(Session["userId"] != null)
                 {
                     return View();
                 }
                 /* SqlConnection con = new SqlConnection(constring);
                  String q = "select FirstName +' '+ LastName as Name,address,email,Phone,password,imageid,UserId from AccountInfo";
                  con.Open();
                  SqlCommand cmd = new SqlCommand(q, con);
                  DataTable dt = new DataTable();
                  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                  adapter.Fill(dt);
                 return RedirectToAction("Login");
             } */
            return View();
        }


        //for admin panel
        /* public ActionResult AdminAccountInfo()
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "select name,address,email,phone,password,imageid,id,id from AccountInfo";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             DataTable dt = new DataTable();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             adapter.Fill(dt);
             return View(dt);
         }


        //for edit image
         public ActionResult Edit1(int id)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "SELECT * from AccountInfo where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", id);

             cmd.ExecuteNonQuery();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             adapter.Fill(dt);
             accountModel ob = new accountModel();
             if (dt.Rows.Count == 1)
             {
                 ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                 ob.imgpath = dt.Rows[0][7].ToString();


             }


             return View(ob);
         }


         //for editing name and address

         public ActionResult Edit2(int id)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "SELECT * from AccountInfo where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", id);

             cmd.ExecuteNonQuery();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             adapter.Fill(dt);
             accountModel ob = new accountModel();
             if (dt.Rows.Count == 1)
             {
                 ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                 ob.firstname = dt.Rows[0][1].ToString();
                 ob.lastname = dt.Rows[0][2].ToString();
                 ob.address = dt.Rows[0][3].ToString();

             }


             return View(ob);
         }


         //for editing phone number

         public ActionResult Edit3(int id)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "SELECT * from AccountInfo where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", id);

             cmd.ExecuteNonQuery();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             adapter.Fill(dt);
             accountModel ob = new accountModel();
             if (dt.Rows.Count == 1)
             {
                 ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                 ob.email = dt.Rows[0][4].ToString();
                 ob.phone = dt.Rows[0][5].ToString();

             }


             return View(ob);
         }

         //for editing password
         public ActionResult Edit4(int id)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "SELECT * from AccountInfo where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", id);

             cmd.ExecuteNonQuery();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             adapter.Fill(dt);
             accountModel ob = new accountModel();
             if (dt.Rows.Count == 1)
             {
                 ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                 ob.password = dt.Rows[0][6].ToString();

             }


             return View(ob);
         }


         [HttpPost]
         public ActionResult Edit1(accountModel ob, HttpPostedFileBase file)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "Update AccountInfo set  imageid=@imageid where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", ob.id);


             if (file != null && file.ContentLength > 0)
             {
                 string filename = Path.GetFileName(file.FileName);
                 string imgpath = Path.Combine(Server.MapPath("/accountimage/"), filename);
                 file.SaveAs(imgpath);
                 cmd.Parameters.AddWithValue("@imageid", "/accountimage/" + file.FileName);

             }

             else
             {
                 cmd.Parameters.AddWithValue("@imageid", DBNull.Value);
             }


             cmd.ExecuteNonQuery();
             return RedirectToAction("Index");

         }

         [HttpPost]

         public ActionResult Edit2(accountModel ob)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "Update AccountInfo set FirstName=@firstname,LastName=@lastname,address=@address where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", ob.id);
             cmd.Parameters.AddWithValue("@firstname", ob.firstname);
             cmd.Parameters.AddWithValue("@lastname", ob.lastname);
             SqlParameter address = cmd.Parameters.AddWithValue("@address", ob.address);
             if (ob.address == null)
             {
                 address.Value = DBNull.Value;
             }


             cmd.ExecuteNonQuery();
             return RedirectToAction("Index");

         }

         [HttpPost]

         public ActionResult Edit3(accountModel ob)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "Update AccountInfo set phone=@phone where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);


             cmd.Parameters.AddWithValue("@id", ob.id);           
             SqlParameter phone = cmd.Parameters.AddWithValue("@phone", ob.phone);
             if (ob.phone == null)
             {
                 phone.Value = DBNull.Value;

             }


                 cmd.ExecuteNonQuery();
             return RedirectToAction("Index");

         }

         [HttpPost]

         public ActionResult Edit4(accountModel ob)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "Update AccountInfo set password=@password where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);999999999
             cmd.Parameters.AddWithValue("@id", ob.id);

             cmd.Parameters.AddWithValue("@password", ob.password);




             cmd.ExecuteNonQuery();
             return RedirectToAction("Index");

         }

        

         public ActionResult Delete(int id)
         {
             SqlConnection con = new SqlConnection(constring);
             String q = "Delete from AccountInfo where UserId=@id";
             con.Open();
             SqlCommand cmd = new SqlCommand(q, con);
             cmd.Parameters.AddWithValue("@id", id);

             cmd.ExecuteNonQuery();

             return RedirectToAction("AdminAccountInfo");
         }*/

    
        // for showing the current info
        [HttpGet]
        public ActionResult Edit1(int UserID)
        {

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.UserID == UserID).FirstOrDefault();
                if (v != null)

                {
                    v.UserID = Convert.ToInt32(Session["userId"]);
                    v.ImageAdd = @Session["imageAdd"].ToString();

                }

                return View(v);

            }
        }


        //for changing image

        [HttpPost]
        public ActionResult Edit1(User v, HttpPostedFileBase file)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var user = db.Users.Where(a => a.UserID == v.UserID).FirstOrDefault();
                if (user != null)
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string imgpath = Path.Combine(Server.MapPath("/accountimage/"), filename);
                        file.SaveAs(imgpath);
                        user.ImageAdd = "/accountimage/" + file.FileName;

                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();

                        Session["imageAdd"] = user.ImageAdd.ToString();

                    }
                    else
                    {
                        Session["imageAdd"] = DBNull.Value;
                    }
                }

            }
            return RedirectToAction("Index");
        }


        //for showing current name and address

        [HttpGet]
        public ActionResult Edit2(int UserID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.UserID == UserID).FirstOrDefault();
                if (v != null)

                {
                    v.UserID = Convert.ToInt32(Session["userId"]);
                    v.FirstName = @Session["firstName"].ToString();
                    v.LastName = @Session["lastName"].ToString();
                    v.Address = @Session["address"].ToString();

                }
                



                    return View(v);

            }

        }

        //for editing name and address

        [HttpPost]
        public ActionResult Edit2(User v)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var user = db.Users.Where(a => a.UserID == v.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.FirstName = v.FirstName;
                    user.LastName = v.LastName;
                    user.Address = v.Address;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    
                    Session["firstName"] = user.FirstName.ToString();
                    Session["lastName"] = user.LastName.ToString();

                    if (v.Address == null)
                    {
                        user.Address = "";
                        Session["address"] = "";
                    }
                    else
                    {
                        Session["address"] = user.Address.ToString();
                    }



                }
                



            }


            return RedirectToAction("Index");
        }

        //for shoing current phone no
        [HttpGet]
        public ActionResult Edit3(int UserID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.UserID == UserID).FirstOrDefault();
                if (v != null)

                {
                    v.UserID = Convert.ToInt32(Session["userId"]);
                    v.PhoneNo = @Session["phoneNo"].ToString();

                }

                return View(v);

            }

        }


        // for editing phone no

        [HttpPost]
        public ActionResult Edit3(User v)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var user = db.Users.Where(a => a.UserID == v.UserID).FirstOrDefault();
                if (user != null)
                {

                    user.PhoneNo = v.PhoneNo;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    if (v.PhoneNo == null)
                    {
                        user.PhoneNo = "";
                        Session["phoneNo"] = "";
                    }
                    else
                    {
                        Session["phoneNo"] = user.PhoneNo.ToString();
                    }



                }
                


            }


            return RedirectToAction("Index");
        }


        // for showing the current password field

        [HttpGet]
        public ActionResult Edit4(int UserID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.UserID == UserID).FirstOrDefault();
                if (v != null)

                {
                    v.UserID = Convert.ToInt32(Session["userId"]);
                    v.Password = @Session["password"].ToString();

                }

                return View(v);

            }

        }

        // for changing password

        [HttpPost]
        public ActionResult Edit4(User v)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var user = db.Users.Where(a => a.UserID == v.UserID).FirstOrDefault();
                if (user != null)
                {

                    user.Password = System.Web.Helpers.Crypto.Hash(v.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    Session["password"] = v.Password.ToString();

                }

            }


            return RedirectToAction("Index");
        }


        // for showing all the registered users in admin panel

        public ActionResult ShowAllUsers(String search)
        {
            MyDatabaseEntities db = new MyDatabaseEntities();
            List<User> listusers = db.Users.ToList();
            return View(db.Users.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
        }


        //logout

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
    }

}
   