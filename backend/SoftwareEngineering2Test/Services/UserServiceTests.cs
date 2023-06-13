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
    public class UserServiceTests {
        private static IMapper _mapper = null!;
        private static IUnitOfWork _unitOfWork = null!;
        private static readonly Mock<IOrderModelRepository> mockRepo = new();
        private static UserService _orderService = null!;

        public UserServiceTests() {
            if (_mapper is null) {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            if (_unitOfWork is null) {
                var mockUnit = new Mock<IUnitOfWork>();
                _unitOfWork = mockUnit.Object;
            }


        }


    }
}