using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.RESTServices.Models;

namespace MitsubishiMotorsPartsECommerce.RESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBLL _productBLL;

        public ProductsController(IProductBLL productBLL
            )
        {
            _productBLL = productBLL;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await _productBLL.GetAll();
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpGet("{id}")]
        public async Task<ProductDTO> Get(int id)
        {
            return await _productBLL.GetById(id);
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpPost("upload")]
        public async Task<IActionResult> Post([FromForm] ProductWithFile productWithFile)
        {
            if (productWithFile.file == null || productWithFile.file.Length == 0)
            {
                return BadRequest("File is required");
            }
            var newName = $"{Guid.NewGuid()}_{productWithFile.file.FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await productWithFile.file.CopyToAsync(stream);
            }

            var productCreateDTO = new ProductCreateDTO
            {

                ProductName = productWithFile.ProductName,
                CategoryID = productWithFile.CategoryID,
                Description = productWithFile.Description,
                Price = productWithFile.Price,
                StockQuantity = productWithFile.StockQuantity,
                ImageUrl = newName

            };

            var product = await _productBLL.Insert(productCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = product.ProductID }, product);
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {

            var product = await _productBLL.Update(id, productUpdateDTO);
            return Ok(product);
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productBLL.Delete(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
