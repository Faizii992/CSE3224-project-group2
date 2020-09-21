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
    public class PolicyTermsController : Controller
    {
     //   GET: PolicyTerms
       // string constring = @"Data Source=DESKTOP-A33P8HB;Initial Catalog=LIFESERVEBD;Integrated Security=True";

        public ActionResult PolicyIndex()
        {

            dynamic ob = new ExpandoObject();
            ob.policy = getPolicies();
           
            return View(ob);
        }
        private static List<PolicyModel> getPolicies()
        {
            List<PolicyModel> Policy = new List<PolicyModel>();
            string query = "select policy_id,policyHead,policies from Policy";
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
                            Policy.Add(new PolicyModel
                            {

                                policy_id = (int)sdr["policy_id"],
                                policyHead = sdr["policyHead"].ToString(),

                                policies = sdr["policies"].ToString(),
                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            });
                        }
                        con.Close();
                        return Policy;
                    }
                }
            }
        }
        public ActionResult TermsIndex()
        {

            dynamic ob = new ExpandoObject();
           
            ob.terms = getTerms();
            return View(ob);
        }


        public List<termsModel> getTerms()
        {
            List<termsModel> term = new List<termsModel>();
            string query = "select * from termsOfServices";
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
                            term.Add(new termsModel
                            {

                                term_id = (int)sdr["term_id"],
                                terms_head = sdr["terms_head"].ToString(),
                                terms = sdr["terms"].ToString(),

                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            });
                        }
                        con.Close();
                        return term;
                    }
                }
            }
        }

       string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";



        public ActionResult policyTable()
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "select policy_id,policyHead,policies from Policy";
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }



        public ActionResult TermsTable()
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "select * from termsOfServices";
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return View(dt);

        }


        public ActionResult CreateTerms()
        {
            termsModel ob = new termsModel();
            return View(ob);

        }

        [HttpPost]
        public ActionResult CreateTerms(termsModel ob)
        {
            SqlConnection con = new SqlConnection(constring);

            string q = "Insert into  termsOfServices(terms_head,terms)" + "values(@terms_head,@terms)";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            if (ob.terms_head != null) { 
            cmd.Parameters.AddWithValue("@terms_head", ob.terms_head);
            }
            else
            {
                cmd.Parameters.AddWithValue("@terms_head", DBNull.Value);

            }
            if (ob.terms != null)
            {
                cmd.Parameters.AddWithValue("@terms", ob.terms);
            }
            else
            {
                cmd.Parameters.AddWithValue("@terms", DBNull.Value);

            }


            cmd.ExecuteNonQuery();
            return RedirectToAction("TermsTable");

        }

        public ActionResult CreatePolicy()
        {
            PolicyModel ob = new PolicyModel();
            return View(ob);

        }

        [HttpPost]
        public ActionResult CreatePolicy(PolicyModel ob)
        {
            SqlConnection con = new SqlConnection(constring);

            string q = "Insert into  Policy(policyHead,policies)" + "values(@policyHead,@policies)";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            cmd.Parameters.AddWithValue("@policyHead", ob.policyHead);

            cmd.Parameters.AddWithValue("@policies", ob.policies);


            cmd.ExecuteNonQuery();
            return RedirectToAction("PolicyTable");

        }

        public ActionResult DeletePolicy(int Id)
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "Delete from Policy where policy_id=@policy_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@policy_id", Id);

            cmd.ExecuteNonQuery();
            return RedirectToAction("policyTable");

        }
        public ActionResult DeleteTerms(int Id)
        {
            SqlConnection con = new SqlConnection(constring);

            string qt = "Delete from termsOfServices where term_id=@term_id";
            con.Open();
            SqlCommand cmdt = new SqlCommand(qt, con);
            cmdt.Parameters.AddWithValue("@term_id", Id);

            cmdt.ExecuteNonQuery();

            return RedirectToAction("TermsTable");
        }

        public ActionResult EditPolicy(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "SELECT * from Policy where policy_id=@policy_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@policy_id", id);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            PolicyModel ob = new PolicyModel();

            if (dt.Rows.Count == 1)
            {


                ob.policy_id = Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.policyHead = dt.Rows[0][1].ToString();

                ob.policies = dt.Rows[0][2].ToString();



            }

            return View(ob);
        }

        [HttpPost]
        public ActionResult EditPolicy(PolicyModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update Policy  set policyHead=@policyHead, policies=@policies where policy_id=@policy_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@policy_id", ob.policy_id);
            cmd.Parameters.AddWithValue("@policyHead ", ob.policyHead);

            cmd.Parameters.AddWithValue("@policies ", ob.policies);

            cmd.ExecuteNonQuery();



            return RedirectToAction("policyTable");

        }

        public ActionResult EditTerms(int id)
        {
            SqlConnection con = new SqlConnection(constring);

            string q = "SELECT * from termsOfServices where term_id=@term_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@term_id", id);

            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            termsModel ob = new termsModel();
            if (dt.Rows.Count == 1)
            {


                ob.term_id = Convert.ToInt32(dt.Rows[0][0].ToString());
                ob.terms_head = dt.Rows[0][1].ToString();
                ob.terms = dt.Rows[0][2].ToString();



            }
            return View(ob);

        }

        [HttpPost]
        public ActionResult EditTerms(termsModel ob)
        {
            SqlConnection con = new SqlConnection(constring);
            String q = "Update  termsOfServices set terms_heads=@terms_heads,terms=@terms where term_id=@term_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@term_id", ob.term_id);
            cmd.Parameters.AddWithValue("@terms_head ", ob.terms_head);
            cmd.Parameters.AddWithValue("@terms ", ob.terms);

            cmd.ExecuteNonQuery();



            return RedirectToAction("TermsTable");

        }




    }
}