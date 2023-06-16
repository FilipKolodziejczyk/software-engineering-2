using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class ImageRepository : IImageRepository {
    private readonly FlowerShopContext _context;

    public ImageRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(ImageModel image) {
        await _context.ImageModels.AddAsync(image);
    }

    public async Task<ImageModel?> GetByIdAsync(int id) {
        return await _context.ImageModels
            .Include(image => image.Products)
            .FirstOrDefaultAsync(image => image.ImageId == id);
    }

    public void Delete(ImageModel image) {
        if (!image.Products.IsNullOrEmpty())
            throw new InvalidOperationException("Cannot delete image that is assigned to a product");
        _context.ImageModels.Remove(image);
    }
}