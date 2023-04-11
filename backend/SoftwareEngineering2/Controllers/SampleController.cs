using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService) {
            _sampleService = sampleService;
        }

        // GET: api/Sample
        [HttpGet]
        [SwaggerResponse(200, "Returns a list of samples", typeof(SampleDTO[]))]
        [SwaggerResponse(404, "No samples found")]
        public async Task<IActionResult> Get([FromQuery] string? filter, [FromQuery] string? type) {
            filter ??= "";
            type ??= "";

            var samples = await _sampleService.GetFilteredModelsAsync(filter, type);
            return samples.Any() ? 
                Ok(samples) : 
                NotFound(new { message = $"No samples found with name {filter} and type {type}" });
        }

        // GET: api/Sample/5
        [HttpGet("{id:int}", Name = "Get")]
        [SwaggerResponse(200, "Returns a sample", typeof(SampleDTO))]
        [SwaggerResponse(404, "Sample not found")]
        public async Task<IActionResult> Get(int id) {
            var sample = await _sampleService.GetModelByIdAsync(id);
            return sample != null ? 
                Ok(sample) : 
                NotFound(new { message = $"No sample found with id {id}" });
        }

        // POST: api/Sample
        [HttpPost]
        [SwaggerResponse(201, "Sample created", typeof(SampleDTO))]
        [SwaggerResponse(400, "Sample is invalid")]
        [SwaggerResponse(409, "Sample already exists")]
        public async Task<IActionResult> Post([FromBody] [Required] NewSampleDTO newSample) {
            if (string.IsNullOrWhiteSpace(newSample.Name) || string.IsNullOrWhiteSpace(newSample.Type))
                return BadRequest(new { message = "Nonempty sample and type names are required" });

            if (await _sampleService.GetModelTypeByNameAsync(newSample.Type) == null)
                return BadRequest(new { message = $"No type found with name {newSample.Type}" });

            if (await _sampleService.GetModelByData(newSample.Name, newSample.Type) != null)
                return Conflict(new { message = $"Sample with name {newSample.Name} and type {newSample.Type} already exists" });
            
            var result = await _sampleService.CreateModelAsync(newSample);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT: api/Sample/5
        [HttpPut("{id:int}")]
        [SwaggerResponse(200, "Value updated", typeof(SampleDTO))]
        [SwaggerResponse(400, "Value is null or empty")]
        [SwaggerResponse(404, "Value not found")]
        public async Task<IActionResult> Put(int id, [FromBody] [Required] NewSampleDTO updatedModel) {
            if (string.IsNullOrWhiteSpace(updatedModel.Name) || string.IsNullOrWhiteSpace(updatedModel.Type))
                return BadRequest(new { message = "Nonempty sample and type names are required" });

            if (await _sampleService.GetModelTypeByNameAsync(updatedModel.Type) == null)
                return BadRequest(new { message = $"No type found with name {updatedModel.Type}" });
            
            if (await _sampleService.GetModelByIdAsync(id) == null)
                return NotFound(new { message = $"No sample found with id {id}" });

            if (await _sampleService.GetModelByData(updatedModel.Name, updatedModel.Type) != null)
                return Conflict(new { message = $"Sample with name {updatedModel.Name} and type {updatedModel.Type} already exists" });
            
            return Ok(await _sampleService.UpdateModelAsync(id, updatedModel));
        }

        // DELETE: api/Sample/5
        [HttpDelete("{id:int}")]
        [SwaggerResponse(204, "Model deleted")]
        [SwaggerResponse(404, "Model not found")]
        public async Task<IActionResult> Delete(int id) {
            if (await _sampleService.GetModelByIdAsync(id) == null)
                return NotFound(new { message = $"No sample found with id {id}" });
            
            await _sampleService.DeleteModelAsync(id);
            return NoContent();
        }
    }
}