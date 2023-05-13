using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IAddressModelRepository {
    Task AddAsync(AddressModel model);
}