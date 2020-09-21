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


namespace productpagepr.Controllers
{
    public class CartController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

        [HttpGet]
        public ActionResult Cart()
        {
            cartModel ob = new cartModel();
            return View(ob);
        }
        

        /*
        [HttpPost]
        public ActionResult Cart(cartModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "INSERT into Cart(Product_Name,Price,quantity,Category,UserId) " +
                "values(@name,@price,@quantity,@category,@userid)";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            cmd.Parameters.AddWithValue("@name", ob.name);
            cmd.Parameters.AddWithValue("@price", ob.price);
            cmd.Parameters.AddWithValue("@quantity", ob.quantity);
            cmd.Parameters.AddWithValue("@category", ob.category);
            cmd.Parameters.AddWithValue("@userid", ob.userid);


            cmd.ExecuteNonQuery();
                return RedirectToAction("Index","Cart");
            
        }*/

        // insert item into the cart
        
        [HttpPost]
        public ActionResult Cart(cartModel ob )
        {
            SqlConnection con = new SqlConnection(constring);

            String q1 = "select count(Product_Name) from Cart where UserId = " + ob.userid +
                " and Product_Name LIKE '" + ob.name + "'";
            SqlCommand cmd1 = new SqlCommand(q1, con);
            con.Open();
            int cnt = Convert.ToInt32(cmd1.ExecuteScalar());
            con.Close();

            if (cnt == 0)
            {
                String q = "INSERT into Cart(Product_Name,Price,quantity,Category,UserId) " +
                    "values(@name,@price,@quantity,@category,@userid)";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();

                cmd.Parameters.AddWithValue("@name", ob.name);
                cmd.Parameters.AddWithValue("@price", ob.price);
                cmd.Parameters.AddWithValue("@quantity", ob.quantity);
                cmd.Parameters.AddWithValue("@category", ob.category);
                cmd.Parameters.AddWithValue("@userid", ob.userid);


                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Index", "Cart");
                
            }

            else 
            {
                String q3 = "Select quantity from Cart where Product_Name = '" + ob.name + "'" +
                     "and UserId = " + ob.userid;
                SqlCommand cmd3 = new SqlCommand(q3, con);
                con.Open();
                int quantityno = Convert.ToInt32(cmd3.ExecuteScalar());
                int quantityupdated = quantityno + ob.quantity;
                con.Close();
                String q2 = "Update Cart set Quantity=" + quantityupdated + " where Product_Name = '" + ob.name + "'" +
                    "and UserId = " + ob.userid;


                SqlCommand cmd2 = new SqlCommand(q2, con);
                con.Open();
               

                cmd2.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Index", "Cart");
            }
            

        }

    


        [Authorize] //only currently logged in user can go to their Account
        [HttpGet]


        // show cart table in cart page
        public ActionResult Index()
            {
                
                
                SqlConnection con = new SqlConnection(constring);
                String q = "select Product_Name,Price,quantity,Category,Total_price,UserId,ID from Cart";
               
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);
                
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
               
                adapter.Fill(dt);
                
                return View(dt);

            }

        // delete a record
        public ActionResult Delete(int id)
        { 
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from Cart where id=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }


        //show the quantity and for an item in cart table
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from Cart where ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cartModel ob = new cartModel();
            if (dt.Rows.Count == 1)
            {
                ob.name = dt.Rows[0][1].ToString();
                ob.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.price = Convert.ToDouble(dt.Rows[0][2].ToString());
                ob.quantity = Convert.ToInt32(dt.Rows[0][3].ToString());
                ob.category = dt.Rows[0][4].ToString();
                ob.totalprice = Convert.ToDouble(dt.Rows[0][5].ToString());


            }


            return View(ob);


        }


        // edit the quantity for an item in the cart
        [HttpPost]
        public ActionResult Edit(cartModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update Cart set Quantity=@quantity where ID= @id";



            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", ob.id);
            cmd.Parameters.AddWithValue("@quantity", ob.quantity);



 


            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }




    }

    }
