using AutoMapper;
using MitsubishiMotorsPartsECommerce.Data.Interfaces;
using MitsubishiMotorsPartsECommerce.Domain.Models;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.Interfaces;

namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL
{
    public class ProductBLL : IProductBLL
    {
        private readonly IProductData _productData;
        private readonly IMapper _mapper;

        public ProductBLL(IProductData productData, IMapper mapper)
        {
            _productData = productData;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _productData.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _productData.GetAll();
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _productData.GetById(id);
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        public async Task<int> GetCountProducts(string name)
        {
            var count = await _productData.GetCountProducts(name);
            return count;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductByCategory(int categoryId)
        {
            var products = _productData.GetProductByCategory(categoryId);
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductWithCategory()
        {
            var products = await _productData.GetProductWithCategory();
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var products = await _productData.GetWithPaging(pageNumber, pageSize, name);
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> Insert(ProductCreateDTO entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                var result = await _productData.Insert(product);
                var productDto = _mapper.Map<ProductDTO>(result);
                return productDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> InsertWithIdentity(ProductCreateDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> Update(int id, ProductUpdateDTO entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                await _productData.Update(id, product);
                var productDto = _mapper.Map<ProductDTO>(product);
                return productDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
