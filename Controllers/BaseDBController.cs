using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinalProject.Controllers
{
  public class BaseDBController
  {
    private AWSConfig AWSConfig { get; }
    private NpgsqlConnection conn;

    public BaseDBController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
    }

    public Boolean OpenConnection()
    {
      try
      {
        String connString = getConnectionString();
        conn = new NpgsqlConnection(connString);
        conn.Open();
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public DbDataReader ExecuteQuery(string sql)
    {
      DataSet ds = new DataSet();
      try
      {
        NpgsqlCommand query = new NpgsqlCommand(sql, conn);

        DbDataReader reader = query.ExecuteReader();

        return reader;
      }
      catch (Exception e)
      {
      }
      return null;

    }

    string getConnectionString()
    {
      string hostname = AWSConfig.RDS_Hostname;
      string dbname = AWSConfig.RDS_DB_Name;
      int port = AWSConfig.RDS_Port;
      string username = AWSConfig.RDS_Username;
      string password = AWSConfig.RDS_Password;

      return String.Format("Server={0};Database={1};User Id={2};Password={3};Port={4};", hostname, dbname, username, password, port);
    }

    public void CloseConnection()
    {
      conn.Close();
    }
  }
}
