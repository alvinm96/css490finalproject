using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FinalProject.Controllers;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using FinalProject.Models;

namespace FinalProject.Controllers
{
  [Route("api/[controller]")]
  public class ImagesController : Controller
  {
    [HttpGet]
    public ActionResult GetImages()
    {
      try
      {
        SqlConnectionStringBuilder builder = Builder();

        StringBuilder result = new StringBuilder();
        ArrayList images = new ArrayList();

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
          connection.Open();

          using (SqlCommand command = new SqlCommand("SELECT * FROM Images", connection))
          {
            using (SqlDataReader reader = command.ExecuteReader())
            {

              for (int i = 0; reader.Read(); i++)
              {
                var name = reader[1];
                ImageDB image = new ImageDB(
                  (int) reader[0],
                  (string) reader[1],
                  (string)reader[2],
                  (string)reader[3],
                  (string)reader[4],
                  (byte[])reader[5]
                );

                images.Add(image);
              }
            }
          }
          return new JsonResult(new { results = images });
        }
      }
      catch (Exception e)
      {
        string msg = e.Message;
        return null;
      }
    }

    private SqlConnectionStringBuilder Builder()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
      {
        DataSource = "supersecretserver.database.windows.net",
        UserID = "Rhun4it",
        Password = "/gWwfz5WKAvw",
        InitialCatalog = "SuperSecretDatabase"
      };

      return builder;
    }

    public IActionResult Index()
    {
      return View();
    }
  }
}