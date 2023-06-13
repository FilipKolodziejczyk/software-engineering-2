using AutoMapper;
using Microsoft.CodeAnalysis;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;


namespace SoftwareEngineering2.Services;

public class BasketService : IBasketService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketItemModelRepository _basketItemModelRepository;
    private readonly IMapper _mapper;

    public BasketService(
        IUnitOfWork unitOfWork,
        IBasketItemModelRepository basketItemModelRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _basketItemModelRepository = basketItemModelRepository;
        _mapper = mapper;
    }

    public async Task<BasketItemDTO?> AddToBasket(int clientId, int productId, int quantity) {
        BasketItemModel? model = await _basketItemModelRepository.GetByIds(clientId, productId);

        if (model is null) {
            model = _mapper.Map<BasketItemModel>(new BasketItemDTO() { ProductID = productId, Quantity = 0 });
            model.ClientID = clientId;
            await _basketItemModelRepository.AddAsync(model);
        }

        model.Quantity += quantity;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BasketItemDTO>(model);
    }

    public async Task<BasketItemDTO?> Modify(int clientId, int productId, int quantity) {
        BasketItemModel? model = await _basketItemModelRepository.GetByIds(clientId, productId);

        if (model is null)
            return null;

        model.Quantity = quantity;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BasketItemDTO>(model);
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

    public async Task<List<BasketItemDTO>> GetForUser(int clientId) {
        var result = await _basketItemModelRepository.GetAllModels(clientId);
        return new List<BasketItemDTO>(result.Select(_mapper.Map<BasketItemDTO>));
    }

    public async Task<BasketItemDTO?> GetItemByProductId(int clientId, int productId) {
        var model = await _basketItemModelRepository.GetByIds(clientId, productId);
        if (model is null)
            return null;

        return _mapper.Map<BasketItemDTO>(model);
    }
}