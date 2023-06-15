using Moq;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Profiles;
using AutoMapper;

namespace SoftwareEngineering2.Services.Tests; 

[TestFixture()]
public class OrderServiceTests {
    private static IMapper _mapper = null!;
    private static IUnitOfWork _unitOfWork = null!;
    private static readonly Mock<IOrderModelRepository> MockRepo = new();
    private static OrderService _orderService = null!;
    private static IOrderDetailsModelRepository _orderDetailsModelRepository = null!;


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

        MockRepo.Setup(e => e.GetByIdAsync(6)).Returns(Task.FromResult((OrderModel?) new OrderModel {
            OrderId = 6,
            ClientId = 0,
            Client = null,
            DeliveryManId = null,
            DeliveryMan = null,
            AddressId = 0,
            Address = null,
            Status = null
        }));

        if (_orderService is null) {
            var mockRepoObj = MockRepo.Object;
            _orderService = new(_unitOfWork, mockRepoObj, _orderDetailsModelRepository, _mapper);
        }
    }

    [Test()]
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