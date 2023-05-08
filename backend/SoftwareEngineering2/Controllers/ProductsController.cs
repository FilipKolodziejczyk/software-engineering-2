using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;


namespace SoftwareEngineering2.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) {
            _productService = productService;
        }
        
        // POST: api/products/
        [HttpPost]
        [SwaggerOperation(Summary = "Create new Product")]
        [SwaggerResponse(201, "Created Successfully")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> Add([FromBody] NewProductDTO productModel)
        {
            //check validity
            if (string.IsNullOrWhiteSpace(productModel.Name) || string.IsNullOrWhiteSpace(productModel.Description))
            {
                return BadRequest(new { message = "No name or description provided" });
            }
            //add to db
            var result = await _productService.CreateModelAsync(productModel);
            return CreatedAtAction(nameof(Add), new { id = result.ProductID }, result); //?
        }
        // PUT???: api/products/
        //create multiply products???
        
        
        // GET: api/products/5
        [HttpGet("{id:int}", Name = "GetProduct")]
        [SwaggerOperation(Summary = "Fetch a specific product", Description =  "This endpoint will return the specific product matching productID")]
        [SwaggerResponse(200, "Returns a product", typeof(ProductDTO))]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> Get(int id) {
            var product = await _productService.GetModelByIdAsync(id);
            return product != null ? 
                Ok(product) : 
                NotFound(new { message = $"No product found with id {id}" });
        }
        // GET: api/Sample
        [HttpGet]
        [SwaggerOperation(Summary = "Fetch Products", Description =  "This endpoint will return the list of all products matching provided criteria.")]
        [SwaggerResponse(200, "Returns a list of samples", typeof(ProductDTO[]))]
        [SwaggerResponse(404, "No samples found")]
        public async Task<IActionResult> Get([FromQuery] string? filter, [FromQuery] string? type) {
            filter ??= "";
            type ??= "";

            var samples = await _productService.GetFilteredModelsAsync(filter, type);
            return samples.Any() ? 
                Ok(samples) : 
                NotFound(new { message = $"No samples found with name {filter} and type {type}" });
        }
        
        
        // DELETE: api/products/6
        [HttpDelete("{ProductID:int}")]
        [SwaggerOperation(Summary = "Delete product", Description =  "This endpoint will delete the specific product matching productID")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public async Task<IActionResult> Delete(int id) {
            if (await _productService.GetModelByIdAsync(id) == null)
            {
                return NotFound(new { message = $"No product found with id {id}" }); 
            }
            await _productService.DeleteModelAsync(id);
            return NoContent();
        }
        
        // PUT: api/products
        [HttpPut]
        [SwaggerOperation(Summary = "Modify product", Description =  "This endpoint will modify the specific product")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(201, "Created")]
        public ActionResult Update([FromBody] SampleDTO newModel)
        {
            return Ok("Succesfully updated");
        }
    }
}

//4.3.7 Employee adding new product
//4.3.8 Employee removing a product
//4.3.9 Employee updating a product