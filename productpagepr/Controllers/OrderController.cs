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
    public class OrderController : Controller


    {



            // GET: Order
            string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";
            public ActionResult Index()
            {
                dynamic model = new ExpandoObject();
                model.Carts = GetCarts();
                //model.Users = GetUsers();
                model.Customers = GetCustomers();

            return View(model);
            }
              
              // for showing cart info in order page
              public List<cartModel> GetCarts()
              {
                    List<cartModel> carts = new List<cartModel>();
                    string query = "select UserId,Product_Name,Price,quantity,Category,Total_price from Cart";
                    
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
                                    carts.Add(new cartModel
                                    {
                                        userid = sdr["UserId"].ToString(),
                                        name = sdr["Product_Name"].ToString(),
                                        price = Convert.ToDouble(sdr["Price"]),
                                        quantity = Convert.ToInt32(sdr["quantity"]),
                                        category = sdr["Category"].ToString(),
                                        totalprice =Convert.ToDouble(sdr["Total_price"]),
                                    }) ;
                                }
                            }
                            con.Close();
                            return carts;
                        }
                    }
       }

       
       /*
        private static List<User> GetUsers()
             {
                List<User> users = new List<User>();
                string query = "select UserID,FirstName + ' ' + LastName as name ,Address,EmailID,PhoneNo from Users";
                string constring = @"Data Source=DESKTOP-CIV7264\SQLEXPRESS;Initial Catalog=LifeServeBD_DB;Integrated Security=True";
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
                                 users.Add(new User
                                 {
                                     UserID = Convert.ToInt32(sdr["UserID"]),
                                     FirstName = sdr["name"].ToString(),
                                     LastName = sdr["name"].ToString(),
                                     Address = sdr["Address"].ToString(),
                                     EmailID = sdr["EmailID"].ToString(),
                                     PhoneNo = sdr["PhoneNo"].ToString(),
                                     

                                 });
                             }
                             con.Close();
                             return users;
                         }
                     }
                 }
             }
       */

        // for showing order summary
        
        public List<customerModel> GetCustomers()
        {
            List<customerModel> customers = new List<customerModel>();
            string query = "select CustomerId,OrderId,Name,Address,Email,Phone,Prescription1,Prescription2,Prescription3,convert(varchar, Date_Time, 9) as Date_Time,DATENAME(dw, Date_Time) as Day,OrderStatus, AcceptDate,ID from CustomerTable ORDER BY Date_Time desc";
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
                            customers.Add(new customerModel
                            {
                                customerid = sdr["CustomerId"].ToString(),
                                orderid = sdr["OrderId"].ToString(),
                                datetm = sdr["Date_Time"].ToString(),
                                day = sdr["Day"].ToString(),
                                orderstatus = sdr["OrderStatus"].ToString(),
                                acceptdate = sdr["AcceptDate"].ToString()
                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            });
                        }
                        con.Close();
                        return customers;
                    }
                }
            }
        }



        


        
        public ActionResult Order()
        {
            customerModel obj = new customerModel();
            return View(obj);
        }


        // order process
        [HttpPost]
        
        public ActionResult Order(customerModel obj, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, double amount)
        {
            String query = "SELECT * from Cart where UserId = " + UserController.id;
            SqlConnection con = new SqlConnection(constring);
            String query2 = "SELECT COUNT(*) from Cart where UserId = " + UserController.id;
            SqlCommand command1 = new SqlCommand(query2, con);
            con.Open();

            int cnt = Convert.ToInt32(command1.ExecuteScalar());
            var ob = new orderModel[cnt];
            var ob1 = new orderModel();
            int i;
            for (i = 0; i < cnt; i++)
                ob[i] = new orderModel();

            con.Close();
            SqlCommand command2 = new SqlCommand(query, con);
            con.Open();

            i = 0;
            SqlDataReader read = command2.ExecuteReader();
                while(read.Read())
                {
                    ob[i].userid = read["UserId"].ToString();
                    ob[i].productname = read["Product_Name"].ToString();
                    ob[i].category = read["Category"].ToString();
                    ob[i].price = Convert.ToDouble(read["Price"]);
                    ob[i].quantity =Convert.ToInt32( read["quantity"]);
                    i++;
                }
                con.Close();

            String q = "INSERT into OrderItem(OrderId,CustomerId,ProductName,Category,Price,Quantity) " 
            + "values(@orderid,@userid,@productname,@category,@price,@quantity)" ;
            System.Guid orderid = System.Guid.NewGuid();

            for (int j = 0; j < i; j++)
            {

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();

                cmd.Parameters.AddWithValue("@orderid", orderid.ToString());
                cmd.Parameters.AddWithValue("@productname", ob[j].productname);
                cmd.Parameters.AddWithValue("@userid", ob[j].userid);
                
                cmd.Parameters.AddWithValue("@category", ob[j].category);
                cmd.Parameters.AddWithValue("@price", ob[j].price);
                cmd.Parameters.AddWithValue("@quantity", ob[j].quantity);
                cmd.ExecuteNonQuery();
                con.Close();
            }



            String query3 = "INSERT into CustomerTable(CustomerId,Name,Address,Email,Phone,OrderId,Prescription1,Prescription2,Prescription3) " +
            "values(@customerid,@name,@address,@email,@phone,@orderid,@prescription1,@prescription2,@prescription3)";

            SqlCommand command3 = new SqlCommand(query3, con);
            con.Open();
            command3.Parameters.AddWithValue("@customerid", obj.id);
            
            command3.Parameters.AddWithValue("@name", obj.name);
           
            command3.Parameters.AddWithValue("@address", obj.address);
            
            command3.Parameters.AddWithValue("@email", obj.email);
            
            command3.Parameters.AddWithValue("@phone", obj.phone);
            command3.Parameters.AddWithValue("@orderid", orderid.ToString());
            


            

            
            if (file1 != null && file1.ContentLength > 0)
            {
                string filename = Path.GetFileName(file1.FileName);
                string imgpath = Path.Combine(Server.MapPath("/orderimages/"), filename);
                file1.SaveAs(imgpath);
                command3.Parameters.AddWithValue("@prescription1", "/orderimages/" + file1.FileName);

            }

            else
            {
                command3.Parameters.AddWithValue("@prescription1", DBNull.Value);
            }



            if (file2 != null && file2.ContentLength > 0)
            {
                string filename = Path.GetFileName(file2.FileName);

                string imgpath = Path.Combine(Server.MapPath("/orderimages/"), filename);
                file2.SaveAs(imgpath);
                command3.Parameters.AddWithValue("@prescription2", "/orderimages/" + file2.FileName);
            }

            else
            {
                command3.Parameters.AddWithValue("@prescription2", DBNull.Value);
            }


            if (file3 != null && file3.ContentLength > 0)
            {
                string filename = Path.GetFileName(file3.FileName);

                string imgpath = Path.Combine(Server.MapPath("/orderimages/"), filename);
                file3.SaveAs(imgpath);
                command3.Parameters.AddWithValue("@prescription3", "/orderimages/" + file3.FileName);
            }

            else
            {
                command3.Parameters.AddWithValue("@prescription3", DBNull.Value);
            }

            command3.ExecuteNonQuery();
            con.Close();


            double deliverycharge = 60.0;

            String query5 = "INSERT into Payment(CustomerId,OrderId,Amount,DeliveryCharge) " +
            "values(@customerid,@orderid,@amount,@deliverycharge)";

            SqlCommand command5 = new SqlCommand(query5, con);
            con.Open();
            command5.Parameters.AddWithValue("@customerid", obj.id);
     
            command5.Parameters.AddWithValue("@orderid", orderid.ToString());
            command5.Parameters.AddWithValue("@amount", amount);
            command5.Parameters.AddWithValue("@deliverycharge", deliverycharge);


            command5.ExecuteNonQuery();
            con.Close();

            String query4 = "Delete from Cart where UserId = " + UserController.id;
            con.Open();
            SqlCommand command4 = new SqlCommand(query4, con);
            

            command4.ExecuteNonQuery();

            con.Close();

            return RedirectToAction("Index","Customer");



        }

    

    }

}