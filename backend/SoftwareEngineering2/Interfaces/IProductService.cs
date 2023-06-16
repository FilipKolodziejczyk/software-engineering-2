using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateModelAsync(NewProductDto newProduct);
    Task<ProductDto?> GetModelByIdAsync(int id);
    Task DeleteModelAsync(int id);
    Task<List<ProductDto>> GetFilteredModelsAsync(string searchQuery, string filteredCategory, decimal minPrice, decimal maxPrice, int pageNumber, int elementsOnPage);
    Task<ProductDto> UpdateModelAsync(UpdateProductDto product);
}