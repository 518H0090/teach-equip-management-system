using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class UserRequestValidator : AbstractValidator<AccountRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
               .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            .WithMessage("Invalid email address format.");
        }
    }
}
