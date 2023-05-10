using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;
using SoftwareEngineering2.Interfaces;

namespace SoftwareEngineering2.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) {
            _orderService = orderService;
        }
        // POST: api/orders
        [HttpPost]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(201, "Created")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> PlaceOrder([FromBody] NewOrderDTO order)
        {
            //check validity
            if (string.IsNullOrWhiteSpace(order.Address.City) || 
                string.IsNullOrWhiteSpace(order.Address.Country) ||
                string.IsNullOrWhiteSpace(order.Address.Street) ||
                string.IsNullOrWhiteSpace(order.Address.BuildingNo) ||
                string.IsNullOrWhiteSpace(order.Address.HouseNo) ||
                string.IsNullOrWhiteSpace(order.Address.PostalCode))
            {
                return BadRequest(new { message = "Address is not correct" });
            }
            //add to db
            var result = await _orderService.CreateModelAsync(order);
            return CreatedAtAction(nameof(PlaceOrder), result);
        }
        
        // PUT: api/orders
        [HttpPut]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        [Authorize(Roles = "employee")]
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
        [Authorize(Roles = "employee,deliveryman")]
        public ActionResult ChangeStatus([FromBody] OrderDTO newModel)
        {
            return Ok("Order status succesfully changed");
        }

        
        // GET: api/orders
        [HttpGet]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        [Authorize(Roles = "employee,deliveryman")]
        public ActionResult GetAssignedOrders([FromBody] OrderDTO newModel)
        {
            return Ok("Succesfully get assigned order");
        }

    }
}
