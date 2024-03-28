using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Entities;
using ProductAPI.Repositories.Interface;

namespace ProductAPI.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductModel?> CreateProduct(ProductModel product)
        {
            await _dbContext.Products.AddAsync(product);
            _dbContext.SaveChanges();
            return product;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _dbContext.Products.FirstAsync(x => x.ProductId == productId);

            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return true;
        } 

        public async Task<ProductModel?> GetProductById(int productId)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<List<ProductModel?>> GetProductList()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<ProductModel?> UpdateProduct(ProductModel product)
        {
            var productDetails = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
            if (productDetails != null)
            {
                productDetails.Name = product.Name;
                productDetails.Price = product.Price;
                productDetails.Description = product.Description;
                productDetails.CategoryName = product.CategoryName;

                _dbContext.SaveChanges();
                return productDetails;
            }

            return product;
        }
    }
}
