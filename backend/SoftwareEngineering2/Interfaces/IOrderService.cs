using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IOrderService {
    Task<OrderDTO?> CreateModelAsync(NewOrderDTO order, int clientId);
    Task<OrderDTO?> GetOrderById(int orderId);
    Task<List<OrderDTO>?> GetOrders();
    Task<List<OrderDTO>?> GetOrdersByDeliverymanId(int deliverymanId);
    Task<OrderDTO?> ChangeOrderStatus(int orderId, OrderStatusDTO orderStatusDTO);
    Task DeleteModelAsync(int orderId);
}