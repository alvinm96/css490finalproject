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
    public async Task<JsonResult> Register([FromBody] UserIdentity body)
    {
      using (var provider = new Amazon.CognitoIdentityProvider.AmazonCognitoIdentityProviderClient(AWSConfig.AWS_Access_Key_Id, AWSConfig.AWS_Secret_Access_Key, RegionEndpoint.USWest2))
      {
        try
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

          return new JsonResult(new { response = signup.CodeDeliveryDetails.ToString() });
        }
        catch (Exception e)
        {
          return new JsonResult(new { response = e.Message })
          {
            StatusCode = 400
          };
        }
      }
    }
  }

  [Route("api/[controller]")]
  public class LoginController : Controller
  {
    private AmazonCognitoIdentityProviderClient _client;
    public AWSConfig AWSConfig { get; }

    public LoginController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
      _client = new AmazonCognitoIdentityProviderClient(AWSConfig.AWS_Access_Key_Id, AWSConfig.AWS_Secret_Access_Key, RegionEndpoint.USWest2);
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] UserIdentity body)
    {
      try
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

        return new JsonResult(new { response = authResp });    
      }
      catch (Exception e)
      {
        return new JsonResult(new { response = e.Message })
        {
          StatusCode = 400
        };
      }
    }
  }

  [Route("api/[controller]")]
  public class VerifyController : Controller
  {
    private AmazonCognitoIdentityProviderClient _client;
    public AWSConfig AWSConfig { get; }

    public VerifyController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
      _client = new AmazonCognitoIdentityProviderClient(AWSConfig.AWS_Access_Key_Id, AWSConfig.AWS_Secret_Access_Key, RegionEndpoint.USWest2);
    }

    [HttpPost]
    public async Task<JsonResult> Verify([FromBody] UserIdentity body)
    {
      try
      {
        Amazon.CognitoIdentityProvider.Model.ConfirmSignUpRequest confirmReq =
          new ConfirmSignUpRequest()
          {
            Username = body.Username,
            ClientId = AWSConfig.Client_ID,
            ConfirmationCode = body.Code
          };

        var result = await _client.ConfirmSignUpAsync(confirmReq);

        return new JsonResult(new { response = result.ToString() });
      }
      catch (Exception e)
      {
        return new JsonResult(new { response = e.Message })
        {
          StatusCode = 400
        };
      }
    }
  }

  [Route("api/[controller]")]
  public class LogoutController : Controller
  {
    private AmazonCognitoIdentityProviderClient _client;
    public AWSConfig AWSConfig { get; }

    public LogoutController(IOptions<AWSConfig> awsConfig)
    {
      AWSConfig = awsConfig.Value;
      _client = new AmazonCognitoIdentityProviderClient(AWSConfig.AWS_Access_Key_Id, AWSConfig.AWS_Secret_Access_Key, RegionEndpoint.USWest2);
    }

    [HttpPost]
    public async Task<JsonResult> Logout([FromBody] UserIdentity body)
    {
      try
      {
        Amazon.CognitoIdentityProvider.Model.AdminUserGlobalSignOutRequest confirmReq =
          new AdminUserGlobalSignOutRequest()
          {
            UserPoolId = AWSConfig.Userpool_ID,
            Username = body.Username,
          };

        var result = await _client.AdminUserGlobalSignOutAsync(confirmReq);

        return new JsonResult(new { response = result.ToString() });
      }
      catch (Exception e)
      {
        return new JsonResult(new { response = e.Message })
        {
          StatusCode = 400
        };
      }
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
