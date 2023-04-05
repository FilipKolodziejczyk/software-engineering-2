using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
        // POST: api/orders
        [HttpPost]
        [SwaggerResponse(400, "Bad Request"]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(201, "Created")]
        public ActionResult Place([FromBody] SampleDTO newModel)
        {

            return null;
        }
        
        // PUT: api/orders
        [HttpPut]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Modify([FromBody] SampleDTO newModel)
        {
            return null;
        }
        
        
        // POST: api/orders
        [HttpPost]
        [SwaggerResponse(400, "Bad Request"]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult ChangeStatus([FromBody] SampleDTO newModel)
        {

            return null;
        }
        
        // GET: api/orders
        [HttpPost]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult AssignedOrders([FromBody] SampleDTO newModel)
        {

            return null;
        }

    }
}

// 4.3.3 Client placing an order
// 4.3.4 Client modifying an order
// 4.3.11 Changing Order status
// 4.3.12 Delivery man getting list of Orders
