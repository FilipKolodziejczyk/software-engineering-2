using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IAddressModelRepository {
    Task<AddressModel> AddAsync(AddressModel model);
    Task<AddressModel?> GetByClient(ClientModel client);
}