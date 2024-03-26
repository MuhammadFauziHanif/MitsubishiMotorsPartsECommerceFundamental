using Microsoft.EntityFrameworkCore;
using MitsubishiMotorsPartsECommerce.Data.Interfaces;
using MitsubishiMotorsPartsECommerce.Domain.Models;

namespace MitsubishiMotorsPartsECommerce.Data
{
    public class ProductData : IProductData
    {

        private readonly AppDbContext _context;
        public ProductData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var delProduct = await _context.Products.SingleOrDefaultAsync(a => a.ProductId == id);
                if (delProduct == null)
                {
                    throw new ArgumentException("Product not found");
                }
                _context.Products.Remove(delProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.OrderBy(a => a.ProductName).ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.Include(a => a.Category).SingleOrDefaultAsync(a => a.ProductId == id);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            return product;
        }

        public async Task<int> GetCountProducts(string name)
        {
            var count = await _context.Products.Where(a => a.ProductName.Contains(name)).CountAsync();
            return count;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int categoryId)
        {
            var products = await _context.Products.Where(a => a.CategoryId == categoryId).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductWithCategory()
        {
            var products = await _context.Products.Include(a => a.Category).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            var products = await _context.Products
                .Where(a => a.CategoryId == categoryId)
                .OrderBy(a => a.ProductName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }

        public Task<IEnumerable<Product>> GetWithPaging(int categoryId, int pageNumber, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Insert(Product entity)
        {
            try
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Task> InsertProductWithCategory(Product product)
        {
            try
            {
                _context.ProductCategories.Add(product.Category);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> InsertWithIdentity(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Update(int id, Product entity)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(a => a.ProductId == id);
                if (product == null)
                {
                    throw new ArgumentException("Product not found");
                }

                product.ProductName = entity.ProductName;
                product.Description = entity.Description;
                product.Price = entity.Price;
                product.StockQuantity = entity.StockQuantity;
                product.ImageUrl = entity.ImageUrl;
                product.CategoryId = entity.CategoryId;
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
