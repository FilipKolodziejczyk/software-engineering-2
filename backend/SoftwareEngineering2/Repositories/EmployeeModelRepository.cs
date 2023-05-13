using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class EmployeeModelRepository : IEmployeeModelRepository {
    private readonly FlowerShopContext _context;

    public EmployeeModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(EmployeeModel model) {
        await _context.EmployeeModels.AddAsync(model);
    }

    public async Task<EmployeeModel?> GetByEmail(string emailAddress) {
        return await _context.EmployeeModels.FirstOrDefaultAsync(model => model.Email == emailAddress);
    }

    public async Task<EmployeeModel?> GetByID(int id) {
        return await _context.EmployeeModels.FirstOrDefaultAsync(model => model.EmployeeID == id);
    }
}