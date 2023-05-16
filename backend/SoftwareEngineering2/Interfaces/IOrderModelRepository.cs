namespace SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
public interface IOrderModelRepository {
    Task AddAsync(OrderModel model);
    Task<OrderModel?> GetByIdAsync(int orderId);
    Task<IEnumerable<OrderModel>> GetAllModelsAsync();
    Task<IEnumerable<OrderModel>> GetByDeliverymanIdAsync(int deliverymanId);
    void Delete(OrderModel? model);
}