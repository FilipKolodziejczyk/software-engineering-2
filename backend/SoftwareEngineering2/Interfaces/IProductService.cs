using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateModelAsync(NewProductDTO newProduct);
    Task<ProductDTO> GetModelByIdAsync(int id);
    Task DeleteModelAsync(int id);
    Task<List<ProductDTO>> GetFilteredModelsAsync(string searchQuery, string filteredCategory, int pageNumber, int elementsOnPage);
    Task<ProductDTO> UpdateModelAsync(UpdateProductDTO product);
}