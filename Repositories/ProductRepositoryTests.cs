using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fullstack.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }


        [Fact]
        public async Task AddAsync_DeberiaAgregarUnProducto()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);

            var product = new Product
            {
                Name = "Prótesis de titanio",
                Price = 1234.56M,
                Stock = 10
            };

            // Act
            var result = await repo.AddAsync(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Prótesis de titanio", result.Name);
            Assert.Equal(1, context.Products.Count());
        }

        [Fact]
        public async Task GetAllAsync_DeberiaRegresarListaDeProductos()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);

            context.Products.AddRange(
                new Product { Name = "Chip neural", Price = 5000, Stock = 2 },
                new Product { Name = "Ojo biónico", Price = 12000, Stock = 3 }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

    }
}
