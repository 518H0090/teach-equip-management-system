using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Type)
            .NotEmpty().WithMessage("CategoryType is required.")
            .MaximumLength(20).WithMessage("CategoryType must not exceed 20 characters.");
        }
    }
}
