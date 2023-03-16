using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEnginnering2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        // GET: api/Sample
        [HttpGet]
        [SwaggerResponse(200, "Returns a list of values", typeof(string[]))]
        [SwaggerResponse(404, "No values found")]
        public ActionResult Get() {
            // data should be kept in repositories, this is only for demo purposes
            var data = new[] { "value1", "value2" };

            if (!data.Any())
                return NotFound();
            
            return Ok(data);
        }

        // GET: api/Sample/5
        [HttpGet("{id}", Name = "Get")]
        [SwaggerResponse(200, "Returns a value", typeof(string))]
        [SwaggerResponse(404, "Value not found")]
        public ActionResult Get(int id)
        {
            // data should be kept in repositories, this is only for demo purposes
            var data = new[] { "value1", "value2" };  
            
            if (id < 0 || id >= data.Length)
                return NotFound();
            
            return Ok(data[id]);
        }

        // POST: api/Sample
        [HttpPost]
        [SwaggerResponse(201, "Value created", typeof(string))]
        [SwaggerResponse(400, "Value is null or empty")]
        [SwaggerResponse(409, "Value already exists")]
        public ActionResult Post([FromBody] string value)
        {
            // data should be kept in repositories, this is only for demo purposes
            var data = Array.Empty<string>();  
            
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest();

            if (data.Contains(value))
                return Conflict();

            return CreatedAtAction(nameof(Get), new { id = 0 }, value);
        }

        // PUT: api/Sample/5
        [HttpPut("{id}")]
        [SwaggerResponse(200, "Value updated", typeof(string))]
        [SwaggerResponse(400, "Value is null or empty")]
        [SwaggerResponse(404, "Value not found")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            // data should be kept in repositories, this is only for demo purposes
            var data = new[] { "value1", "value2" };

            if (id < 0 || id >= data.Length)
                return NoContent();
            
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest();
            
            return Ok(value);
        }

        // DELETE: api/Sample/5
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Value deleted")]
        [SwaggerResponse(404, "Value not found")]
        public ActionResult Delete(int id)
        {
            // data should be kept in repositories, this is only for demo purposes
            var data = new[] { "value1", "value2" };

            if (id < 0 || id >= data.Length)
                return NotFound();

            return NoContent();
        }
    }
}
