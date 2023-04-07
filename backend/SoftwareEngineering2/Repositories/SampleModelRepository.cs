using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories; 

public class SampleModelRepository : ISampleModelRepository {
    private readonly FlowerShopContext _context;
    
    public SampleModelRepository(FlowerShopContext context) {
        _context = context;
    }
    
    public async Task<IEnumerable<SampleModel>> GetAllAsync() {
        return await _context.SampleModels.ToListAsync();
    }

    public async Task<SampleModel?> GetByIdAsync(int id) {
        return await _context.SampleModels
            .Include(model => model.Type)
            .FirstOrDefaultAsync(sample => sample.Id == id);
    }

    public async Task<SampleModel?> GetByNameAsync(string name) {
        return await _context.SampleModels
            .Where(sample => sample.Name == name)
            .Include(model => model.Type)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(SampleModel sample) {
        await _context.SampleModels.AddAsync(sample);
    }

    public void Delete(SampleModel sample) {
        _context.SampleModels.Remove(sample);
    }
}