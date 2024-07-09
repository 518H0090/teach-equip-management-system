using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class AuthenticatedRequestValidator : AbstractValidator<AuthenticatedRequest>
    {
        public AuthenticatedRequestValidator()
        {
            RuleFor(x => x.Username)
           .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        }
    }
}
