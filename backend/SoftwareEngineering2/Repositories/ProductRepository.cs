using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class ProductRepository : IProductRepository {
    private readonly FlowerShopContext _context;

    public ProductRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(ProductModel product) {
        await _context.ProductModels.AddAsync(product);
    }

    public async Task<ProductModel?> GetByIdAsync(int id) {
        return await _context.ProductModels
            .Include(product => product.Images)
            .FirstOrDefaultAsync(product => product.ProductId == id);
    }

    public void Delete(ProductModel product) {
        _context.ProductModels.Remove(product);
    }

    public async Task<IEnumerable<ProductModel>> GetAllFilteredAsync(string searchQuery, string filteredCategory,
        decimal minPrice, decimal maxPrice, int pageNumber, int elementsOnPage) {
        if (maxPrice > (decimal)Math.Pow(10, 17 - 2)) maxPrice = (decimal)Math.Pow(10, 17 - 2);

        return await _context.ProductModels
            .Include(product => product.Images)
            .Where(product => product.Name.Contains(searchQuery))
            .Where(product => product.Category.Contains(filteredCategory))
            .Where(product => product.Price >= minPrice && product.Price <= maxPrice)
            .Skip(elementsOnPage * (pageNumber - 1))
            .Take(elementsOnPage)
            .ToListAsync();
    }
}