using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Controllers;

namespace WebApplication1.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductReadDto?>> GetAllProduct();
        Task<ProductReadDto> GetProductById(Guid productId);
        Task<ProductReadDto> CreateProduct([FromBody] ProductCreateDto productData);
        Task<bool> UpdateProductById(Guid productId, [FromBody] ProductUpdateDto productData);
        Task<bool> DeleteProductById(Guid productId);
    }
}
