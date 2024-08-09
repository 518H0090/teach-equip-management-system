using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class AccountDetailRequestValidator : AbstractValidator<AccountDetailRequest>
    {
        public AccountDetailRequestValidator()
        {
            RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.")
            .Must(accountId => accountId is Guid)
            .WithMessage("Id must be Guid");
        }
    }
}
