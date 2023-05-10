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
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(201, "Created")]
        public ActionResult PlaceOrder([FromBody] OrderDTO newModel)
        {
            return Ok("Order succesfully placed");
        }
        
        // PUT: api/orders
        [HttpPut]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Modify([FromBody] OrderDTO newModel)
        {
            return Ok("Order succesfully modified");
        }
        
        // POST: api/orders/6
        [HttpPost("{OrderID:int}")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult ChangeStatus([FromBody] OrderDTO newModel)
        {
            return Ok("Order status succesfully changed");
        }

        
        // GET: api/orders
        [HttpGet]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult GetAssignedOrders([FromBody] OrderDTO newModel)
        {
            return Ok("Succesfully get assigned order");
        }

    }
}

// 4.3.3 Client placing an order
// 4.3.4 Client modifying an order
// 4.3.11 Changing Order status
// 4.3.12 Delivery man getting list of Orders
