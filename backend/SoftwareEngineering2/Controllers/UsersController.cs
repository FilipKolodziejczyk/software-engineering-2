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

        // POST: api/users
        [HttpPost]
        [SwaggerResponse(400, "Bad Request"]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Login([FromBody] SampleDTO newModel)
        {

            return null;
        }
        
        
        // POST: api/users
        [HttpPost]
        [SwaggerResponse(400, "Bad Request"]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(200, "OK")]
        public ActionResult SubscribeNewsletter([FromBody] SampleDTO newModel)
        {

            return null;
        }
    }
}

// 4.3.2 Client/Employee logging in
// 4.3.6 Client subscribing to / resigning from newsletter