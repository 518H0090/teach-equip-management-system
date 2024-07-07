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
            .MaximumLength(20).WithMessage("ToolName must not exceed 20 characters.");

            RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Supplier is required.");
        }
    }
}
