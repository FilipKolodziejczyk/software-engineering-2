using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        // GET: api/Sample
        [HttpGet]
        [SwaggerResponse(200, "Returns a list of models", typeof(string[]))]
        [SwaggerResponse(404, "No models found")]
        public ActionResult Get([FromQuery] string filter)
        {
            // data should be kept in repositories, this is only for demo purposes
            var types = new[] { new SampleModelType { Id = 1, Name = "Type 1" }, new SampleModelType { Id = 2, Name = "Type 2" } };
            var data = new[] { new SampleModel { Id = 1, Name = "Value 1", Type = types[0] }, new SampleModel { Id = 2, Name = "Value 2", Type = types[1] } };
            
            var result = data.Where(x => x.Name.Contains(filter)).ToList();

            if (!result.Any())
                return NotFound();
            
            return Ok(result);
        }

        // GET: api/Sample/5
        [HttpGet("{id:int}", Name = "Get")]
        [SwaggerResponse(200, "Returns a model", typeof(string))]
        [SwaggerResponse(404, "Model not found")]
        public ActionResult Get(int id)
        {
            // data should be kept in repositories, this is only for demo purposes
            var types = new[] { new SampleModelType { Id = 1, Name = "Type 1" }, new SampleModelType { Id = 2, Name = "Type 2" } };
            var data = new[] { new SampleModel { Id = 1, Name = "Value 1", Type = types[0] }, new SampleModel { Id = 2, Name = "Value 2", Type = types[1] } };
            
            if (id < 0 || id >= data.Length)
                return NotFound();
            
            return Ok(data[id]);
        }

        // POST: api/Sample
        [HttpPost]
        [SwaggerResponse(201, "Model created", typeof(string))]
        [SwaggerResponse(400, "Model is invalid")]
        [SwaggerResponse(409, "Model already exists")]
        public ActionResult Post([FromBody] SampleDTO newModel)
        {
            // data should be kept in repositories, this is only for demo purposes
            var types = new[] { new SampleModelType { Id = 1, Name = "Type 1" }, new SampleModelType { Id = 2, Name = "Type 2" } };
            var data = new[] { new SampleModel { Id = 1, Name = "Value 1", Type = types[0] }, new SampleModel { Id = 2, Name = "Value 2", Type = types[1] } };
            
            var type = types.FirstOrDefault(x => x.Name == newModel.Type);
            
            if (null == type || string.IsNullOrWhiteSpace(newModel.Name))
                return BadRequest();
            
            var model = new SampleModel { Name = newModel.Name, Type = type };

            if (data.Contains(model))
                return Conflict();

            return CreatedAtAction(nameof(Get), new { id = data.Length + 1 }, newModel);
        }

        // PUT: api/Sample/5
        [HttpPut("{id:int}")]
        [SwaggerResponse(200, "Value updated", typeof(string))]
        [SwaggerResponse(400, "Value is null or empty")]
        [SwaggerResponse(404, "Value not found")]
        public ActionResult Put(int id, [FromBody] SampleDTO updatedModel)
        {
            // data should be kept in repositories, this is only for demo purposes
            var types = new[] { new SampleModelType { Id = 1, Name = "Type 1" }, new SampleModelType { Id = 2, Name = "Type 2" } };
            var data = new[] { new SampleModel { Id = 1, Name = "Value 1", Type = types[0] }, new SampleModel { Id = 2, Name = "Value 2", Type = types[1] } };


            if (id < 0 || id >= data.Length)
                return NoContent();
            
            if (string.IsNullOrWhiteSpace(updatedModel.Name))
                return BadRequest();
            
            var modelToUpdate = data[id];
            var type = types.FirstOrDefault(x => x.Name == updatedModel.Type);
            if (type == null)
                return BadRequest();
            
            modelToUpdate.Name = updatedModel.Name;
            modelToUpdate.Type = type;

            return Ok(updatedModel);
        }

        // DELETE: api/Sample/5
        [HttpDelete("{id:int}")]
        [SwaggerResponse(204, "Model deleted")]
        [SwaggerResponse(404, "Model not found")]
        public ActionResult Delete(int id)
        {
            // data should be kept in repositories, this is only for demo purposes
            var types = new[] { new SampleModelType { Id = 1, Name = "Type 1" }, new SampleModelType { Id = 2, Name = "Type 2" } };
            var data = new[] { new SampleModel { Id = 1, Name = "Value 1", Type = types[0] }, new SampleModel { Id = 2, Name = "Value 2", Type = types[1] } };

            if (id < 0 || id >= data.Length)
                return NotFound();

            return NoContent();
        }
    }
}