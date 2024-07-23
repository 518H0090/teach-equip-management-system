using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class InvoceRequestValidator : AbstractValidator<InvoceRequest>
    {
        public InvoceRequestValidator()
        {
            RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.");

            RuleFor(x => x.ToolId)
            .NotEmpty().WithMessage("ToolId is required.");
        }
    }
}
