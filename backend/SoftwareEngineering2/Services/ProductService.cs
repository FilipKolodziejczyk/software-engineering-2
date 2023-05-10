using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Services;

public class ProductService: IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDTO> CreateModelAsync(NewProductDTO newProduct)
    {
        var model = _mapper.Map<ProductModel>(newProduct);
        await _productRepository.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDTO>(model);
    }

    public async Task<ProductDTO> GetModelByIdAsync(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(result);
    }

    public async Task DeleteModelAsync(int id)
    {
            var model = await _productRepository.GetByIdAsync(id) ?? throw new Exception("Product not found");
            _productRepository.Delete(model);
            await _unitOfWork.SaveChangesAsync();
    }


    public async Task<List<ProductDTO>> GetFilteredModelsAsync(string searchQuery, string filteredCategory, int pageNumber, int elementsOnPage )
    {
        var result = await _productRepository.GetAllFilteredAsync(searchQuery, filteredCategory, pageNumber, elementsOnPage);
        return new List<ProductDTO>(result.Select(item => _mapper.Map<ProductDTO>(item)));
    }

    public async Task<ProductDTO> UpdateModelAsync(UpdateProductDTO product)
    {
        var model = await _productRepository.GetByIdAsync(product.ProductID) ?? throw new Exception("Model not found");

        //model = _mapper.Map<ProductModel>(product);
        //to be done differently
        model.Name = product.Name;
        model.ProductID = product.ProductID;
        model.Archived = product.Archived;
        model.Category = product.Category;
        model.Price = product.Price;
        model.Image = product.Image;
        model.Quantity = product.Quantity;
        model.Description = product.Description;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDTO>(model);
    }
}