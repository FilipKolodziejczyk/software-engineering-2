using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IBasketItemModelRepository {
    Task AddAsync(BasketItemModel itemModel);
    Task<IEnumerable<BasketItemModel>> GetAllModels(int clientId);
    Task<BasketItemModel?> GetByIds(int clientId, int productId);
    void Delete(BasketItemModel model);
    void DeleteMany(IEnumerable<BasketItemModel> models);
}