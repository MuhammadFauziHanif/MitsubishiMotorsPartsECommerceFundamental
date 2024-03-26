using Microsoft.EntityFrameworkCore;
using MitsubishiMotorsPartsECommerce.Data.Interfaces;
using MitsubishiMotorsPartsECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitsubishiMotorsPartsECommerce.Data
{
    public class CategoryData : ICategoryData
    {
        private readonly AppDbContext _context;
        public CategoryData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await GetById(id);
                if (category == null)
                {
                    throw new ArgumentException("Category not found");
                }
                _context.ProductCategories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            var categories = await _context.ProductCategories.OrderBy(c => c.CategoryName).ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetById(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            return category;
        }

        public async Task<IEnumerable<ProductCategory>> GetByName(string name)
        {
            var categories = await _context.ProductCategories
                .Where(c => c.CategoryName.Contains(name))
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            return categories;
        }

        public async Task<int> GetCountCategories(string name)
        {
            var count = await _context.ProductCategories
                .Where(c => c.CategoryName.Contains(name))
                .CountAsync();
            return count;
        }

        public async Task<IEnumerable<ProductCategory>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var categories = await _context.ProductCategories
                .Where(c => c.CategoryName.Contains(name))
                .OrderBy(c => c.CategoryName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> Insert(ProductCategory entity)
        {
            try
            {
                _context.ProductCategories.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<int> InsertWithIdentity(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategory> Update(int id, ProductCategory entity)
        {
            try
            {
                var category = await GetById(id);
                if (category == null)
                {
                    throw new ArgumentException("Category not found");
                }
                category.CategoryName = entity.CategoryName;
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
