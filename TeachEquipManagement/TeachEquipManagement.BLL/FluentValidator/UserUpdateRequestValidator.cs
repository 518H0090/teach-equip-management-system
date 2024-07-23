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
    public class UserUpdateRequestValidator : AbstractValidator<AccountUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id is Guid)
                .WithMessage("Id must be guid");

            RuleFor(x => x.Password)
               .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
               .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");

            RuleFor(x => x.Email)
            .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            .WithMessage("Invalid email address format.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .Must(id => id is int)
                .WithMessage("Id must be int");
        }
    }
}
