using AutoMapper;
using Moq;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Profiles;

namespace SoftwareEngineering2.Services.Tests;

[TestFixture]
public class UserServiceTests {
    private static IMapper _mapper = null!;
    private static IUnitOfWork _unitOfWork = null!;
    private static readonly Mock<IOrderModelRepository> MockRepo = new();
    private static UserService _orderService = null!;

    public UserServiceTests() {
        if (_mapper is null) {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        if (_unitOfWork is null) {
            var mockUnit = new Mock<IUnitOfWork>();
            _unitOfWork = mockUnit.Object;
        }
    }
}