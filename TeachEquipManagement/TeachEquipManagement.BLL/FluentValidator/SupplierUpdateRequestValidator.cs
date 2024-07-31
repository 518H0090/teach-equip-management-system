using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class SupplierUpdateRequestValidator : AbstractValidator<SupplierUpdateRequest>
    {
        public SupplierUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage("SupplierName is required.")
            .MaximumLength(50).WithMessage("Address must not exceed 50 characters.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.ContactName)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(50).WithMessage("Address must not exceed 50 characters.");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^0?\d{8,14}$").WithMessage("Invalid phone number format.");
        }
    }
}
