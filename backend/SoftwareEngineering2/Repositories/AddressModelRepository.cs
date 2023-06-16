using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class AddressModelRepository : IAddressModelRepository {
    private readonly FlowerShopContext _context;

    public AddressModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task<AddressModel> AddAsync(AddressModel model) {
        var ret = await _context.AddressModels.AddAsync(model);
        return ret.Entity;
    }

    public async Task<AddressModel?> GetByClient(ClientModel client) {
        return await _context.AddressModels
            .Where(model => model.Client == client)
            .FirstOrDefaultAsync();
    }
}