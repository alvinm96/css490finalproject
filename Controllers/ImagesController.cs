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
    //BaseDBController dbconn;
    public ImagesController(IOptions<AWSConfig> awsConfig)
    {
      //dbconn = new BaseDBController(awsConfig);
    }

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
                Image image = new Image(
                  (int) reader[0],
                  (string) reader[1],
                  (string) reader[2],
                  (string) reader[3],
                  (string) reader[4],
                  (string) reader[5]
                );

                images.Add(image);
              }
            }
          }
          return new JsonResult(new { results = images });
        }
      }
      catch
      {
        return null;
      }
    }

    [HttpPost]
    public ActionResult PostImage([FromBody] Image body)
    {
      try
      {
        SqlConnectionStringBuilder builder = Builder();

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
          using (SqlCommand command = new SqlCommand())
          {
            string imageName = body.ImageName;
            string groupName = body.GroupName;
            string userName = body.UserName;
            string description = body.Description;
            var imageUrl = body.ImageObj;

           
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "INSERT INTO dbo.Images (imageName, groupName, userName, description, imageUrl) VALUES(@imageName, @groupName, @userName, @description, @imageUrl)";
            command.Parameters.AddWithValue("@imageName", imageName);
            command.Parameters.AddWithValue("@groupName", groupName);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@imageUrl", imageUrl);

            try
            {
              connection.Open();
              int rowsAffected = command.ExecuteNonQuery();

              return new CreatedResult("Images", imageName);
            }
            catch (SqlException e)
            {

              string msg = e.Message;
              return new BadRequestResult();

            }
            finally
            {
              connection.Close();
            }
          }
        }
      }
      catch (Exception e)
      {
        string msg = e.Message;
        return new BadRequestResult();
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