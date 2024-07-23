using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class ProcessRequestValidator : AbstractValidator<ProcessRequest>
    {
        public ProcessRequestValidator()
        {
            RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.")
            .Must(id => id is Guid)
            .WithMessage("AccountId Should Be Guid");

            RuleFor(x => x.InventoryId)
           .NotEmpty().WithMessage("InventoryId is required.")
           .Must(id => id is Guid)
           .WithMessage("InventoryId Should Be Guid");
        }
    }
}
