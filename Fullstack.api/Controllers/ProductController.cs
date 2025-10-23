using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _service.GetByIdAsync(id);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto) =>
            Ok(await _service.AddAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }


    }
}
