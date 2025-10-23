using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fullstack.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddAsync_DeberiaCrearUnProductoCorrectamente() { 

            /// Arrange
            var mockRepo = new Mock<IProductRepository>();
            var services = new ProductService(mockRepo.Object);

            var dto = new ProductCreateDto { Name = "Test", Price = 100, Stock = 2 };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync((Product p) => p);


            // Act
             var result = await services.AddAsync(dto);

            // Assert
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Price, result.Price);
            Assert.Equal(dto.Stock, result.Stock);

        }
    }
}
