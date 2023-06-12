using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class DeliveryManModelRepository : IDeliveryManModelRepository {
    private readonly FlowerShopContext _context;

    public DeliveryManModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(DeliveryManModel model) {
        await _context.DeliveryManModels.AddAsync(model);
    }

    public async Task<DeliveryManModel?> GetByEmail(string emailAddress) {
        return await _context.DeliveryManModels.FirstOrDefaultAsync(model => model.Email == emailAddress);
    }

    public async Task<DeliveryManModel?> GetByID(int id) {
        return await _context.DeliveryManModels.FirstOrDefaultAsync(model => model.DeliveryManID == id);
    }

    public async Task<IEnumerable<DeliveryManModel>> GetAll() {
        return await _context.DeliveryManModels
            .Include(model => model.Orders)
            .ToListAsync();
    }
}