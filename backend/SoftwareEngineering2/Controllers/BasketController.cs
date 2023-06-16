using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Profiles;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers;

[Route("api/basket")]
[ApiController]
public class BasketController : ControllerBase {
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService) {
        _basketService = basketService;
    }

    // POST: api/basket
    [HttpPost]
    [SwaggerOperation(Summary = "Add to basket of current user", Description = "")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(201, "Created")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> AddToBasket([FromBody] BasketItemDto item) {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Forbid();

        try {
            var result = await _basketService.AddToBasket(clientId, item.ProductId, item.Quantity);
            return CreatedAtAction(nameof(AddToBasket), result);
        }
        catch (ArgumentException e) {
            return BadRequest(e.Message);
        }
    }

    // GET: api/basket
    [HttpGet]
    [SwaggerOperation(Summary = "Get basket of current user", Description = "")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(200, "Ok")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> GetBasket() {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Forbid();

        var result = await _basketService.GetForUser(clientId);
        return Ok(result);
    }

    // DELETE: api/basket/6
    [HttpDelete("{productId:int}")]
    [SwaggerOperation(Summary = "Delete product from basket", Description = "")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(204, "Removed")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> DeleteFromBasket(int productId) {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Forbid();

        try {
            await _basketService.DeleteByProductId(clientId, productId);
            return NoContent();
        }
        catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }

    // DELETE: api/basket
    [HttpDelete]
    [SwaggerOperation(Summary = "Delete all items from basket", Description = "")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(204, "Removed")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> DeleteAllFromBasket() {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Forbid();

        await _basketService.DeleteAll(clientId);
        return NoContent();
    }

    // PUT: api/basket/6
    [HttpPut("{productId:int}")]
    [SwaggerOperation(Summary = "Modify product in basket", Description = "")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not found")]
    [SwaggerResponse(204, "Removed")]
    [SwaggerResponse(200, "Modified")]
    [Authorize(Roles = Roles.Client)]
    public async Task<IActionResult> ModifyInBasket(int productId, [FromBody] BasketItemDto updatedItem) {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var clientId))
            return Forbid();

        try {
            var result = await _basketService.Modify(clientId, productId, updatedItem.Quantity);
            return Ok(result);
        }
        catch (ArgumentException e) {
            return NotFound(e.Message);
        }
    }
}