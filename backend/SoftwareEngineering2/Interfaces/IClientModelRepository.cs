using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IClientModelRepository {
    Task AddAsync(ClientModel model);
    Task<ClientModel?> GetByEmail(string emailAddress);
    Task<ClientModel?> GetByID(int id);
    Task UpdateNewsletter(int id, bool subscribed);
}