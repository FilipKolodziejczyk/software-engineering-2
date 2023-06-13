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
    public class UserServiceTests
    {
        private static IMapper _mapper = null!;
        private static IUnitOfWork _unitOfWork = null!;
        private static readonly Mock<IClientModelRepository> mockRepoClient = new();
        private static readonly Mock<IEmployeeModelRepository> mockRepoEmployee = new();
        private static readonly Mock<IDeliveryManModelRepository> mockRepoDeliveryman = new();
        private static UserService _userService = null!;

        public UserServiceTests()
        {
            if (_mapper is null)
            {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            if (_unitOfWork is null)
            {
                var mockUnit = new Mock<IUnitOfWork>();
                _unitOfWork = mockUnit.Object;
            }

            mockRepoClient.Setup(e => e.GetByID(1)).Returns(Task.FromResult((ClientModel?)new ClientModel
            {
                ClientID = 1,
                Name = "David Gilmour",
                Email = "david.gilmour@gmail.com",
                AddressID = 0,
                Address = null,
                HasNewsletterOn = false,
                Password = "Another brick in the wall 123"
            }));
            mockRepoEmployee.Setup(e => e.GetByID(2)).Returns(Task.FromResult((EmployeeModel?)new EmployeeModel
            {
                EmployeeID = 2,
                Name = "Nick Mason",
                Email = "nick.mason@gmail.com",
                Password = null
            }));
            mockRepoDeliveryman.Setup(e => e.GetByID(3)).Returns(Task.FromResult((DeliveryManModel?)new DeliveryManModel
            {
                DeliveryManID = 3,
                Name = "Roger Waters",
                Email = "roger.waters@gmail.com",
                Password = null
            }));

            mockRepoClient.Setup(e => e.GetByEmail("david.gilmour@gmail.com")).Returns(Task.FromResult(
                (ClientModel?)new ClientModel
                {
                    ClientID = 1,
                    Name = "David Gilmour",
                    Email = "david.gilmour@gmail.com",
                    AddressID = 0,
                    Address = null,
                    HasNewsletterOn = false,
                    Password = null
                }));

            mockRepoEmployee.Setup(e => e.GetByEmail("nick.mason@gmail.com")).Returns(Task.FromResult(
                (EmployeeModel?)new EmployeeModel
                {
                    EmployeeID = 2,
                    Name = "Nick Mason",
                    Email = "nick.mason@gmail.com",
                    Password = null
                }));

            mockRepoDeliveryman.Setup(e => e.GetAll()).Returns((Task.FromResult((IEnumerable<DeliveryManModel>)new[]
            {
                new DeliveryManModel
                {
                    DeliveryManID = 3,
                    Name = "Roger Waters",
                    Email = "roger.waters@gmail.com",
                    Password = null
                }
            })));


            if (_userService is null)
            {
                var mockRepoClientObj = mockRepoClient.Object;
                var mockRepoEmployeeObj = mockRepoEmployee.Object;
                var mockRepoDeliveryManObj = mockRepoDeliveryman.Object;
                _userService = new UserService(_unitOfWork, mockRepoClientObj, mockRepoEmployeeObj,
                    mockRepoDeliveryManObj,
                    _mapper);
            }
        }

        [Test()]
        public async Task GetUserByIDTest()
        {
            var dto = await _userService.GetUserByID(Roles.Client, 1);
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(1));
                Assert.That(dto.Name, Is.EqualTo("David Gilmour"));
                Assert.That(dto.Email, Is.EqualTo("david.gilmour@gmail.com"));
            });

            dto = await _userService.GetUserByID(Roles.Employee, 2);
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(2));
                Assert.That(dto.Name, Is.EqualTo("Nick Mason"));
                Assert.That(dto.Email, Is.EqualTo("nick.mason@gmail.com"));
            });

            dto = await _userService.GetUserByID(Roles.DeliveryMan, 3);
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(3));
                Assert.That(dto.Name, Is.EqualTo("Roger Waters"));
                Assert.That(dto.Email, Is.EqualTo("roger.waters@gmail.com"));
            });

            dto = await _userService.GetUserByID(Roles.DeliveryMan, 1);
            Assert.That(dto, Is.EqualTo(null));

            dto = await _userService.GetUserByID(Roles.DeliveryMan, 5);
            Assert.That(dto, Is.EqualTo(null));

            dto = await _userService.GetUserByID(Roles.Client, 5);
            Assert.That(dto, Is.EqualTo(null));

            dto = await _userService.GetUserByID(Roles.Employee, 1);
            Assert.That(dto, Is.EqualTo(null));
        }

        [Test()]
        public async Task GetUserByEmailTest()
        {
            var dto = await _userService.GetUserByEmail("david.gilmour@gmail.com");
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(1));
                Assert.That(dto.Name, Is.EqualTo("David Gilmour"));
                Assert.That(dto.Email, Is.EqualTo("david.gilmour@gmail.com"));
            });

            dto = await _userService.GetUserByEmail("nick.mason@gmail.com");
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(2));
                Assert.That(dto.Name, Is.EqualTo("Nick Mason"));
                Assert.That(dto.Email, Is.EqualTo("nick.mason@gmail.com"));
            });

            dto = await _userService.GetUserByEmail("nickmason@gmail.com");
            Assert.That(dto, Is.EqualTo(null));

            dto = await _userService.GetUserByEmail("");
            Assert.That(dto, Is.EqualTo(null));
        }

        // [Test()]
        // public async Task GetAvailableDeliveryMan()
        // {
        //     var dto = await _userService.GetAvailableDeliveryMan();
        //     Assert.Multiple(() =>
        //     {
        //         Assert.That(dto.UserID, Is.EqualTo(3));
        //         Assert.That(dto.Name, Is.EqualTo("Roger Waters"));
        //         Assert.That(dto.Email, Is.EqualTo("roger.waters@gmail.com"));
        //     });
        //     
        // }
        
        [Test()]
        public async Task GetAvailableDeliveryMan()
        {
            var dto = await _userService.GetAvailableDeliveryMan();
            Assert.Multiple(() =>
            {
                Assert.That(dto.UserID, Is.EqualTo(3));
                Assert.That(dto.Name, Is.EqualTo("Roger Waters"));
                Assert.That(dto.Email, Is.EqualTo("roger.waters@gmail.com"));
            });
            
        }
    }
}