using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class AddressModelRepository : IAddressModelRepository {
    private readonly FlowerShopContext _context;

    public AddressModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(AddressModel model) {
        await _context.AddressModels.AddAsync(model);
    }
}