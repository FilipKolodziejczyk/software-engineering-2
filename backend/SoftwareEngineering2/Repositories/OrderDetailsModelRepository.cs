using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class OrderDetailsModelRepository : IOrderDetailsModelRepository {
    private readonly FlowerShopContext _context;

    public OrderDetailsModelRepository(FlowerShopContext context) {
        _context = context;
    }
}