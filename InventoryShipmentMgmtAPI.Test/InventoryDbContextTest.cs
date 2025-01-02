using InventoryDAL;
using InventoryUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryShipmentMgmtAPI.Test
{
    public class InventoryDbContextTest
    {
        private IServiceProvider serviceProvider;
        private InventoryDbContext? dbContext;

        // This method is run before each test
        [SetUp]
        public void Setup()
        {
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

        }

        [Test]
        public void GetCorrectConnectionString()
        {
            // Retrieve the DbContext from DI container
            dbContext = serviceProvider.GetService<InventoryDbContext>();

            // Optionally, you can query the database or check connection settings
            var connectionString = dbContext.Database.GetDbConnection().ConnectionString;
            CollectionAssert.AreEqual("Server=DESKTOP-6KKJ0V7\\ADMIN;Database=WebAPI_DB;User Id=sa;Password=Anujit_2402@#dec;Integrated Security=true;TrustServerCertificate=True;", connectionString);
        }

        [Test]
        public void GetDbContextWithCorrectConnectionString()
        {
            // Arrange
            dbContext = serviceProvider.GetService<InventoryDbContext>();

            // Act
            var options = dbContext.Database.GetDbConnection().ConnectionString;

            // Assert
            CollectionAssert.AreEqual("Server=DESKTOP-6KKJ0V7\\ADMIN;Database=WebAPI_DB;User Id=sa;Password=Anujit_2402@#dec;Integrated Security=true;TrustServerCertificate=True;", options);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of _dbContext after each test to release resources
            dbContext?.Dispose();
            //_serviceProvider?.Dispose();
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
