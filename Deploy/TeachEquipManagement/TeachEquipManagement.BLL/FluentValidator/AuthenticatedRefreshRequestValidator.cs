using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class AuthenticatedRefreshRequestValidator : AbstractValidator<AuthenticatedRefreshRequest>
    {
        public AuthenticatedRefreshRequestValidator()
        {
            RuleFor(x => x.AccessToken)
           .NotEmpty().WithMessage("AccessToken is required.");

            RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required.");
        }
    }
}
