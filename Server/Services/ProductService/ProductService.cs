using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Server.Data;
using Shared;

namespace Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.ToListAsync(),
                Success = true,
                Message = string.Empty
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.FindAsync(productId);
            if(product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exists";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
        {
            var pageResult = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResult);

            var products = await _context.Products
                          .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                          ||
                          p.Description.ToLower().Contains(searchText.ToLower()))
                          .Skip((page - 1) * (int)pageCount)
                          .Take((int)pageResult)
                          .ToListAsync();
            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };
            return response;

        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                          .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                          ||
                          p.Description.ToLower().Contains(searchText.ToLower()))
                          .ToListAsync();
        }
    }
}
