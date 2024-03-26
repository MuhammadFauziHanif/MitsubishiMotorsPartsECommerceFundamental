using FluentValidation;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;

namespace MyRESTServices.BLL.DTOs.Validation
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name must not be empty");
        }
    }
}
