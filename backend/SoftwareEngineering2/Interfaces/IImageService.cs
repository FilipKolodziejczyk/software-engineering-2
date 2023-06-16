using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces;

public interface IImageService {
    Task<ImageDto> UploadImageAsync(NewImageDto image);

    Task<ImageDto?> GetImageByIdAsync(int imageId);

    Task DeleteImageAsync(int imageId);
}