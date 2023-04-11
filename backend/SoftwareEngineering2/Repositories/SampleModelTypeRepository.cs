using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories; 

public class SampleModelTypeRepository : ISampleModelTypeRepository {
    private readonly FlowerShopContext _context;
    
    public SampleModelTypeRepository(FlowerShopContext context) {
        _context = context;
    }
    
    public async Task<IEnumerable<SampleModelType>> GetAllAsync() {
        return await _context.SampleModelTypes.ToListAsync();
    }
    
    public async Task<SampleModelType?> GetByNameAsync(string name) {
        return await _context.SampleModelTypes.FirstOrDefaultAsync(x => x.Name == name);
    }
}