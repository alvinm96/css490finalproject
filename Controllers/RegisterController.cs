using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
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
    public async Task Register([FromBody] UserIdentity body)
    {
      using (var provider = new Amazon.CognitoIdentityProvider.AmazonCognitoIdentityProviderClient(RegionEndpoint.USWest2))
      {
        var signup = await provider.SignUpAsync(new SignUpRequest
        {
          ClientId = AWSConfig.Client_ID,
          Username = body.Username,
          Password = body.Password,
          UserAttributes = new List<Amazon.CognitoIdentityProvider.Model.AttributeType>
          {
            new AttributeType
            {
              Name = "email",
              Value = body.Email
            }
          }
        });
      }
    }
  }

  [Route("api/[controller]")]
  public class LoginController
  {
    private AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient(RegionEndpoint.USWest2);
    public AWSConfig AWSConfig { get; }

    public LoginController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
    }

    [HttpPost]
    public async Task Login([FromBody] UserIdentity body)
    {
      AdminInitiateAuthRequest authReq = new AdminInitiateAuthRequest()
      {
        UserPoolId = AWSConfig.Userpool_ID,
        ClientId = AWSConfig.Client_ID,
        AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
      };

      authReq.AuthParameters.Add("USERNAME", body.Username);
      authReq.AuthParameters.Add("PASSWORD", body.Password);

      AdminInitiateAuthResponse authResp = await _client.AdminInitiateAuthAsync(authReq);
    }
  }

  [Route("api/[controller]")]
  public class VerifyController
  {
    private AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient(RegionEndpoint.USWest2);
    public AWSConfig AWSConfig { get; }

    public VerifyController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
    }

    [HttpPost]
    public async Task Verify([FromBody] UserIdentity body)
    {
      Amazon.CognitoIdentityProvider.Model.ConfirmSignUpRequest confirmReq = 
        new ConfirmSignUpRequest()
      {
        Username = body.Username,
        ClientId = AWSConfig.Client_ID,
        ConfirmationCode = body.Code
      };

      var result = await _client.ConfirmSignUpAsync(confirmReq);
    }
  }

  public class UserIdentity
  {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Code { get; set; }
  }
}
