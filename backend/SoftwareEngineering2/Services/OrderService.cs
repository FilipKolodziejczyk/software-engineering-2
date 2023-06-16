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
    private readonly IAddressModelRepository _addressModelRepository;
    private readonly IClientModelRepository _clientModelRepository;
    private readonly IDeliveryManModelRepository _deliveryManModelRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IUnitOfWork unitOfWork,
        IOrderModelRepository orderModelRepository,
        IOrderDetailsModelRepository orderDetailsModelRepository,
        IAddressModelRepository addressModelRepository,
        IClientModelRepository clientModelRepository,
        IDeliveryManModelRepository deliveryManModelRepository,
        IProductRepository productRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _orderModelRepository = orderModelRepository;
        _orderDetailsModelRepository = orderDetailsModelRepository;
        _addressModelRepository = addressModelRepository;
        _clientModelRepository = clientModelRepository;
        _deliveryManModelRepository = deliveryManModelRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> ChangeOrderStatus(int orderId, OrderStatusDto orderStatusDto, int? deliverymanId = null) {
        var model = await _orderModelRepository.GetByIdAsync(orderId);
        if (model is null)
            return null;

        model.Status = orderStatusDto.OrderStatus;

        if (orderStatusDto.OrderStatus == OrderStatus.Accepted) {
            DeliveryManModel deliveryman;
            if (deliverymanId is null) {
                var result = await _deliveryManModelRepository.GetAll();
                if (!result.Any()) {
                    throw new SystemException("No deliveryman available");
                }
                deliveryman = result.First();
            }
            else {
                var result = await _deliveryManModelRepository.GetById(deliverymanId.Value);
                if (result is null) {
                    throw new ArgumentException("Deliveryman does not exist");
                }
                deliveryman = result;
            }
            
            model.DeliveryMan = deliveryman;
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDto>(model);
    }

    public async Task<OrderDto?> CreateModelAsync(NewOrderDto order, int clientId) {
        var model = _mapper.Map<OrderModel>(order);
        
        foreach (var item in order.Items!) {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product is null) {
                throw new ArgumentException("Product does not exist");
            }
            if (product.Quantity < item.Quantity) {
                throw new ArgumentException("Not enough products in stock");
            }
        }

        var client = await _clientModelRepository.GetById(clientId);
        if (client is null) {
            throw new ArgumentException("Client does not exist");
        }
        model.Client = client;
        
        if (order.Address is null) {
            var address = await _addressModelRepository.GetByClient(client);
            if (address is null) {
                throw new ArgumentException("Pass an address or add one to the client");
            }
        }
        else {
            var address = _mapper.Map<AddressModel>(order.Address);
            model.Address = await _addressModelRepository.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();
        }
        
        await _orderModelRepository.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();

        foreach (var item in order.Items!) {
            var itemModel = _mapper.Map<OrderDetailsModel>(item);
            itemModel.Order = model;
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