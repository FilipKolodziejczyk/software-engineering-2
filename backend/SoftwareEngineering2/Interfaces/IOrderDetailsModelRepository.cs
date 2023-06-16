using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IOrderDetailsModelRepository {
    Task AddAsync(OrderDetailsModel itemModel);
}