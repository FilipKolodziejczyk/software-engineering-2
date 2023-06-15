using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces;

public interface IOrderService {
    Task<OrderDto?> CreateModelAsync(NewOrderDto order, int clientId);
    Task<OrderDto?> GetOrderById(int orderId);
    Task<List<OrderDto>?> GetOrders(int pageNumber, int elementsOnPage);
    Task<List<OrderDto>?> GetOrdersByDeliverymanId(int deliverymanId, int pageNumber, int elementsOnPage);
    Task<OrderDto?> ChangeOrderStatus(int orderId, OrderStatusDto orderStatusDto, int? deliverymanId = null);
    Task DeleteModelAsync(int orderId);
}