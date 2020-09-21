using productpagepr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace productpagepr.Controllers
{
    public class AdminController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
        // GET: Admin
        //This is the admin homepage
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.TotalCustomers = GetTotalCustomers();
                model.TotalDoctors = GetTotalDoctors();
                model.TotalProducts = GetTotalProducts();
                model.LatestCustomer = GetLatestCustomer();
                model.TotalMessages = GetTotalMessages();


                return View(model);
                

            }
           

        }

        //Gets the customer details to show in the notifications
        public List<customerModel> GetCustomers()
        {
            List<customerModel> customers = new List<customerModel>();
          
            string query = "select CustomerId,OrderId,Name,Address,Email,Phone,Prescription1,Prescription2,Prescription3,convert(varchar, Date_Time,9) as Date_Time,DATENAME(dw, Date_Time) as Day,ID from CustomerTable";
            
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
                            customers.Add(new customerModel
                            {
                             
                                orderid = sdr["OrderId"].ToString(),
                                datetm = sdr["Date_Time"].ToString(),
                                day = sdr["Day"].ToString(),
                               

                            




                            });
                        }
                        con.Close();
                        return customers;
                    }
                }
            }
        }

       
       //Shows details of a particular notification
        [HttpGet]
        public ActionResult NotificationDetails(string orderID)
        {



            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.OrderedProducts = GetOrderedProducts(orderID);
                model.CustomerInfo = GetCustomerInfo(orderID);

                return View(model);


            }
           

        }

        //Gets the products ordered by customer
        public List<orderModel> GetOrderedProducts(string orderID)
        {
            List<orderModel> OrderedProducts = new List<orderModel>();
            string query = "select id,OrderId,ProductName,Category from OrderItem where OrderId LIKE '"+orderID+"'";
            //string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
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
                            OrderedProducts.Add(new orderModel
                            {
                                id = Convert.ToInt32(sdr["id"]),
                                productname = sdr["ProductName"].ToString(),
                               
                                category = sdr["category"].ToString(),
                                orderid = sdr["OrderId"].ToString(),
                               


                            });
                        }
                    }
                    con.Close();
                    return OrderedProducts;
                }
            }
        }

        //Gets the customer info to show in a table while checking prescription
        public List<customerModel> GetCustomerInfo(string orderID)
        {
            List<customerModel> CustomerInfo = new List<customerModel>();
            string query = "select ID,CustomerId,OrderId,Address,Email,Phone,Prescription1 from CustomerTable where OrderId LIKE '" + orderID + "'";
           
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
                            CustomerInfo.Add(new customerModel
                            {
                                id = Convert.ToInt32(sdr["ID"]),
                               address = sdr["Address"].ToString(),
                                phone = sdr["Phone"].ToString(),
                                email = sdr["Email"].ToString(),
                                orderid = sdr["OrderId"].ToString(),
                                imgpath= sdr["Prescription1"].ToString(),
                                customerid= sdr["CustomerId"].ToString(),

                            });
                        }
                    }
                    con.Close();
                    return CustomerInfo;
                }
            }
        }




        //Shows a partial view containing notification and admin account card
        public ActionResult Notification()
        {


            dynamic ob = new ExpandoObject();
            ob.notifs = getNotif();
            ob.msgs = getMsg();

            return PartialView(ob);
        }

        //Gets the top 3 helpline messages to show in notif
        private static List<HelplineModel> getMsg()
        {
            List<HelplineModel> msg = new List<HelplineModel>();
            string query = "select top 3 Id,Name, Email,Phone,Subject,Message,imgid,Prescription from Helpline order by Id desc";
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
                            msg.Add(new HelplineModel
                            {

                                Id = (int)sdr["Id"],
                                Name = sdr["Name"].ToString(),
                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            });
                        }
                        con.Close();
                        return msg;
                    }
                }
            }
        }



        //Gets the top 3 order notifications
        public List<customerModel> getNotif()
        {
            List<customerModel> notif = new List<customerModel>();
            string q = "select TOP 3 Name,Address,Email,Phone,convert(varchar, Date_Time, 5) as Date_Time,DATENAME(dw, Date_Time) as Day,OrderId from CustomerTable order by ID desc";

            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(q))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            notif.Add(new customerModel
                            {

                                //  P = (int)sdr["term_id"],
                                name= sdr["Name"].ToString(),
                                email = sdr["Email"].ToString(),
                                orderid = sdr["OrderId"].ToString(),
                                datetm=sdr["Date_Time"].ToString(),
                                day=sdr["Day"].ToString(),
                                phone=sdr["Phone"].ToString(),

                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            });
                        }
                        con.Close();
                        return notif;
                    }
                }
            }
        }


      //Shows all order notifications

        public ActionResult AllNotifications()
        {

            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                
                SqlConnection con = new SqlConnection(constring);

                String q = "select Name,Address,Email,Phone,convert(varchar, Date_Time, 9),DATENAME(dw, Date_Time) as Day,OrderId from CustomerTable order by ID desc";
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return View(dt);




            }
        }

        //Gets total number of customers
        public int GetTotalCustomers()
        {
            int customerCount;
            
            string query = "select Count(ID) from CustomerTable";

            SqlConnection con = new SqlConnection(constring);

            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
      
           
                    cmd.ExecuteNonQuery();
                    customerCount=(int)cmd.ExecuteScalar();
                    con.Close();
                        return customerCount;
                    
                
            }
        //Gets total number of products
        public int GetTotalDoctors()
        {
            int DoctorCount;

            string query = "select Count(DoctorId) from DoctorInfo";

            SqlConnection con = new SqlConnection(constring);

            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);


            cmd.ExecuteNonQuery();
           DoctorCount = (int)cmd.ExecuteScalar();
            con.Close();
            return DoctorCount;
        }
        //Gets total number of products
        public int GetTotalProducts()
        {
            int ProductCount;

            string query = "select Count(Product_ID) from Product";

            SqlConnection con = new SqlConnection(constring);

            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);


            cmd.ExecuteNonQuery();
            ProductCount = (int)cmd.ExecuteScalar();
            con.Close();
            return ProductCount;
        }
        //Gets total message no
        public int GetTotalMessages()
        {
            int MessageCount;

            string query = "select Count(Id) from Helpline";

            SqlConnection con = new SqlConnection(constring);

            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);


            cmd.ExecuteNonQuery();
            MessageCount = (int)cmd.ExecuteScalar();
            con.Close();
            return MessageCount;
        }
        //Retrieves  the latest customer info
        public List<customerModel> GetLatestCustomer()
        {
            List<customerModel> CustomerInfo = new List<customerModel>();
            string query = "select TOP 1 ID,CustomerId,OrderId,Address,Email,Phone,Prescription1,Name from CustomerTable ORDER BY ID desc";
            
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
                            CustomerInfo.Add(new customerModel
                            {
                                id = Convert.ToInt32(sdr["ID"]),
                                address = sdr["Address"].ToString(),
                                phone = sdr["Phone"].ToString(),
                                email = sdr["Email"].ToString(),
                                orderid = sdr["OrderId"].ToString(),
                                imgpath = sdr["Prescription1"].ToString(),
                                customerid = sdr["CustomerId"].ToString(),
                                name = sdr["Name"].ToString(),
                            });
                        }
                    }
                    con.Close();
                    return CustomerInfo;
                }
            }
        }

       
        //Shows admin related information in cards
        public ActionResult AdminProfileCardShow()
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "select Id,Name, Position,Department,Workplace,imgid from AboutUs";
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);
        }


        //Shows notifications of helpline messages
        public ActionResult Msg_notification()
        {


            SqlConnection con = new SqlConnection(constring);
            string qt = "select Id,Name, Email,Phone,Subject,Message,imgid,Prescription ,UserId from Helpline order by Id desc";
            con.Open();
            SqlCommand cmdt = new SqlCommand(qt, con);
            DataTable ddt = new DataTable();
            SqlDataAdapter adaptert = new SqlDataAdapter(cmdt);
            adaptert.Fill(ddt);

            return View(ddt);

        }

        //view helpline messages

        public ActionResult HelplineMsg(int Id)
        {


            string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

            SqlConnection con = new SqlConnection(constring);

            con.Open();

        
            string qt = "select Id,Name, Email,Phone,Subject,Message,imgid,Prescription,UserId from Helpline where Id like '" + Id + "' " +
                " and Id not in(select Msg_Id from Reply)";

            SqlCommand cmd = new SqlCommand(qt, con);
        

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            return View(dt);

        }

        public ActionResult ReplyMsg(int Id)
        {


            ReplyModel ob = new ReplyModel();
            ob.mesag = GetHelpmsg(Id);
            return View(ob);



        }

        //the form to see helpline messages
        public  List<HelplineModel> GetHelpmsg(int Id)
        {
            List<HelplineModel> mesag = new List<HelplineModel>();
            string query = "select Id,Name, Email,Phone,Subject,Message,imgid,Prescription,UserId from Helpline where Id like '" + Id + "'";

          
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
                            mesag.Add(new HelplineModel
                            {
                                Id = (int)(sdr["Id"]),


                                Email = sdr["Email"].ToString(),

                                Subject = sdr["Subject"].ToString(),
                                Message = sdr["Message"].ToString(),
                                imgid = sdr["imgid"].ToString(),
                                Prescription = sdr["Prescription"].ToString(),
                                UserId = sdr["UserId"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return mesag;
                }
            }
        }

        //The form to reply to a helpline message
        [HttpPost]
        public ActionResult ReplyMsg(ReplyModel ob, HttpPostedFileBase file, int Id)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            string q11 = "SELECT UserId  from Helpline where Id='" + Id + "' ";
            SqlCommand cmd11 = new SqlCommand(q11, con);

            String UserId = (String)cmd11.ExecuteScalar();

            String q = "INSERT into Reply(Msg_id,Reply,UserId) " +
               "values(@Msg_Id,@Reply,@UserId)";
            // string q="Insert Msg_id,Reply,UserId into Reply where Msg_Id=@Msg_Id and Reply=@Reply And (Select UserId From Helpline where "

            SqlCommand cmd = new SqlCommand(q, con);
            //cmd.Parameters.AddWithValue("@Qsn_no", ob.Qsn_no);
            //cmd.Parameters.AddWithValue("@Id", ob.Id);

            cmd.Parameters.AddWithValue("@Msg_Id", Id);
            cmd.Parameters.AddWithValue("@Reply", ob.Reply);
            cmd.Parameters.AddWithValue("@UserId", UserId);



            //cmd.Parameters.AddWithValue("@UserId",);



            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");


        }



        [HttpGet]
        //Opens up admin login form
        public ActionResult AdminLogin()
        {
            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");

            }
        }

        //Performs admin login
        //Login POST
        [HttpPost]

        public ActionResult AdminLogin(AboutUsModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Select * From AboutUs where Email=@Email and Pass=@Pass ";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            //cmd.Parameters.AddWithValue("@Qsn_no", ob.Qsn_no);
            //cmd.Parameters.AddWithValue("@Id", ob.Id);

            //  cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Email", ob.Email);
            cmd.Parameters.AddWithValue("@Pass", ob.Pass);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                ob.Name = dt.Rows[0][1].ToString();
                ob.imgid = dt.Rows[0][5].ToString();
                Session["imgid"] = ob.imgid;
                Session["Name"] = ob.Name;


            }

            // cmd.Parameters.AddWithValue("@Department", ob.Department);
            //  cmd.Parameters.AddWithValue("@Position", ob.Position);
            //  cmd.Parameters.AddWithValue("@Workplace", ob.Workplace);
            // cmd.Parameters.AddWithValue("@Email", ob.Email);
            //cmd.Parameters.AddWithValue("@imgid", ob.imgid);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                //Session["Name"] = ob.Name.ToString();
                Session["Email"] = ob.Email.ToString();

                Session["Pass"] = ob.Pass.ToString();
                //  Session["Position"] = Position;
                // Session["Department"] = "+AboutusController.Deartment";
                // Session["Workplace"] = "+AboutusController.Workplace";
                // Session["Email"] = "+AboutusController.Email";
                // Session["imgid"] = "+AboutusController.imgid";

                return RedirectToAction("Index");

            }
            else
            {
                string message = "Incorrect Password or Email";
                ViewBag.Message = message;
                //ViewData["Messages"] = "Incorrect Name or Password !";
                return View();
            }
           

          
        }

        //Performs logout
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }



    }
}