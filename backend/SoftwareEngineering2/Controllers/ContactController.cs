using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers; 

[Route("api/contact")]
[ApiController]
public class ContactController: ControllerBase
{
    // POST: api/contact
    [HttpPost]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(201, "Created")]
    public ActionResult FillComplaint([FromBody] ComplaintDto newModel)
    {
        return Ok("Complain filled");
    }
        
    // // POST: api/contact
    // [HttpPost]
    // [SwaggerResponse(400, "Bad Request")]
    // [SwaggerResponse(401, "Unauthorised")]
    // [SwaggerResponse(404, "Not found")]
    // [SwaggerResponse(201, "Created")]
    // public ActionResult AnswerComplaint([FromBody] SampleDTO newModel)
    // {
    //     return Ok("Complain answered");
    // }

}

//4.3.5 Client filing a complain
// 4.3.10 Employee sending message to a Client / proposing a solution to a complaint