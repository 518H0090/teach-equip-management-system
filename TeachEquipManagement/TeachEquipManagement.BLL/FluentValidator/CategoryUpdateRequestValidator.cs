using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
    {
        public CategoryUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(x => x.Type)
            .NotEmpty().WithMessage("CategoryType is required.")
            .MaximumLength(20).WithMessage("CategoryType must not exceed 20 characters.");

            RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("CategoryUnit is required.")
            .MaximumLength(20).WithMessage("CategoryUnit must not exceed 20 characters.");
        }
    }
}
