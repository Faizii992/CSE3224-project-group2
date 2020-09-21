using productpagepr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace productpagepr.Controllers
{
    public class UserNotificationController : Controller
    {
        string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

      //  [Authorize]
        [HttpGet]

        public ActionResult notifUser()
        {
            dynamic ob = new ExpandoObject();
            ob.Usernotif = getUsernotif();

            return PartialView(ob);
        }
        private static List<ReplyModel> getUsernotif()
        {
            List<ReplyModel> Usernotif = new List<ReplyModel>();
            string query = "select * from Reply ";
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
                            Usernotif.Add(new ReplyModel
                            {
                                UserId = sdr["UserId"].ToString(),
                                Id = (int)sdr["Id"],
                                Msg_Id = (int)sdr["Msg_Id"],
                                //datetim = Convert.ToDateTime(sdr["Datetm"]),
                            }) ;
                        }
                        con.Close();
                        return Usernotif;
                    }
                }
            }
        }
        // GET: UserNotification
        public ActionResult AllUsernotif()
        {
            string constring = @"Data Source=DESKTOP-K860ERO;Initial Catalog=connection;Integrated Security=True";

            SqlConnection con = new SqlConnection(constring);
            string qt = "select Id,Msg_Id,Reply,UserId from Reply order by Id desc";
            con.Open();
            SqlCommand cmdt = new SqlCommand(qt, con);
            DataTable ddt = new DataTable();
            SqlDataAdapter adaptert = new SqlDataAdapter(cmdt);
            adaptert.Fill(ddt);

            return View(ddt);


        }
        public ActionResult Reply(int Id)
        {

            /*
            SqlConnection con = new SqlConnection(constring);
            String q = "select Id,Msg_Id ,Reply,Helpline.Message from Reply where Msg_Id like '" + Msg_Id + "'" + " INNER JOIN Helpline ON Reply.Msg_Id=Helpline.Id";

            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return View(dt);*/
            

                dynamic ob = new ExpandoObject();
                ob.msg= getMsg(Id);
            ob.Reply = getReply(Id);

            return View(ob);
            }

            private static List<HelplineModel> getMsg(int Id)
            {
            List<HelplineModel> mesag = new List<HelplineModel>();
            string query = "select Id,Subject,Message,imgid,Prescription,UserId from Helpline where Id like '" + Id + "'" ;

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
                            mesag.Add(new HelplineModel
                            {
                                Id = (int)(sdr["Id"]),
                                UserId = sdr["UserId"].ToString(),

                                Subject = sdr["Subject"].ToString(),
                                Message = sdr["Message"].ToString(),
                                imgid = sdr["imgid"].ToString(),
                                Prescription = sdr["Prescription"].ToString(),

                            });
                        }
                    }
                    con.Close();
                    return mesag;

                }

                
            }
            }


        private static List<ReplyModel> getReply(int Id)
        {
            List<ReplyModel> rep = new List<ReplyModel>();
            string query = "select Id,Msg_Id,Reply,UserId from Reply where Id like '" + Id + "' ";

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
                            rep.Add(new ReplyModel
                            {
                                //Id = (int)(sdr["Id"]),
                               // Msg_Id =(int)(sdr["Msg_Id"]),
                               //Subject = sdr["Subject"].ToString(),
                                //Message = sdr["Message"].ToString(),
                                //imgid = sdr["imgid"].ToString(),
                                Reply = sdr["Reply"].ToString(),
                                UserId=sdr["UserId"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return rep;

                }


            }
        }

        }

    }
