using SoftwareEngineering2.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly FlowerShopContext _context;
    
    public ProductRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(ProductModel product)
    {
        await _context.ProductModels.AddAsync(product);
    }

    public async Task<ProductModel?> GetByIdAsync(int id)
    {
        return await _context.ProductModels
            .FirstOrDefaultAsync(product => product.ProductID == id);
    }
}