using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Services;

public class ProductService : IProductService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IImageRepository imageRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateModelAsync(NewProductDto newProduct) {
        var model = _mapper.Map<ProductModel>(newProduct);
        var imagesList = new List<ImageModel>();
        foreach (var imageId in newProduct.ImageIds) {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image == null) {
                throw new KeyNotFoundException("Image not found");
            }
            imagesList.Add(image);
        }
        model.Images = imagesList;
        await _productRepository.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(model);
    }

    public async Task<ProductDto?> GetModelByIdAsync(int id) {
        var result = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(result);
    }

    public async Task DeleteModelAsync(int id) {
        var model = await _productRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Product not found");
        _productRepository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ProductDto>> GetFilteredModelsAsync(string searchQuery, string filteredCategory, decimal minPrice, decimal maxPrice, int pageNumber, int elementsOnPage) {
        if (minPrice > maxPrice) {
            throw new ArgumentException("Min price cannot be greater than max price");
        }
        
        var result = await _productRepository.GetAllFilteredAsync(searchQuery, filteredCategory, minPrice, maxPrice, pageNumber, elementsOnPage);
        return new List<ProductDto>(result.Select(_mapper.Map<ProductDto>));
    }

    public async Task<ProductDto> UpdateModelAsync(UpdateProductDto product) {
        var model = await _productRepository.GetByIdAsync(product.ProductId) ?? throw new KeyNotFoundException("Model not found");
        var imagesList = new List<ImageModel>();
        foreach (var imageId in product.ImageIds) {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image == null) {
                throw new KeyNotFoundException("Image not found");
            }
            imagesList.Add(image);
        }
        model.Images = imagesList;

        model.Name = product.Name;
        model.ProductId = product.ProductId;
        model.Archived = product.Archived;
        model.Category = product.Category;
        model.Price = product.Price;
        model.Quantity = product.Quantity;
        model.Description = product.Description;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(model);
    }
}