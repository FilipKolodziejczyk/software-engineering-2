using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        // POST: api/products
        [HttpPost]
        [SwaggerResponse(400, "Bad Request"]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Add([FromBody] SampleDTO newModel)
        {

            return null;
        }
        
        
        // DELETE: api/products
        [HttpDelete]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(200, "OK")]
        public ActionResult Delete([FromBody] SampleDTO newModel)
        {

            return null;
        }
        
        // PUT: api/products
        [HttpPut]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(201, "Created")]
        public ActionResult Update([FromBody] SampleDTO newModel)
        {

            return null;
        }
        

    }
}

//4.3.7 Employee adding new product
//4.3.8 Employee removing a product
//4.3.9 Employee updating a product