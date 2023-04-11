using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories; 

public class SampleModelRepository : ISampleModelRepository {
    private readonly FlowerShopContext _context;
    
    public SampleModelRepository(FlowerShopContext context) {
        _context = context;
    }
    
    public async Task<IEnumerable<SampleModel>> GetAllFilteredAsync(string? name, string? type, bool exactMatch = false) {
        name ??= "";
        type ??= "";
        
        return await _context.SampleModels
            .Where(sample => exactMatch ? sample.Name == name : sample.Name.Contains(name))
            .Where(sample => exactMatch ? sample.Type.Name == type : sample.Type.Name.Contains(type ?? ""))
            .Include(model => model.Type)
            .ToListAsync();
    }

    public async Task<SampleModel?> GetByIdAsync(int id) {
        return await _context.SampleModels
            .Include(model => model.Type)
            .FirstOrDefaultAsync(sample => sample.Id == id);
    }

    public async Task AddAsync(SampleModel sample) {
        await _context.SampleModels.AddAsync(sample);
    }

    public void Delete(SampleModel sample) {
        _context.SampleModels.Remove(sample);
    }
}