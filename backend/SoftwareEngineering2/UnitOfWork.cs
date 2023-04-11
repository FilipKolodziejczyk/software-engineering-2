using SoftwareEngineering2.Interfaces;

namespace SoftwareEngineering2; 

public class UnitOfWork : IUnitOfWork {
    private readonly FlowerShopContext _context;
    
    public UnitOfWork(FlowerShopContext context) {
        _context = context;
    }
    
    public async Task SaveChangesAsync() {
        await _context.SaveChangesAsync();
    }
}