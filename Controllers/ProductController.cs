using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController:ControllerBase
    {
        private IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //GET method for get all Products
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var productList =await _productService.GetAllProduct();
            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(productList,200,"Products returned successfully!"));
        }

        //GET method for getting a single Product using product id
        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var productReadDto =await _productService.GetProductById(productId);
            if (productReadDto == null) return NotFound(ApiResponse<object>.SuccessResponse(null,400,"Product with this id does not exist"));
            return Ok(ApiResponse<ProductReadDto>.SuccessResponse(productReadDto,200,"Product returned successfully!"));
        }

        //POST method for create a product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productData)
        {
            var newProduct =await _productService.CreateProduct(productData);
            return Created($"/api/v1/{newProduct.ProductId}", 
                ApiResponse<ProductReadDto>.SuccessResponse(newProduct,201,"Product Created Successfully!"));
       }

        //PUT method for updating a Product by id
        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProductById(Guid productId, [FromBody] ProductUpdateDto   productData)
        {
            var foundProduct =await _productService.UpdateProductById(productId, productData);
            if (!foundProduct) return NotFound(ApiResponse<object>.SuccessResponse(null,400,"Product with this id does not eixst"));
            return Ok(ApiResponse<object>.SuccessResponse(null,200,"Product updated successfully!"));
        }

        //DELETE method for deleting a product
        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProductById(Guid productId)
        {
            var foundProduct =await _productService.DeleteProductById(productId);
            if (!foundProduct) return NotFound(ApiResponse<object>.SuccessResponse(null,400,"Product with this id does not exist!"));
            return Ok(ApiResponse<object>.SuccessResponse(null,200, "Product Deleted successfully"));
        }


    }
}
