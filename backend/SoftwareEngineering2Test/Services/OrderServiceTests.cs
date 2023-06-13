using NUnit.Framework;
using SoftwareEngineering2.Services;
using Moq;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Profiles;
using AutoMapper;
using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Services.Tests {
    [TestFixture()]
    public class OrderServiceTests {
        private static IMapper _mapper = null!;
        private static IUnitOfWork _unitOfWork = null!;
        private static readonly Mock<IOrderModelRepository> mockRepo = new();
        private static OrderService _orderService = null!;
        private static IOrderDetailsModelRepository _orderDetailsModelRepository = null!;


        public OrderServiceTests() {
            if (_mapper is null) {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            if (_unitOfWork is null) {
                var mockUnit = new Mock<IUnitOfWork>();
                _unitOfWork = mockUnit.Object;
            }

            mockRepo.Setup(e => e.GetByIdAsync(6)).Returns(Task.FromResult((OrderModel?) new OrderModel {
                OrderID = 6,
                ClientID = 0,
                Client = null,
                DeliveryManID = null,
                DeliveryMan = null,
                AddressID = 0,
                Address = null,
                Status = null
            }));

            if (_orderService is null) {
                var mockRepoObj = mockRepo.Object;
                _orderService = new(_unitOfWork, mockRepoObj, _orderDetailsModelRepository, _mapper);
            }
        }

        [Test()]
        public async Task GetByIdAsyncTest() {
            var dto = await _orderService.GetOrderById(6);
            Assert.Multiple(() => {
                Assert.That(dto.OrderID, Is.EqualTo(6));
                Assert.That(dto.ClientID, Is.EqualTo(0));
                Assert.That(dto.Address, Is.EqualTo(null));
                Assert.That(dto.Status, Is.EqualTo(null));
                Assert.That(dto.DeliveryMan, Is.EqualTo(null));
                Assert.That(dto.Items, Is.Empty);
            });
        }
    }
}