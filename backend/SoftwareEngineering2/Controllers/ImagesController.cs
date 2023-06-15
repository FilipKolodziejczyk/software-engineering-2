using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using Swashbuckle.AspNetCore.Annotations;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Profiles;

namespace SoftwareEngineering2.Controllers; 

[Route("api/images")]
[ApiController]
public class ImagesController : ControllerBase {
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService) {
        _imageService = imageService;
    }

    // POST: api/images
    [HttpPost]
    [SwaggerOperation(Summary = "Upload new image")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(201, nameof(Created))]
    [Authorize(Roles = Roles.Employee)]
    public async Task<ActionResult<ImageDto>> UploadImage([FromForm] NewImageDto image) {
        var result = await _imageService.UploadImageAsync(image);
        return CreatedAtAction(nameof(UploadImage),  new {id = result.ImageId}, result);
    }
        
    // GET: api/images/5
    [HttpGet("{imageId:int}")]
    [SwaggerOperation(Summary = "Fetch a specific image")]
    [SwaggerResponse(200, "Returns an image", typeof(ImageDto))]
    [SwaggerResponse(404, "Image not found")]
    public async Task<IActionResult> GetImageById(int imageId) {
        var image = await _imageService.GetImageByIdAsync(imageId);
        return image != null ?
            Ok(image) :
            NotFound(new { message = $"No image found with id {imageId}" });
    }
        
    // DELETE: api/images/5
    [HttpDelete("{imageId:int}")]
    [SwaggerOperation(Summary = "Delete a specific image")]
    [SwaggerResponse(204, "No Content")]
    [SwaggerResponse(404, "Image not found")]
    [Authorize(Roles = Roles.Employee)]
    public async Task<IActionResult> DeleteImage(int imageId) {
        try {
            await _imageService.DeleteImageAsync(imageId);
        }
        catch (KeyNotFoundException) {
            return NotFound(new { message = $"No image found with id {imageId}" });
        }
            
        return NoContent();
    }
}