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
        private static readonly Mock<IImageRepository> mockImageRepo = new();
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

            mockRepo.Setup(e => e.GetByIdAsync(4)).Returns(Task.FromResult((ProductModel?) new ProductModel
            {
                ProductID = 4, Name = "Rose", Description = "String", Archived = false,
                Category = "flowers", Images = new List<ImageModel>(), Price = 5, Quantity = 10
            }));
            mockRepo.Setup(e => e.GetAllFilteredAsync("daffodil", "flower", 1,32))
                    .Returns(Task.FromResult((IEnumerable<ProductModel>) new [] { new ProductModel
            {
                ProductID = 4, Name = "Rose", Description = "String", Archived = false,
                Category = "flowers", Images = new List<ImageModel>(), Price = 5, Quantity = 10
            }}));
            mockImageRepo.Setup(e => e.GetByIdAsync(1)).Returns(Task.FromResult((ImageModel?) new ImageModel
            {
                ImageId = 1, ImageUri = new Uri("data:image/png;base64,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")
            }));
            
            if (_productService is null)
            {
                var mockRepoObj = mockRepo.Object;
                var mockImageRepoObj = mockImageRepo.Object;
                _productService = new(_unitOfWork, mockRepoObj, mockImageRepoObj, _mapper);
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
                ImageIds = new List<int>(),
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
            var dto = await _productService.GetFilteredModelsAsync("daffodil", "flower", 1,32 );
            Assert.That(dto, Is.EqualTo(new List <ProductDTO> {
                    new ProductDTO
                    {
                        Archived = false,
                        Category = "flowers",
                        Description = "String",
                        ImageIds = new List<int>(),
                        Name = "Rose",
                        Price = 5,
                        Quantity = 10,
                        ProductID = 4
                    }
            }));
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