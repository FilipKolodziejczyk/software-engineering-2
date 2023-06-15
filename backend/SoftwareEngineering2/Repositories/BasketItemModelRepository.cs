using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class BasketItemModelRepository : IBasketItemModelRepository {
    private readonly FlowerShopContext _context;

    public BasketItemModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(BasketItemModel itemModel) {
        await _context.BasketItemModels.AddAsync(itemModel);
    }

    public void Delete(BasketItemModel model) {
        _context.BasketItemModels.Remove(model);
    }

    public void DeleteMany(IEnumerable<BasketItemModel> models) {
        _context.BasketItemModels.RemoveRange(models);
    }

    public async Task<IEnumerable<BasketItemModel>> GetAllModels(int clientId) {
        return await _context.BasketItemModels
             .Where(model => model.ClientId == clientId)
             .Include(model => model.Client)
             .Include(model => model.Product)
             .ToListAsync();
    }

    public async Task<BasketItemModel?> GetByIds(int clientId, int productId) {
        return await _context.BasketItemModels
             .Where(model => model.ClientId == clientId && model.ProductId == productId)
             .Include(model => model.Client)
             .Include(model => model.Product)
             .FirstOrDefaultAsync();
    }
}