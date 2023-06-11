using NUnit.Framework;
using SoftwareEngineering2.Services;
using Moq;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Profiles;
using AutoMapper;
using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Services.Tests
{
    [TestFixture()]
    public class ProductServiceTests
    {
        private static IMapper _mapper = null!;
        private static IUnitOfWork _unitOfWork = null!;
        private static readonly Mock<IProductRepository> mockRepo = new();
        private static ProductService _productService = null!;

        public ProductServiceTests()
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

            mockRepo.Setup(e => e.GetByIdAsync(4)).Returns(Task.FromResult((ProductModel?)new ProductModel
            {
                ProductID = 4, Name = "Rose", Description = "String", Archived = false,
                Category = "flowers", Image = "", Price = 5, Quantity = 10
            }));
            
            if (_productService is null)
            {
                var mockRepoObj = mockRepo.Object;
                _productService = new(_unitOfWork, mockRepoObj, _mapper);
            }
        }

        [Test()]
        public async Task GetModelByIdAsyncTest()
        {
            var dto = await _productService.GetModelByIdAsync(4);

            Assert.That(dto, Is.EqualTo(new ProductDTO
            {
                Archived = false,
                Category = "flowers",
                Description = "String",
                Image = "",
                Name = "Rose",
                Price = 5,
                Quantity = 10,
                ProductID = 4
            }));

            Assert.That(await _productService.GetModelByIdAsync(6), Is.Null);
        }
        
        [Test()]
        public async Task GetAllFilteredAsyncTest()
        {
            Assert.Pass();
        }
        [Test()]
        public void CreateModelAsyncTest() {
            Assert.Pass();
        }
        [Test()]
        public void DeleteModelAsyncTest() {
            Assert.Pass();
        }
        
        [Test()]
        public void UpdateModelAsyncTest()
        {
            Assert.Pass();
        }
    }
}