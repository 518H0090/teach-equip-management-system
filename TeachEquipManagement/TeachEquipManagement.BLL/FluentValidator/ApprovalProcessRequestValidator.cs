using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class ApprovalProcessRequestValidator : AbstractValidator<ApprovalProcessRequest>
    {
        public ApprovalProcessRequestValidator()
        {
            RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.")
            .Must(id => id is Guid)
            .WithMessage("AccountId Should Be Guid");

            RuleFor(x => x.InventoryId)
           .NotEmpty().WithMessage("InventoryId is required.")
           .Must(id => id is Guid)
           .WithMessage("InventoryId Should Be Guid");

            RuleFor(x => x.Quantity)
            .Must(id => id is int)
            .WithMessage("Quantity Should Be Int")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity must be greater than or equal to 0.");

            RuleFor(x => x.RequestType)
            .NotEmpty().WithMessage("RequestType is required.");
        }
    }
}
