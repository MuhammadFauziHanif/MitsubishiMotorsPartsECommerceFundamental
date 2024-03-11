
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.BO;
using MitsubishiMotorsPartsECommerce.DAL;
using MitsubishiMotorsPartsECommerce.DAL.MyWebFormApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class ProductBLL : IProductBLL
    {
        private readonly IProductDAL _productDAL;

        public ProductBLL()
        {
            _productDAL = new ProductDAL();
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ArticleID is required");
            }

            try
            {
                _productDAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var productsFromDAL = _productDAL.GetAll();
            foreach (var product in productsFromDAL)
            {
                products.Add(new ProductDTO
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    ImageUrl = product.ImageUrl,
                    CategoryID = product.CategoryID,
                });
            }
            return products;
        }

        public IEnumerable<ProductDTO> GetProductByCategory(int categoryId)
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var productsFromDAL = _productDAL.GetProductByCategory(categoryId);
            foreach (var product in productsFromDAL)
            {
                products.Add(new ProductDTO
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    ImageUrl = product.ImageUrl,
                    CategoryID = product.CategoryID,

                    Category = new CategoryDTO
                    {
                        CategoryID = product.Category.CategoryID,
                        CategoryName = product.Category.CategoryName
                    }
                });
            }

            return products;
        }

        public ProductDTO GetProductByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ProductID is required");
            }

            var product = _productDAL.GetById(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with id:{id} not found");
            }

            return new ProductDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CategoryID = product.CategoryID,
            };
        }

        public IEnumerable<ProductDTO> GetProductWithCategory()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var productsFromDAL = _productDAL.GetProductWithCategory();
            foreach (var product in productsFromDAL)
            {
                products.Add(new ProductDTO
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryID = product.CategoryID,

                    Category = new CategoryDTO
                    {
                        CategoryID = product.Category.CategoryID,
                        CategoryName = product.Category.CategoryName
                    }
                });
            }

            return products;
        }

        public void Insert(ProductCreateDTO entity)
        {
            if (string.IsNullOrEmpty(entity.ProductName))
            {
                throw new ArgumentException("ProductName is required");
            }

            try
            {
                var product = new Product
                {
                    ProductName = entity.ProductName,
                    StockQuantity = entity.StockQuantity,
                    Description = entity.Description,
                    Price = entity.Price,
                    ImageUrl = entity.ImageUrl,
                    CategoryID = entity.CategoryID
                };
                _productDAL.Insert(product);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public int InsertWithIdentity(ProductCreateDTO entity)
        {
            if (string.IsNullOrEmpty(entity.ProductName))
            {
                throw new ArgumentException("ProductName is required");
            }

            try
            {
                var product = new Product
                {
                    ProductName = entity.ProductName,
                    Description = entity.Description,
                    Price = entity.Price,
                    ImageUrl = entity.ImageUrl,
                    CategoryID = entity.CategoryID
                };
                return _productDAL.InsertWithIdentity(product);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(ProductUpdateDTO entity)
        {
            if (entity.ProductID <= 0)
            {
                throw new ArgumentException("ProductID is required");
            }
            else if (string.IsNullOrEmpty(entity.ProductName))
            {
                throw new ArgumentException("ProductName is required");
            }

            try
            {
                var product = new Product
                {
                    ProductID = entity.ProductID,
                    ProductName = entity.ProductName,
                    Description = entity.Description,
                    Price = entity.Price,
                    StockQuantity = entity.StockQuantity,
                    ImageUrl = entity.ImageUrl,
                    CategoryID = entity.CategoryID
                };
                _productDAL.Update(product);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
