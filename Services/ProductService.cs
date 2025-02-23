using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.data;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductService:IProductService
    {
        //private readonly static List<Product> _products = new List<Product>();
        private AppDbContext _appDbContext;
        private IMapper _mapper;

        public ProductService(IMapper mapper,AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        //Get All Products
        public async Task<List<ProductReadDto?>> GetAllProduct()
        {
            //var productList = _products.Select(p => new ProductReadDto
            //{
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    ProductDescription = p.ProductDescription,
            //    CreatedAt = p.CreatedAt

            //}).ToList();
            var productList = _mapper.Map<List<ProductReadDto>>(_appDbContext.Products);
            return productList;
        }
        //Get a Product by Id
        public async Task<ProductReadDto> GetProductById(Guid productId)
        {
            IQueryable<Product> query = _appDbContext.Products;
            var foundProduct =await query.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (foundProduct == null) return null;
            var productReadDto = _mapper.Map<ProductReadDto>(foundProduct);
            return productReadDto;
        }

        //Post a product
        public async Task<ProductReadDto> CreateProduct([FromBody] ProductCreateDto productData)
        {
            var query = _appDbContext.Products;
            var newProduct = _mapper.Map<Product>(productData);
            newProduct.ProductId = Guid.NewGuid();
            newProduct.CreatedAt = DateTime.UtcNow;
            await query.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();
            var productReadDto = _mapper.Map<ProductReadDto>(newProduct);
            return productReadDto;
        }

        //Updating a Product
        public async Task<bool> UpdateProductById(Guid productId, [FromBody] ProductUpdateDto productData)
        {
            var foundProduct =await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (foundProduct == null) return false;
            if (!string.IsNullOrEmpty(productData.ProductName))
            {
                foundProduct.ProductName = productData.ProductName;
            }
            if (!string.IsNullOrEmpty(productData.ProductDescription))
            {
                foundProduct.ProductDescription = productData.ProductDescription;
            }
            _appDbContext.Update(foundProduct);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        
        //Delete a product
        public async Task<bool> DeleteProductById(Guid productId)
        {
            var foundProduct =await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (foundProduct == null) return false;
            _appDbContext.Products.Remove(foundProduct);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
   }
}
