using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IImageRepository {
    public Task AddAsync(ImageModel image);

    Task<ImageModel?> GetByIdAsync(int id);

    void Delete(ImageModel image);
}