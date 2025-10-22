using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;


        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Product> AddAsync(ProductCreateDto dto)
        {
            var p = new Product { Name = dto.Name, Price = dto.Price, Stock = dto.Stock };
            return await _repo.AddAsync(p);
        }

        public async Task UpdateAsync(int id, ProductCreateDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) throw new Exception("No encontrado");
            p.Name = dto.Name;
            p.Price = dto.Price;
            p.Stock = dto.Stock;
            await _repo.UpdateAsync(p);
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

    }
}
