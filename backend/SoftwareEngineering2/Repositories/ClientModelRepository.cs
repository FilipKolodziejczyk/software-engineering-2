using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Repositories;

public class ClientModelRepository : IClientModelRepository {
    private readonly FlowerShopContext _context;

    public ClientModelRepository(FlowerShopContext context) {
        _context = context;
    }

    public async Task AddAsync(ClientModel model) {
        await _context.ClientModels.AddAsync(model);
    }

    public async Task<ClientModel?> GetByEmail(string emailAddress) {
        return await _context.ClientModels.FirstOrDefaultAsync(model => model.Email == emailAddress);
    }

    public async Task<ClientModel?> GetByID(int id) {
        return await _context.ClientModels.FirstOrDefaultAsync(model => model.ClientID == id);
    }

    public async Task UpdateNewsletter(int id, bool subscribed) {
        ClientModel? model = await GetByID(id);
        if (model is not null)
            model.HasNewsletterOn = subscribed;
    }
}