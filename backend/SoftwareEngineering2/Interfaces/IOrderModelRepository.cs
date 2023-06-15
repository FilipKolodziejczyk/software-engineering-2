namespace SoftwareEngineering2.Interfaces;

using Models;
public interface IOrderModelRepository {
    Task AddAsync(OrderModel model);
    Task<OrderModel?> GetByIdAsync(int orderId);
    Task<IEnumerable<OrderModel>> GetAllModelsAsync(int pageNumber, int elementsOnPage);
    Task<IEnumerable<OrderModel>> GetByDeliverymanIdAsync(int deliverymanId, int pageNumber, int elementsOnPage);
    void Delete(OrderModel model);
}