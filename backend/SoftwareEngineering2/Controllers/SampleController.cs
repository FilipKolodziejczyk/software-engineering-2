using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISampleModelRepository _sampleModelRepository;
        private readonly ISampleModelTypeRepository _sampleModelTypeRepository;
        private readonly IMapper _mapper;

        public SampleController(
            IUnitOfWork unitOfWork,
            ISampleModelRepository sampleModelRepository,
            ISampleModelTypeRepository sampleModelTypeRepository,
            IMapper mapper) {
            _unitOfWork = unitOfWork;
            _sampleModelRepository = sampleModelRepository;
            _sampleModelTypeRepository = sampleModelTypeRepository;
            _mapper = mapper;
        }

        // GET: api/Sample
        [HttpGet]
        [SwaggerResponse(200, "Returns a list of models", typeof(SampleDTO[]))]
        [SwaggerResponse(404, "No models found")]
        public async Task<IActionResult> Get([FromQuery] string? filter, [FromQuery] string? type) {
            var result = await _sampleModelRepository.GetAllFilteredAsync(filter, type);

            var list = result.ToList();
            if (!list.Any()) {
                return NotFound(new {message = $"No models found with name {filter ?? "any"} and type {type ?? "any"}"});
            }

            return Ok(new List<SampleDTO>(list.Select(item => _mapper.Map<SampleDTO>(item))));
        }

        // GET: api/Sample/5
        [HttpGet("{id:int}", Name = "Get")]
        [SwaggerResponse(200, "Returns a model", typeof(SampleDTO))]
        [SwaggerResponse(404, "Model not found")]
        public async Task<IActionResult> Get(int id) {
            var result = await _sampleModelRepository.GetByIdAsync(id);

            if (result == null) {
                return NotFound(new {message = $"No model found with id {id}"});
            }

            return Ok(_mapper.Map<SampleDTO>(result));
        }

        // POST: api/Sample
        [HttpPost]
        [SwaggerResponse(201, "Model created", typeof(SampleDTO))]
        [SwaggerResponse(400, "Model is invalid")]
        [SwaggerResponse(409, "Model already exists")]
        public async Task<IActionResult> Post([FromBody] [Required] NewSampleDTO newModel) {
            if (string.IsNullOrWhiteSpace(newModel.Name) || string.IsNullOrWhiteSpace(newModel.Type)) {
                return BadRequest(new {message = "Nonempty model and type names are required"});
            }

            var type = await _sampleModelTypeRepository.GetByNameAsync(newModel.Type);
            if (type == null) {
                return BadRequest(new {message = $"No type found with name {newModel.Type}"});
            }
            
            var existing = await _sampleModelRepository.GetAllFilteredAsync(newModel.Name, newModel.Type, true);
            if (existing.Any()) {
                return Conflict(new {message = $"Model with name {newModel.Name} and type {newModel.Type} already exists"});
            }
            
            var model = _mapper.Map<SampleModel>(newModel);
            model.Type = type;

            try {
                await _sampleModelRepository.AddAsync(model);
                await _unitOfWork.SaveChangesAsync();
            } catch (Exception e) {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
            return CreatedAtAction(nameof(Get), new {id = model.Id}, _mapper.Map<SampleDTO>(model));
        }

        // PUT: api/Sample/5
        [HttpPut("{id:int}")]
        [SwaggerResponse(200, "Value updated", typeof(SampleDTO))]
        [SwaggerResponse(400, "Value is null or empty")]
        [SwaggerResponse(404, "Value not found")]
        public async Task<IActionResult> Put(int id, [FromBody] [Required] NewSampleDTO updatedModel) {
            if (string.IsNullOrWhiteSpace(updatedModel.Name) || string.IsNullOrWhiteSpace(updatedModel.Type)) {
                return BadRequest(new {message = "Nonempty model and type names are required"});
            }
            
            var model = await _sampleModelRepository.GetByIdAsync(id);
            if (model == null) {
                return NotFound(new {message = $"No model found with id {id}"});
            }
            
            var type = await _sampleModelTypeRepository.GetByNameAsync(updatedModel.Type);
            if (type == null) {
                return BadRequest(new {message = $"No type found with name {updatedModel.Type}"});
            }
            
            try {
                model.Name = updatedModel.Name;
                model.Type = type;
                await _unitOfWork.SaveChangesAsync();
            } catch (Exception e) {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Ok(_mapper.Map<SampleDTO>(model));
        }

        // DELETE: api/Sample/5
        [HttpDelete("{id:int}")]
        [SwaggerResponse(204, "Model deleted")]
        [SwaggerResponse(404, "Model not found")]
        public async Task<IActionResult> Delete(int id) {
            var model = await _sampleModelRepository.GetByIdAsync(id);
            
            if (model == null) {
                return NotFound(new {message = $"No model found with id {id}"});
            }
            
            try {
                _sampleModelRepository.Delete(model);
                await _unitOfWork.SaveChangesAsync();
            } catch (Exception e) {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Ok();
        }
    }
}