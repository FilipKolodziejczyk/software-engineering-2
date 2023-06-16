namespace SoftwareEngineering2.Interfaces;

using Models;

public interface IProductRepository
{
    public Task AddAsync(ProductModel sample);
    Task<ProductModel?> GetByIdAsync(int id);
    
    void Delete(ProductModel product);
    Task<IEnumerable<ProductModel>> GetAllFilteredAsync(string searchQuery, string filteredCategory, decimal minPrice, decimal maxPrice, int pageNumber, int elementsOnPage);
}