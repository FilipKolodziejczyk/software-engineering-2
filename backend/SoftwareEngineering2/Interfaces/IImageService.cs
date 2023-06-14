using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces; 

public interface IImageService {
    Task<ImageDTO> UploadImageAsync(NewImageDTO image);
    
    Task<ImageDTO?> GetImageById(int imageId);
    
    Task DeleteImageAsync(int imageId);
}