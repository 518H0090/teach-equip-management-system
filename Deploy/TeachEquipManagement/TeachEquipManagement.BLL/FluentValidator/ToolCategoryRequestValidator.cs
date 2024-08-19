using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class ToolCategoryRequestValidator : AbstractValidator<ToolCategoryRequest>
    {
        public ToolCategoryRequestValidator()
        {
            RuleFor(x => x.ToolId)
           .NotEmpty().WithMessage("ToolId is required.");

            RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
        }
    }
}
