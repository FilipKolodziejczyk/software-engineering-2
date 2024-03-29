using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Profiles;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase {
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public OrdersController(IOrderService orderService, IProductService productService, IUserService userService) {
        _orderService = orderService;
        _productService = productService;
        _userService = userService;
    }

    // POST: api/orders
    [HttpPost]
    [SwaggerOperation(Summary = "Create new Order", Description = "Difference: No productId ")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(201, "Created")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> PlaceOrder([FromBody] NewOrderDto order) {
        //check validity
        if (order.Address is null ||
            order.Items is null ||
            string.IsNullOrWhiteSpace(order.Address.City) ||
            string.IsNullOrWhiteSpace(order.Address.Country) ||
            string.IsNullOrWhiteSpace(order.Address.Street) ||
            string.IsNullOrWhiteSpace(order.Address.BuildingNo) ||
            string.IsNullOrWhiteSpace(order.Address.HouseNo) ||
            string.IsNullOrWhiteSpace(order.Address.PostalCode))
            return BadRequest(new { message = "Address is not correct" });

        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Unauthorized();

        foreach (var item in order.Items) {
            var product = await _productService.GetModelByIdAsync(item.ProductId);
            if (product.Quantity < item.Quantity)
                return BadRequest(new { message = "There is not enough item in the shop" });
        }

        //add to db
        var result = await _orderService.CreateModelAsync(order, clientId);
        return CreatedAtAction(nameof(PlaceOrder), result);
    }

    // PUT: api/orders
    [HttpPut]
    [SwaggerOperation(Summary = "Modify order", Description = "")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(200, "OK")]
    [Authorize(Roles = Roles.Employee)]
    public ActionResult Modify([FromBody] OrderDto newModel) {
        throw new NotImplementedException();
    }

    // POST: api/orders/6/change_status
    [HttpPut("{orderId:int}/change_status")]
    [SwaggerOperation(Summary = "Change status", Description = "Difference: PUT instead of POST")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(200, "OK")]
    [Authorize(Roles = Roles.Employee + "," + Roles.DeliveryMan)]
    public async Task<IActionResult> ChangeStatus(int orderId, [FromBody] OrderStatusDto orderStatusDto) {
        if (!OrderStatus.IsValid(orderStatusDto.OrderStatus))
            return BadRequest(new { message = "Order status is not correct" });

        var order = await _orderService.GetOrderById(orderId);
        if (order is null)
            return NotFound("Order not found");

        if (User.IsInRole(Roles.DeliveryMan)
            && (!int.TryParse(User.FindFirst("UserID")?.Value, out var deliverymanId)
                || order.DeliveryMan is null
                || order.DeliveryMan.UserId != deliverymanId))
            return Forbid();

        int? assignDeliveryMan = null;
        if (orderStatusDto.OrderStatus == OrderStatus.Accepted) {
            var availableDeliveryMan = await _userService.GetAvailableDeliveryMan();
            if (availableDeliveryMan is null)
                return NotFound();
            assignDeliveryMan = availableDeliveryMan.UserId;
        }

        var result = await _orderService.ChangeOrderStatus(orderId, orderStatusDto, assignDeliveryMan);

        if (result is null)
            return Unauthorized();

        return Ok(result);
    }

    // GET: api/orders/6
    [HttpGet("{orderId:int}")]
    [SwaggerOperation(Summary = "Fetch a specific order", Description = "Difference: -")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(200, "OK")]
    [Authorize(Roles = Roles.Employee + "," + Roles.DeliveryMan)]
    public async Task<IActionResult> GetOrder(int orderId) {
        var order = await _orderService.GetOrderById(orderId);
        if (order is null)
            return NotFound("Order not found");

        if (User.IsInRole(Roles.DeliveryMan)
            && (!int.TryParse(User.FindFirst("UserID")?.Value, out var deliverymanId)
                || order.DeliveryMan is null
                || order.DeliveryMan.UserId != deliverymanId))
            return Forbid();

        return Ok(order);
    }

    // GET: api/orders
    [HttpGet]
    [SwaggerOperation(Summary = "Fetch assigned orders",
        Description = "Difference: Difference: Endpoint does not exist ")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(200, "OK")]
    [Authorize(Roles = Roles.Employee + "," + Roles.DeliveryMan)]
    public async Task<IActionResult> GetAssignedOrders([FromQuery] int? pageNumber, [FromQuery] int? elementsOnPage) {
        List<OrderDto>? result;
        pageNumber ??= 1;
        elementsOnPage ??= 32;

        if (User.IsInRole(Roles.Employee)) {
            result = await _orderService.GetOrders(pageNumber.Value, elementsOnPage.Value);
        }
        else if (User.IsInRole(Roles.DeliveryMan)) {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out var deliverymanId))
                return Forbid();

            result = await _orderService.GetOrdersByDeliverymanId(deliverymanId, pageNumber.Value,
                elementsOnPage.Value);
        }
        else {
            return Unauthorized();
        }

        return Ok(result);
    }

    // DELETE: api/orders/6
    [HttpDelete("{orderId:int}")]
    [SwaggerOperation(Summary = "Delete order", Description = "Difference: -")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(204, "Removed")]
    [Authorize(Roles = Roles.Employee)]
    public async Task<IActionResult> DeleteOrder(int orderId) {
        var order = await _orderService.GetOrderById(orderId);
        if (order is null)
            return NotFound("Order not found");

        await _orderService.DeleteModelAsync(orderId);
        return NoContent();
    }
}