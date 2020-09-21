using productpagepr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace productpagepr.Controllers
{
    public class ProductController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

        [HttpGet]

        
        //Primary homepage of the product page
        public ActionResult Index(string searchString)
        {


            int NoofProductstoShow;
           
            NoofProductstoShow = 100;
            DataTable dt = new DataTable();
            
           
                SqlConnection con = new SqlConnection(constring);
                String q = "select TOP "+ NoofProductstoShow + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status from Product " +
                " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                " where Product_Name Like '%" +searchString+ "%' ";

            con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View("ComponentSearchView");
            }
            else
            {
                return View(dt);
            }
            

        }
        //Gives the result of clicking a company from the left side bar, shows medicines that falls under that company
        public ActionResult FindMedicineByCompanyName(string Company)
        {

           
            DataTable dt = new DataTable();
            

            SqlConnection con = new SqlConnection(constring);
            String q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
               " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
               "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
               " where   Product.Product_ID IN(SELECT Product_ID from Company where Company LIKE '%" + Company + "%')";
          
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }

       
        //Searches a medicine by component when the customer cant find it by its name
        [HttpGet]
        public ActionResult SearchByComponent(string SearchString)
        {

            
            DataTable dt = new DataTable();
           
            SqlConnection con = new SqlConnection(constring);
            String q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
            " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
            "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
            " where   Product.Product_ID IN(SELECT Product_ID from Company where Product_Component LIKE '%" + SearchString + "%')";
          
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }

        
        //Sorts the medicines according to their
        //names(A-z) (z-a)
        //Price(high to low) (low to high)
        //New in

        [HttpGet]
        public ActionResult SortBy(string searchString, string sort)
        {
           
            DataTable dt = new DataTable();
           

            SqlConnection con = new SqlConnection(constring);
            String q;
            if (sort == "NameAsc")
            {
                q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product_Name Like '%" + searchString + "%' Order BY Product_Name";
                
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            else if (sort == "NameDesc")
            {
                q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                   " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                   "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                   " where Product_Name Like '%" + searchString + "%' Order BY Product_Name desc";
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }

            else if (sort == "PriceDesc")
            {
                q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                  " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                  "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                  " where Product_Name Like '%" + searchString + "%' Order BY Product_Price desc";
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            else if (sort == "PriceAsc")
            {
               
                q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product_Name Like '%" + searchString + "%' Order BY Product_Price";
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }

            else if (sort == "Newest")
            {
                q = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product_Name Like '%" + searchString + "%' Order BY Product.Product_ID desc";
                con.Open();
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }

            return View(dt);

        }


        //Gets the number of product to show from the left select bar
        [HttpGet]
        public ActionResult No_Products_toshow(string searchString, string product_no_show)
        {
            int NoofProductstoShow;
            
            if (product_no_show == null)
            {
                NoofProductstoShow = 6;
            }
            else
            {


                NoofProductstoShow = Convert.ToInt32(product_no_show);
                
            }
            DataTable dt = new DataTable();
        

            SqlConnection con = new SqlConnection(constring);
            String q = "select TOP " + NoofProductstoShow + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product_Name Like '%" + searchString + "%' Order BY Product_Price";
          
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }

        //Supplies a medicine with less side effects
        [HttpGet]
        public ActionResult BetterMedicineSuggestion(int MainMedicineID,string MainMedicineComponent)
        {

            
           
            DataTable dt = new DataTable();
            

            SqlConnection con = new SqlConnection(constring);
            String q2 = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where  Product_Component LIKE '" + MainMedicineComponent + "' AND Product.Product_ID IN(Select Product_ID From SideEffects Where SideEffect_Percentage < (Select DISTINCT SideEffect_Percentage from SideEffects where Product_ID=" + MainMedicineID + " ))";
          
            con.Open();
            SqlCommand cmd = new SqlCommand(q2, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }


        //Supplies a medicine with less price
        [HttpGet]
        public ActionResult CheaperMedicineSuggestion(int MainMedicineID, string MainMedicineComponent)
        {



            DataTable dt = new DataTable();
       

            SqlConnection con = new SqlConnection(constring);
            String q2 = "select TOP " + 6 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
               " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
               "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
               " where Product_Component LIKE '" + MainMedicineComponent + "' AND Product_Price < (Select DISTINCT Product_Price from product where Product.Product_ID=" + MainMedicineID + " )";
           
            con.Open();
            SqlCommand cmd = new SqlCommand(q2, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }


        //Filters Regular and non regular med
        [HttpGet]

        public ActionResult FilterRegNonReg(int id)
        {
            productModel ob = new productModel();
           
            DataTable dt = new DataTable();
            ;
            String q;
            SqlConnection con = new SqlConnection(constring);
            if (id==1)
            {

                q = "select TOP " + 10 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product.Product_ID IN (SELECT Product_ID from ProductCategory Where Product_Category Like 'Regular')";
                con.Open();

                SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            else if (id == 2)
            {

                q = "select TOP " + 10 + " Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status  from Product " +
                 " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                 "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID" +
                 " where Product.Product_ID IN (SELECT Product_ID from ProductCategory Where Product_Category Like 'Non-Regular')";
                con.Open();

                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
      




            return View(dt);

        }

        //Creates a side bar with all the distinct companies
        public ActionResult GetAllCompanies()
        {

            SqlConnection con = new SqlConnection(constring);
            string q = "select DISTINCT Company from company";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return PartialView(dt);


        }

        //Opens up the productTables containing all the product info
        [HttpGet]

        public ActionResult ProductTables(string searchString, string product_no_show)
        {
            int NoofProductstoShow = Convert.ToInt32(product_no_show);
          
            NoofProductstoShow = 100;
            DataTable dt = new DataTable();
            

            SqlConnection con = new SqlConnection(constring);
            String q = "select TOP " + NoofProductstoShow + " * from Product " +
                " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                " INNER JOIN SideEffects ON SideEffects.Product_ID=Product.Product_ID " +
                "INNER JOIN Company On Company.Product_ID=Product.Product_ID " +
                " where Product.Product_Name Like '%" + searchString + "%'";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                productModel ob = new productModel();
                return View(dt);

            }


            

        }

        //Opens a product creation form with the product model
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Session["Email"] as string))
            {

                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                productModel ob = new productModel();
                return View(ob);

            }


               
        }


        //Obtains the values obtained from the creation form.
        //Inserts into the database
        [HttpPost]
        public ActionResult Create(productModel ob,HttpPostedFileBase file,string Company,int Category,int SideEffectPercentage,string SideEffect,int Status)
        {
            SqlConnection con = new SqlConnection(constring);
            String q1 = "INSERT into Product(Product_Name,Product_Price,Product_Status,Product_ImagePath,Product_Strength,Product_Component,Product_Description) " +
                "values(@name,@price,@status,@imageid,@strength,@component,@desc)";
       
            SqlCommand cmd = new SqlCommand(q1, con);
            con.Open();
            cmd.Parameters.AddWithValue("@name", ob.Product_Name);
            cmd.Parameters.AddWithValue("@price", ob.Product_Price);

            if (Status == 1)
            {
                cmd.Parameters.AddWithValue("@status", "Available");
            }
            else
            {
                cmd.Parameters.AddWithValue("@status", "Out of Stock");

            }
              
            
            cmd.Parameters.AddWithValue("@strength", ob.Product_Strength);         
            cmd.Parameters.AddWithValue("@Component", ob.Product_Component);
            cmd.Parameters.AddWithValue("@desc", ob.Product_Description);

            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/productimages/"), filename);
                file.SaveAs(imgpath);

            }
            cmd.Parameters.AddWithValue("@imageid", "/productimages/" + file.FileName);
            cmd.ExecuteNonQuery();
            string q11 = "SELECT top 1 Product_ID from Product order by Product_ID desc";
            SqlCommand cmd11 = new SqlCommand(q11, con);
            
            int Prod_ID = (int)cmd11.ExecuteScalar();
      
            String q2 = "INSERT into Company(Product_ID,Company) values(@current_prod_id,@company)";
            SqlCommand cmd2 = new SqlCommand(q2, con);

            cmd2.Parameters.AddWithValue("@Current_prod_id", Prod_ID);
            cmd2.Parameters.AddWithValue("@company", Company);
            cmd2.ExecuteNonQuery();
            String q3 = "INSERT into ProductCategory(Product_ID,Product_Category) values(@current_prod_id,@category)";
            SqlCommand cmd3 = new SqlCommand(q3, con);

            if (Category == 1)
            {
                cmd3.Parameters.AddWithValue("@category", "Regular");
            }
            else
            {
                cmd3.Parameters.AddWithValue("@category", "Non-Regular");
            }
            cmd3.Parameters.AddWithValue("@Current_prod_id", Prod_ID);
            
            cmd3.ExecuteNonQuery();

            String q4 = "INSERT into SideEffects(Product_ID,SideEffect,SideEffect_Percentage) values(@current_prod_id,@sideEffect,@sideEffectPercentage)";
            SqlCommand cmd4 = new SqlCommand(q4, con);

            cmd4.Parameters.AddWithValue("@Current_prod_id", Prod_ID);
            cmd4.Parameters.AddWithValue("@sideEffect", SideEffect);
            cmd4.Parameters.AddWithValue("@sideEffectPercentage",SideEffectPercentage);
            cmd4.ExecuteNonQuery();


            return RedirectToAction("ProductTables");
        }


        //Deletes a particular product
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Delete from Product where Product_ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id",id);
         
            cmd.ExecuteNonQuery();

            return RedirectToAction("Altindex");
        }


      

        

       
        //Opens the form to edit product information
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from Product where Product_ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            productModel ob = new productModel();
            if (dt.Rows.Count == 1)
            {
                ob.Product_Name = dt.Rows[0][1].ToString();
                ob.Product_ID = Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.Product_Price = Convert.ToInt32(dt.Rows[0][6].ToString());
                ob.Product_Status = dt.Rows[0][2].ToString();
                ob.Product_ImagePath = dt.Rows[0][7].ToString();
                ob.Product_Component= dt.Rows[0][4].ToString();
                
                ob.Product_Strength = Convert.ToInt32(dt.Rows[0][3].ToString());
                ob.Product_Description= dt.Rows[0][5].ToString();
               



            }


            return View(ob);

           
        }


        //Updates product info in the db
        [HttpPost]
        public ActionResult Edit(productModel ob,int status)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            String q = "Update Product set Product_Price=@price,Product_Status=@status,Product_Component=@component,Product_Description=@desc,Product_Name=@name,Product_Strength=@strength  where Product_ID= @id";

            SqlCommand cmd = new SqlCommand(q, con);
            
            cmd.Parameters.AddWithValue("@id", ob.Product_ID);
            cmd.Parameters.AddWithValue("@name", ob.Product_Name);
            cmd.Parameters.AddWithValue("@price", ob.Product_Price);
            if (status == 1)
            {
                cmd.Parameters.AddWithValue("@status", "Available");
            }
            else
            {
                cmd.Parameters.AddWithValue("@status", "Out of Stock");
            }
            
            cmd.Parameters.AddWithValue("@component", ob.Product_Component);
            cmd.Parameters.AddWithValue("@strength", ob.Product_Strength);
            cmd.Parameters.AddWithValue("@desc", ob.Product_Description);


            cmd.ExecuteNonQuery();
            return RedirectToAction("ProductTables");
        }

        //Opens the form to view and edit product picture
        public ActionResult EditPropic(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "SELECT * from Product where Product_ID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            productModel ob = new productModel();
            if (dt.Rows.Count == 1)
            {
                ob.Product_ID= Convert.ToInt32(dt.Rows[0][0].ToString());
               
               ob.Product_ImagePath = dt.Rows[0][7].ToString();

            }

            ViewData["img"] = ob.Product_ImagePath;
            return View(ob);


        }


        //Updates the picture of the product in the database
        [HttpPost]
        public ActionResult EditPropic(productModel ob, HttpPostedFileBase file)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update Product set Product_ImagePath=@imageid  where Product_ID=@id";



            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", ob.Product_ID);
           if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/productimages/"), filename);
                file.SaveAs(imgpath);

            }
            cmd.Parameters.AddWithValue("@imageid", "/productimages/" + file.FileName);
            ViewData["img"] = "/productimages/" + file.FileName;

           


            cmd.ExecuteNonQuery();
           con.Close();

            return RedirectToAction("ProductTables");
        }



        //Shows the details of the product clicked on

        public ActionResult Details(int id)
        {
            
            SqlConnection con = new SqlConnection(constring);
            
            String q = "select  Product_Name,Product_Price,Product_Strength,Product_Component,Product_ImagePath,Product.Product_ID,SideEffect,SideEffect_Percentage,Product_Category,Product_Status,Product_Description,Company  from Product " +
                " INNER JOIN ProductCategory ON Product.Product_ID=ProductCategory.Product_ID " +
                "INNER JOIN SideEffects ON Product.Product_ID= SideEffects.Product_ID " +
                "INNER JOIN Company On Company.Product_ID=Product.Product_ID where Product.Product_ID=@id";
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