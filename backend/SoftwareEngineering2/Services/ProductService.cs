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
}