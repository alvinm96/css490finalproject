using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult CreateGroup()
        {
            
            return View();
        }

        public ActionResult CreateGroupResult()
        {
            try
            {
                string groupName = Request.Form["GName"].ToString();
                string groupDisc = Request.Form["GDisc"].ToString();

                SqlConnectionStringBuilder builder = Builder();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "Insert into UGroup (groupID, groupName, Description, numPosts) Values(@groupID, @groupName, @Description, @numPosts);";
                        command.Parameters.AddWithValue("@GroupID", GenerateID());
                        command.Parameters.AddWithValue("@groupname", groupName);
                        command.Parameters.AddWithValue("@Description", groupDisc);
                        command.Parameters.AddWithValue("@numPosts", 0);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            ViewBag.Result = "Successfully created group " + groupName;
                        }
                        catch (SqlException)
                        {
                            ViewBag.Result = "Group Already Exists";
                        }
                        finally
                        {
                            connection.Close();
                        }

                    }
                }


            }
            catch
            {

            }
            return View("CreateGroup");
        }

        public ActionResult JoinGroup()
        {

            return View();
        }

        public ActionResult JoinGroupResult()
        {
            string userName = "Kien Chin";
            string groupName = Request.Form["GName"].ToString();

            SqlConnectionStringBuilder builder = Builder();

            string groupID = "error";
            string userID = "error";


            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Select groupID, userID From UGroup, Users Where groupName = '" + groupName + "' AND U_name = '" + userName + "'");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    bool success = true;
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                groupID = reader.GetString(0);
                                userID = reader.GetString(1);
                            }


                            ViewBag.Result = "GroupID = " + groupID + ", UserID = " + userID;
                            
                        }
                    }
                    catch
                    {
                        success = false;
                    }
                    if (success)
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "Insert into UGroup_Members (GM_groupID, GM_userID) Values(@GM_groupID, @GM_userID);";
                        command.Parameters.AddWithValue("@GM_groupID", groupID);
                        command.Parameters.AddWithValue("@GM_userID", userID);

                        try
                        {
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            ViewBag.Result = "successfully joined group " + groupName + " - " + groupID;
                        }
                        catch (SqlException)
                        {
                            ViewBag.Result = "failed to join group";
                        }
                        
                    }
                    connection.Close();
                }

                
            }


            //ViewBag.Result = "nothing happened";

            return View("JoinGroup");
        }

        private string GenerateID()
        {
            var random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
                sb.Append(random.Next(10).ToString());
            return sb.ToString();
        }

        private SqlConnectionStringBuilder Builder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "supersecretserver.database.windows.net";
            builder.UserID = "Rhun4it";
            builder.Password = "/gWwfz5WKAvw";
            builder.InitialCatalog = "SuperSecretDatabase";

            return builder;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
