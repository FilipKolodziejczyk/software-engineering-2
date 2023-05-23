using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class OrderModelRepository : IOrderModelRepository {
    private readonly FlowerShopContext _context;

    public OrderModelRepository(FlowerShopContext context) {
        _context = context;
    }
    public async Task AddAsync(OrderModel model) {
        await _context.OrderModels.AddAsync(model);
    }

    public async Task<OrderModel?> GetByIdAsync(int orderId) {
        return await _context.OrderModels
            .Where(model => model.OrderID == orderId)
            .Include(model => model.OrderDetails)
            .Include(model => model.Address)
            .Include(model => model.Client)
            .Include(model => model.DeliveryMan)
            .Include(model => model.Complaints)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderModel>> GetAllModelsAsync() {
        return await _context.OrderModels
            .Include(model => model.OrderDetails)
            .Include(model => model.Address)
            .Include(model => model.Client)
            .Include(model => model.DeliveryMan)
            .Include(model => model.Complaints)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderModel>> GetByDeliverymanIdAsync(int deliverymanId) {
        return await _context.OrderModels
            .Where(model => model.DeliveryManID == deliverymanId)
            .Include(model => model.OrderDetails)
            .Include(model => model.Address)
            .Include(model => model.Client)
            .Include(model => model.DeliveryMan)
            .Include(model => model.Complaints)
            .ToListAsync();
    }

    public void Delete(OrderModel? model) {
        _context.OrderModels.Remove(model);
    }
}