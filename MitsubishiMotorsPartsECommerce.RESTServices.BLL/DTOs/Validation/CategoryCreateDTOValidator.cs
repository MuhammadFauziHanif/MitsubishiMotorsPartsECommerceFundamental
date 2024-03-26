using FluentValidation;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;

namespace MitsubishiMotorsPartsECommerce.BLL.DTOs.Validation
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name must not be empty");
            RuleFor(x => x.CategoryName).MaximumLength(100).WithMessage("Category name should be less than 100 characters");
        }
    }
}
