namespace SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

public interface IProductRepository
{
    public Task AddAsync(ProductModel sample);
    Task<ProductModel?> GetByIdAsync(int id);
    
    void Delete(ProductModel product);
    Task<IEnumerable<ProductModel>> GetAllFilteredAsync(string name, string type);
}