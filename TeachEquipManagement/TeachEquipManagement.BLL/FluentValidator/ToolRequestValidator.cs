using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class ToolRequestValidator : AbstractValidator<ToolRequest>
    {
        public ToolRequestValidator()
        {
            RuleFor(x => x.ToolName)
            .NotEmpty().WithMessage("ToolName is required.")
            .MaximumLength(50).WithMessage("ToolName must not exceed 50 characters.");

            RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is required.")
            .MaximumLength(50).WithMessage("Unit must not exceed 50 characters.");

            RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Supplier is required.");
        }
    }
}
