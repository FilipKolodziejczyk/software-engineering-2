using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IOrderService {
    Task<OrderDTO?> CreateModelAsync(NewOrderDTO order, int clientId);
}