using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IBasketService {
    Task<BasketItemDTO?> AddToBasket(int clientId, int productID, int quantity);
    Task<List<BasketItemDTO>> GetForUser(int clientId);
    Task<BasketItemDTO?> GetItemByProductId(int clientId, int productId);
    Task DeleteByProductId(int clientId, int productId);
    Task DeleteAll(int clientId);
    Task<BasketItemDTO?> Modify(int clientId, int productId, int quantity);
}