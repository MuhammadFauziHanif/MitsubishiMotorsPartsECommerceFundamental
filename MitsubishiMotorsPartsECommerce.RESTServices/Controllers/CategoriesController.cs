using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IValidator<CategoryCreateDTO> _validatorCategoryCreateDto;
        private readonly IValidator<CategoryUpdateDTO> _validatorCategoryUpdateDto;

        public CategoriesController(ICategoryBLL categoryBLL,
            IValidator<CategoryCreateDTO> validatorCategoryCreateDto,
            IValidator<CategoryUpdateDTO> validatorCategoryUpdateDto
            )
        {
            _categoryBLL = categoryBLL;
            _validatorCategoryCreateDto = validatorCategoryCreateDto;
            _validatorCategoryUpdateDto = validatorCategoryUpdateDto;
        }


        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var results = await _categoryBLL.GetAll();
            return results;
        }

        /// <summary>
        /// Method to get single Category
        /// </summary>
        /// <param name="id">insert Category ID</param>
        /// <returns>Return single Category</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("GetWithPaging")]
        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber = 1, int pageSize = 5, string name = "")
        {

            var categories = await _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
            return categories;
        }

        [HttpGet("GetCountCategories")]
        public async Task<int> GetCountCategories(string name = "")
        {
            var count = await _categoryBLL.GetCountCategories(name);
            return count;
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDTO categoryCreateDTO)
        {
            var validateResult = await _validatorCategoryCreateDto.ValidateAsync(categoryCreateDTO);

            if (!validateResult.IsValid)
            {
                MitsubishiMotorsPartsECommerce.Helpers.Extensions.AddToModelState(validateResult, ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var newCategory = await _categoryBLL.Insert(categoryCreateDTO);
                return CreatedAtAction(nameof(Get), new { id = newCategory.CategoryID }, newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var validateResult = await _validatorCategoryUpdateDto.ValidateAsync(categoryUpdateDTO);

            if (!validateResult.IsValid)
            {
                MitsubishiMotorsPartsECommerce.Helpers.Extensions.AddToModelState(validateResult, ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryBLL.Update(id, categoryUpdateDTO);
                return Ok("Update data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryBLL.Delete(id);
                return Ok("Delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}