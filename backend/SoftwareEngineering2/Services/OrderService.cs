using AutoMapper;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;

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
}