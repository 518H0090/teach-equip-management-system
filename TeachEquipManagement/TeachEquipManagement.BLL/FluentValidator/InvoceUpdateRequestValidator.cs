using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class InvoceUpdateRequestValidator : AbstractValidator<InvoceUpdateRequest>
    {
        public InvoceUpdateRequestValidator()
        {
            RuleFor(x => x.Price)
           .NotEmpty().WithMessage("Price is required.");

            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        }
    }
}
