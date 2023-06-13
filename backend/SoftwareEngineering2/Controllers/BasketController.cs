using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Services;

namespace SoftwareEngineering2.Controllers {
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
        public async Task<IActionResult> AddToBasket([FromBody] BasketItemDTO item) {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int clientId))
                return Forbid();

            var result = await _basketService.AddToBasket(clientId, item.ProductID, item.Quantity);
            return CreatedAtAction(nameof(AddToBasket), result);
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
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int clientId))
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
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int clientId))
                return Forbid();

            var item = await _basketService.GetItemByProductId(clientId, productId);
            if (item is null)
                return NotFound("Product not found in basket");

            await _basketService.DeleteByProductId(clientId, productId);
            return NoContent();
        }

        // DELETE: api/basket
        [HttpDelete()]
        [SwaggerOperation(Summary = "Delete all items from basket", Description = "")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(204, "Removed")]
        [Authorize(Roles = Roles.Client)]
        public async Task<IActionResult> DeleteAllFromBasket() {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int clientId))
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
        public async Task<IActionResult> ModifyInBasket(int productId, [FromBody] BasketItemDTO updatedItem) {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int clientId))
                return Forbid();

            var item = await _basketService.GetItemByProductId(clientId, productId);
            if (item is null)
                return NotFound();

            if (updatedItem.Quantity > 0) {
                var result = await _basketService.Modify(clientId, productId, updatedItem.Quantity);
                return Ok(result);
            }

            await _basketService.DeleteByProductId(clientId, productId);
            return NoContent();
        }
    }
}
