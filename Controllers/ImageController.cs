using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace FinalProject.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    [HttpPost]
    public ActionResult PostImage([FromBody] Image body)
    {
            string iUrl = "";
            //Upload the file to Azure blob Storage
            CloudStorageAccount storageAccount;
            CloudBlobClient blobClient;
            CloudBlobContainer container;
            try
            {
                storageAccount = CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=cs4b035210e496ex4e62xa99;" +
                    "AccountKey=TfHQMBpzVdWaOY/4YpkOj1C1fkXqZbnBbvEwdrzx+H3x9uR7jF4DGtwGwaGG4HK4SYop03yyIpt15a2+sf9b6Q==;EndpointSuffix=core.windows.net");
                blobClient = storageAccount.CreateCloudBlobClient();
                container = blobClient.GetContainerReference("zdiwus6dmtjglkjgqkdh");
                container.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(body.ImageName);
                //byte[] test = new byte[] { 1, 2, 3 };
                using (var stream = new MemoryStream((Byte[])body.ImageObj)) {
                    blockBlob.UploadFromStreamAsync(stream);
                }
                iUrl = blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception e) { }
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
            object imageUrl = body.ImageObj;

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

    [HttpGet]
    public ActionResult GetImages(string imageName = "", string groupName = "", string userName = "")
    {
      try
      {
        SqlConnectionStringBuilder builder = Builder();

        StringBuilder result = new StringBuilder();
        ArrayList images = new ArrayList();

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
          connection.Open();

          string query = "SELECT * FROM Images";

          if (imageName.Length > 0 || groupName.Length > 0 || userName.Length > 0)
          {
            query += String.Format(" WHERE imageName LIKE '%{0}%' OR groupName LIKE '%{1}%' OR userName LIKE '%{2}%'", imageName, groupName, userName);
          }


          using (SqlCommand command = new SqlCommand(query, connection))
          {
            using (SqlDataReader reader = command.ExecuteReader())
            {

              for (int i = 0; reader.Read(); i++)
              {
                var name = reader[1];
                Image image = new Image(
                  (int)reader[0],
                  (string)reader[1],
                  (string)reader[2],
                  (string)reader[3],
                  (string)reader[4],
                  (string)reader[5]
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
  }
}
