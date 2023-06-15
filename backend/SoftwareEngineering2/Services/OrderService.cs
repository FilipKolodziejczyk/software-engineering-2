using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Profiles;

namespace SoftwareEngineering2.Services;

public class OrderService : IOrderService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderModelRepository _orderModelRepository;
    private readonly IOrderDetailsModelRepository _orderDetailsModelRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IUnitOfWork unitOfWork,
        IOrderModelRepository orderModelRepository,
        IOrderDetailsModelRepository orderDetailsModelRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _orderModelRepository = orderModelRepository;
        _orderDetailsModelRepository = orderDetailsModelRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> ChangeOrderStatus(int orderId, OrderStatusDto orderStatusDto, int? deliverymanId = null) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        if (model is null)
            return null;

        model.Status = orderStatusDto.OrderStatus;

        if (orderStatusDto.OrderStatus == OrderStatus.Accepted) {
            model.DeliveryManId = deliverymanId ?? 1;
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDto>(model);
    }

    public async Task<OrderDto?> CreateModelAsync(NewOrderDto order, int clientId) {
        var model = _mapper.Map<OrderModel>(order);
        model.ClientId = clientId;
        await _orderModelRepository.AddAsync(model);

        foreach (var item in order.Items!) {
            var itemModel = _mapper.Map<OrderDetailsModel>(item);
            itemModel.OrderId = model.OrderId;
            await _orderDetailsModelRepository.AddAsync(itemModel);
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDto>(model);
    }

    public async Task DeleteModelAsync(int orderId) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        _orderModelRepository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<OrderDto?> GetOrderById(int orderId) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        return model is null ? null : _mapper.Map<OrderDto>(model);
    }

    public async Task<List<OrderDto>?> GetOrders(int pageNumber, int elementsOnPage) {
        var result = await _orderModelRepository.GetAllModelsAsync(pageNumber, elementsOnPage);
        return new List<OrderDto>(result.Select(_mapper.Map<OrderDto>));
    }

    public async Task<List<OrderDto>?> GetOrdersByDeliverymanId(int deliverymanId, int pageNumber, int elementsOnPage) {
        var result = await _orderModelRepository.GetByDeliverymanIdAsync(deliverymanId, pageNumber, elementsOnPage);
        return new List<OrderDto>(result.Select(_mapper.Map<OrderDto>));
    }
}