using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Repositories;

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

    public async Task<OrderDTO?> ChangeOrderStatus(int orderId, OrderStatusDTO orderStatusDTO) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        if (model is null)
            return null;

        model.Status = orderStatusDTO.OrderStatus;

        model.DeliveryManID ??= 1; // TODO: Choosing delivery man should happen here

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(model);
    }

    public async Task<OrderDTO?> CreateModelAsync(NewOrderDTO order, int clientId) {
        var model = _mapper.Map<OrderModel>(order);
        model.ClientID = clientId;
        await _orderModelRepository.AddAsync(model);

        foreach (var item in order.Items!) {
            var itemModel = _mapper.Map<OrderDetailsModel>(item);
            itemModel.OrderID = model.OrderID;
            await _orderDetailsModelRepository.AddAsync(itemModel);
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(model);
    }

    public async Task DeleteModelAsync(int orderId) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        _orderModelRepository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<OrderDTO?> GetOrderById(int orderId) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        if (model is null)
            return null;

        return _mapper.Map<OrderDTO>(model);
    }

    public async Task<List<OrderDTO>?> GetOrders() {
        var result = await _orderModelRepository.GetAllModelsAsync();
        return new List<OrderDTO>(result.Select(_mapper.Map<OrderDTO>));
    }

    public async Task<List<OrderDTO>?> GetOrdersByDeliverymanId(int deliverymanId) {
        var result = await _orderModelRepository.GetByDeliverymanIdAsync(deliverymanId);
        return new List<OrderDTO>(result.Select(_mapper.Map<OrderDTO>));
    }
}