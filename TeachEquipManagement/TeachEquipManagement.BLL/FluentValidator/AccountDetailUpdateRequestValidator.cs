using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class AccountDetailUpdateRequestValidator : AbstractValidator<AccountDetailUpdateRequest>
    {
        public AccountDetailUpdateRequestValidator()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required.")
            .MaximumLength(30).WithMessage("FullName must not exceed 30 characters.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(50).WithMessage("Address must not exceed 50 characters.");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Phone is required.")
           .Matches(@"^\d{8,14}$").WithMessage("Phone number must be between 8 and 14 digits.");

            RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.")
            .Must(accountId => accountId is Guid)
            .WithMessage("Id must be Guid");
        }
    }
}
