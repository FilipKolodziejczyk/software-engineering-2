using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class OrderModelRepository : IOrderModelRepository {
    private readonly FlowerShopContext _context;

    public OrderModelRepository(FlowerShopContext context) {
        _context = context;
    }
    public async Task AddAsync(OrderModel model)
    {
        await _context.OrderModels.AddAsync(model);
    }
}