using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;


namespace SoftwareEngineering2.Controllers {
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        // POST: api/products/
        [HttpPost]
        [SwaggerOperation(Summary = "Create new Product", Description = "Difference: Does not contain a ProductID")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [Authorize(Roles = Roles.Employee)]
        public async Task<IActionResult> Add([FromBody] NewProductDTO productModel) {
            //check validity
            if (string.IsNullOrWhiteSpace(productModel.Name) || string.IsNullOrWhiteSpace(productModel.Description)) {
                return BadRequest(new { message = "No name or description provided" });
            }
            //add to db
            var result = await _productService.CreateModelAsync(productModel);
            return CreatedAtAction(nameof(Add), new { id = result.ProductID }, result);
        }

        // GET: api/products/5
        [HttpGet("{ProductID:int}", Name = "GetProduct")]
        [SwaggerOperation(Summary = "Fetch a specific product", Description = "This endpoint will return the specific product matching productID")]
        [SwaggerResponse(200, "Returns a product", typeof(ProductDTO))]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> Get(int ProductID) {
            var product = await _productService.GetModelByIdAsync(ProductID);
            return product != null ?
                Ok(product) :
                NotFound(new { message = $"No product found with id {ProductID}" });
        }

        // GET: api/products
        [HttpGet]
        [SwaggerOperation(Summary = "Fetch Products", Description = "This endpoint will return the list of all products matching provided criteria.")]
        [SwaggerResponse(200, "Returns a list of samples", typeof(ProductDTO[]))]
        [SwaggerResponse(404, "No samples found")]
        public async Task<IActionResult> Get([FromQuery] string? searchQuery, [FromQuery] string? filteredCategory, [FromQuery] int? pageNumber, [FromQuery] int? elementsOnPage) {
            searchQuery ??= "";
            filteredCategory ??= "";
            pageNumber ??= 1;
            elementsOnPage ??= 32;

            var samples = await _productService.GetFilteredModelsAsync(searchQuery, filteredCategory, pageNumber.Value, elementsOnPage.Value);
            return Ok(samples);
        }


        // DELETE: api/products/6
        [HttpDelete("{ProductID:int}")]
        [SwaggerOperation(Summary = "Delete product", Description = "This endpoint will delete the specific product matching productID")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        [Authorize(Roles = Roles.Employee)]
        public async Task<IActionResult> Delete(int ProductID) {
            if (await _productService.GetModelByIdAsync(ProductID) == null) {
                return NotFound(new { message = $"No product found with id {ProductID}" });
            }
            await _productService.DeleteModelAsync(ProductID);
            return NoContent();
        }

        // PUT: api/products
        [HttpPut]
        [SwaggerOperation(Summary = "Modify product", Description = "This endpoint will modify the specific product")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(201, "Created")]
        [Authorize(Roles = Roles.Employee)]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO product) {
            if (string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.Category)) {
                return BadRequest(new { message = "Nonempty product name and category are required" });
            }

            if (await _productService.GetModelByIdAsync(product.ProductID) == null) {
                return NotFound(new { message = $"No sample found with id {product.ProductID})" });
            }
            return Ok(await _productService.UpdateModelAsync(product));
        }
    }
}