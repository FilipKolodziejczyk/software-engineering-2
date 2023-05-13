using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IDeliveryManModelRepository {
    Task AddAsync(DeliveryManModel model);
    Task<DeliveryManModel?> GetByEmail(string emailAddress);
    Task<DeliveryManModel?> GetByID(int id);
}