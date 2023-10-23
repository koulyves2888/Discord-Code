using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapr;
using SlideX.Core.Contracts;
using Dapr.Client;
using SlideX.Core.Constants.Dapr;

namespace Communication.API.Controllers
{
    

     
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmationController : ControllerBase
    {
        private readonly DaprClient _daprclient;

        public ConfirmationController(DaprClient daprclient)
        {
            _daprclient = daprclient;
        }

        [Topic("azpubsub", "UserRegistration")]
        [HttpPost("ConfirmRegistrationEmail")]
        public async Task<ActionResult> SendConfirmationEmail([FromBody] User user)
        {
            //await _daprclient.PublishEventAsync<User>(DaprConfig.PUB_SUB_COMPONENT, "UserProfile", user);

            //_emailSender.SendEmailAsync()
            return Ok();
        }
    }
}
