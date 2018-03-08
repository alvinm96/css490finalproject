using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FinalProject.Controllers
{
  [Route("api/[controller]")]
  public class RegisterController : Controller
  {
    public AWSConfig AWSConfig { get; }

    public RegisterController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task Register([FromBody] UserIdentity test)
    {
      using (var provider = new Amazon.CognitoIdentityProvider.AmazonCognitoIdentityProviderClient(RegionEndpoint.USWest2))
      {
        var signup = await provider.SignUpAsync(new SignUpRequest
        {
          ClientId = AWSConfig.Client_ID,
          Username = test.Username,
          Password = test.Password,
          UserAttributes = new List<Amazon.CognitoIdentityProvider.Model.AttributeType>
          {
            new AttributeType
            {
              Name = "email",
              Value = test.Email
            }
          }
        });
      }
    }
  }

  public class UserIdentity
  {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
