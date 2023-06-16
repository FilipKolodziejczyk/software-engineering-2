using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;


namespace SoftwareEngineering2.Services;

public class BasketService : IBasketService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketItemModelRepository _basketItemModelRepository;
    private readonly IClientModelRepository _clientModelRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public BasketService(
        IUnitOfWork unitOfWork,
        IBasketItemModelRepository basketItemModelRepository,
        IClientModelRepository clientModelRepository,
        IProductRepository productRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _basketItemModelRepository = basketItemModelRepository;
        _clientModelRepository = clientModelRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<BasketItemDto?> AddToBasket(int clientId, int productId, int quantity) {
        var model = await _basketItemModelRepository.GetByIds(clientId, productId);

        var client = await _clientModelRepository.GetById(clientId);
        
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) {
            throw new ArgumentException("Product does not exist");
        }

        if (model is null) {
            model = _mapper.Map<BasketItemModel>(new BasketItemDto { ProductId = productId, Quantity = 0 });
            model.Client = client;
            await _basketItemModelRepository.AddAsync(model);
        }

        model.Quantity += quantity;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BasketItemDto>(model);
    }

    public async Task<BasketItemDto?> Modify(int clientId, int productId, int quantity) {
        var model = await _basketItemModelRepository.GetByIds(clientId, productId);

        if (model is null)
            return null;

        model.Quantity = quantity;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BasketItemDto>(model);
    }

    public async Task DeleteByProductId(int clientId, int productId) {
        var model = await _basketItemModelRepository.GetByIds(clientId, productId);
        _basketItemModelRepository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAll(int clientId) {
        var models = await _basketItemModelRepository.GetAllModels(clientId);
        _basketItemModelRepository.DeleteMany(models);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<BasketItemDto>> GetForUser(int clientId) {
        var result = await _basketItemModelRepository.GetAllModels(clientId);
        return new List<BasketItemDto>(result.Select(_mapper.Map<BasketItemDto>));
    }

    public async Task<BasketItemDto?> GetItemByProductId(int clientId, int productId) {
        var model = await _basketItemModelRepository.GetByIds(clientId, productId);
        return model is null ? null : _mapper.Map<BasketItemDto>(model);
    }
}