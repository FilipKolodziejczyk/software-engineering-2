using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces;

public interface IBasketService {
    Task<BasketItemDto?> AddToBasket(int clientId, int productId, int quantity);
    Task<List<BasketItemDto>> GetForUser(int clientId);
    Task<BasketItemDto?> GetItemByProductId(int clientId, int productId);
    Task DeleteByProductId(int clientId, int productId);
    Task DeleteAll(int clientId);
    Task<BasketItemDto?> Modify(int clientId, int productId, int quantity);
}