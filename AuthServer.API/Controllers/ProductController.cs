using AuthServer.Core.Dtos;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDto> _serviceGeneric;

        public ProductController(IServiceGeneric<Product, ProductDto> serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() => ActionResultInstance(await _serviceGeneric.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto) => ActionResultInstance(await _serviceGeneric.AddAsync(productDto));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto) => ActionResultInstance(await _serviceGeneric.Update(productDto, productDto.Id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => ActionResultInstance(await _serviceGeneric.Remove(id));
    }
}
