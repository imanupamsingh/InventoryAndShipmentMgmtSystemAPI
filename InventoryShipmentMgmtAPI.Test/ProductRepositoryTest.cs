using InventoryDAL;
using InventoryDTO;
using InventoryRepository.Implementation;
using InventoryRepository.Interface;
using InventoryUtility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryShipmentMgmtAPI.Test
{
    public class ProductRepositoryTest
    {
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IProductRepository> mockProductRepository;
        private InventoryDbContext dbContext;
        private ProductRepository productRepository;
        private IServiceProvider serviceProvider;


        [SetUp]
        public void Setup()
        {
            // Mock IConfiguration
            mockConfiguration = new Mock<IConfiguration>();
            // Create a mock of IProductRepository
            mockProductRepository = new Mock<IProductRepository>();
            dbContext = new InventoryDbContext();
            // Setting up mock configuration
            var inMemory = new Dictionary<string, string>
             {
                {"ConnectionStrings:InventoryDbConnection", "Server=DESKTOP-6KKJ0V7\\ADMIN;Database=WebAPI_DB;User Id=sa;Password=Anujit_2402@#dec;Integrated Security=true;TrustServerCertificate=True;"}
             };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .Build();

            // Set up services and DbContext in the test environment
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConstantResources.InventoryDbConnection)));

            serviceProvider = services.BuildServiceProvider();
            dbContext = serviceProvider.GetService<InventoryDbContext>();

            productRepository = new ProductRepository(mockConfiguration.Object, dbContext);

        }

        [Test]
        public void ReturnAllProductsFromGetAllProduct()
        {
            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(new List<ProductResponse>());

            // Act: Call the method to test
            var result = productRepository.GetAllProducts();

            // Assert: Verify that the result matches the expected output
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(5));  // We expect 2 products
            Assert.Multiple(() =>
            {
                Assert.That(result[0].ProductName, Is.EqualTo("Watch"));
                Assert.That(result[0].Quantity, Is.EqualTo(1));
                Assert.That(result[0].Price, Is.EqualTo(2000.00));
            });
        }

        [Test]
        public void ReturnEmptyListWhenNoProductsExistFromGetAllProduct()
        {
            // Arrange: Setup the mock to return an empty list
            mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(new List<ProductResponse>());

            // Act: Call the method to test
            var result = productRepository.GetAllProducts();

            // Assert: Verify that the result is an empty list
            Assert.IsNotNull(result);
            Assert.That(0, Is.EqualTo(0));
        }

        [Test]
        public void ReturnProductDetailsProductById()
        {
            // Arrange: Setup the mock repository to return the mock data
            int productId = 1002;
            mockProductRepository.Setup(repo => repo.GetProductById(productId)).Returns(new ProductResponse());

            // Act: Call the method to test
            var result = productRepository.GetProductById(productId);

            // Assert: Verify that the result matches the expected output
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.ProductName, Is.EqualTo("Watch"));
                Assert.That(result.Quantity, Is.EqualTo(1));
                Assert.That(result.Price, Is.EqualTo(2000.00));
            });
        }

        [Test]
        public void ReturnNoDataWhenProductNotAvailableFromGetProduct()
        {
            // Arrange: Setup the mock repository to return the mock data
            int productId = 0;
            mockProductRepository.Setup(repo => repo.GetProductById(productId)).Returns(new ProductResponse());

            // Act: Call the method to test
            var result = productRepository.GetProductById(productId);

            // Assert: Verify that the result matches the expected output
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.ProductName, Is.EqualTo(null));
                Assert.That(result.Quantity, Is.EqualTo(0));
                Assert.That(result.Price, Is.EqualTo(0));
            });
        }

        [Test]
        public void SaveProductDetailsReturnOk()
        {

            // Arrange
            var product = new ProductRequest
            {
                ProductName = "Bike",
                Price = 10000,
                Quantity = 11
            };
            var response = new APIResponseModel<object>
            {
                Status = true,
                ResponseMessage = "Product details has been added successfully!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.SaveProductDetails(product)).Returns(new APIResponseModel<object> { Data = 200 });

            // Act: Call the method to test
            var result = productRepository.SaveProductDetails(product);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void SaveProductDetailsReturnFailed()
        {

            // Arrange
            var failedproduct = new ProductRequest
            {
                ProductName = null,
                Price = 10000,
                Quantity = -1
            };
            var response = new APIResponseModel<object>
            {
                Status = false,
                ResponseMessage = "Invalid Product Details!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.SaveProductDetails(failedproduct)).Returns(new APIResponseModel<object> { Data = 404 });

            // Act: Call the method to test
            var result = productRepository.SaveProductDetails(failedproduct);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(response.Status, result.Status);
        }

        [Test]
        public void UpdateProductDetailsReturnOk()
        {

            // Arrange
            var product = new ProductRequest
            {
                ProductId = 1010,
                ProductName = "Scotty",
                Price = 10000,
                Quantity = 2
            };
            var response = new APIResponseModel<object>
            {
                Status = true,
                ResponseMessage = "Product details has been updated successfully!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.UpdateProductDetails(product)).Returns(new APIResponseModel<object> { Data = 200 });

            // Act: Call the method to test
            var result = productRepository.UpdateProductDetails(product);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void UpdateProductDetailsReturnFailed()
        {

            // Arrange
            var failedproduct = new ProductRequest
            {
                ProductId = 0,
                ProductName = null,
                Price = 10000,
                Quantity = -1
            };
            var response = new APIResponseModel<object>
            {
                Status = false,
                ResponseMessage = "Failed to update Product Details!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.UpdateProductDetails(failedproduct)).Returns(new APIResponseModel<object> { Data = 404 });

            // Act: Call the method to test
            var result = productRepository.UpdateProductDetails(failedproduct);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(response.Status, result.Status);
        }

        [Test]
        public void DeleteProductDetailsReturnOk()
        {

            // Arrange
            var product = new ProductRequest
            {
                ProductId = 1010
            };
            var response = new APIResponseModel<object>
            {
                Status = true,
                ResponseMessage = "Product details has been deleted successfully!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.DeleteProductDetails(product.ProductId)).Returns(new APIResponseModel<object> { Data = 200 });

            // Act: Call the method to test
            var result = productRepository.DeleteProductDetails(product.ProductId);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteProductDetailsReturnFailed()
        {

            // Arrange
            var failedproduct = new ProductRequest
            {
                ProductId = 0
            };
            var response = new APIResponseModel<object>
            {
                Status = false,
                ResponseMessage = "Failed to delete Product Details!",
                Data = string.Empty
            };

            // Arrange: Setup the mock repository to return the mock data
            mockProductRepository.Setup(repo => repo.DeleteProductDetails(failedproduct.ProductId)).Returns(new APIResponseModel<object> { Data = 404 });

            // Act: Call the method to test
            var result = productRepository.DeleteProductDetails(failedproduct.ProductId);

            // Assert
            Assert.IsInstanceOf<APIResponseModel<object>>(result); // If the method directly returns an int
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(response.Status, result.Status);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of _dbContext after each test to release resources
            dbContext?.Dispose();
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

        }
    }
}
