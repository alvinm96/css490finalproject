using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject
{
    public class QueryController : Controller
    {

        public ActionResult Queries()
        {
            string[] result = new string[] { "" };
            ViewBag.Results = result;
            return View();
        }

        public ActionResult GetGroups()
        {
            string[] test = new string[20];
            

            try
            {
                SqlConnectionStringBuilder builder = Builder();

                StringBuilder result = new StringBuilder();


                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT groupName FROM UGroup");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int count = 0;
                            while (reader.Read())
                            {
                                ViewBag.Message = "Test";
                                test[count] = reader.GetString(0);
                                if(count < test.Length)
                                    count++;
                            }

                        }
                    }
                }


            }
            catch
            {

            }
            ViewBag.Results = test;
            return View("Queries");
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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

