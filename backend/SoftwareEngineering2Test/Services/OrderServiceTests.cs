using AutoMapper;
using Moq;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Profiles;

namespace SoftwareEngineering2.Services.Tests;

[TestFixture]
public class OrderServiceTests {
    private static IMapper _mapper = null!;
    private static IUnitOfWork _unitOfWork = null!;
    private static readonly Mock<IOrderModelRepository> MockRepo = new();
    private static OrderService _orderService = null!;
    private static readonly IOrderDetailsModelRepository _orderDetailsModelRepository = null!;
    private static readonly IAddressModelRepository _addressModelRepository = null!;
    private static readonly IClientModelRepository _clientModelRepository = null!;
    private static readonly IDeliveryManModelRepository _deliveryManModelRepository = null!;
    private static readonly IProductRepository _productRepository = null!;

    public OrderServiceTests() {
        if (_mapper is null) {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        if (_unitOfWork is null) {
            var mockUnit = new Mock<IUnitOfWork>();
            _unitOfWork = mockUnit.Object;
        }

        MockRepo.Setup(e => e.GetByIdAsync(6)).Returns(Task.FromResult((OrderModel?)new OrderModel {
            OrderId = 6,
            Client = null,
            DeliveryMan = null,
            Address = null,
            Status = null
        }));

        if (_orderService is null)
            _orderService = new OrderService(_unitOfWork, MockRepo.Object, _orderDetailsModelRepository,
                _addressModelRepository,
                _clientModelRepository, _deliveryManModelRepository, _productRepository, _mapper);
    }

    [Test]
    public async Task GetByIdAsyncTest() {
        var dto = await _orderService.GetOrderById(6);
        Assert.Multiple(() => {
            Assert.That(dto.OrderId, Is.EqualTo(6));
            Assert.That(dto.ClientId, Is.EqualTo(0));
            Assert.That(dto.Address, Is.EqualTo(null));
            Assert.That(dto.Status, Is.EqualTo(null));
            Assert.That(dto.DeliveryMan, Is.EqualTo(null));
            Assert.That(dto.Items, Is.Empty);
        });
    }
}