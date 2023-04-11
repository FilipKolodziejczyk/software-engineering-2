using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        // POST: api/users/log_in
        [HttpPost("log_in")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Login([FromBody] LoginDTO loginModel)
        {
            //username and password should be taken from a post request in the future
            if (loginModel.Username == "user" && loginModel.Password == "password")
            {
                return Ok("JWT Token");
            }
            
            //Bad request
            //Not found
            //Unauthorised
            return null;
        }
        
        
        // POST: api/users/newsletter
        [HttpPost("newsletter")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(200, "OK")]
        public ActionResult SubscribeNewsletter([FromBody] NewsletterDTO newsletterModel)
        {
            return Ok("Succesfully changed newsletter status");
        }
    }
}

// 4.3.2 Client/Employee logging in
// 4.3.6 Client subscribing to / resigning from newsletter