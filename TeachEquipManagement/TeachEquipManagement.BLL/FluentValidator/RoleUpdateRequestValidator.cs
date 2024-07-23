﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class RoleUpdateRequestValidator : AbstractValidator<RoleUpdateRequest>
    {
        public RoleUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("RoleId is required.");

            RuleFor(x => x.RoleName)
           .NotEmpty().WithMessage("RoleName is required.")
           .MaximumLength(20).WithMessage("RoleName must not exceed 20 characters.");

            RuleFor(x => x.RoleDescription)
            .NotEmpty().WithMessage("RoleDescription is required.")
            .MaximumLength(20).WithMessage("RoleDescription must not exceed 20 characters.");
        }
    }
}
