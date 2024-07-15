﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;

namespace TeachEquipManagement.BLL.FluentValidator
{
    public class InventoryUpdateRequestValidator : AbstractValidator<InventoryUpdateRequest>
    {
        public InventoryUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("InventoryId is required.")
            .Must(id => id is Guid)
            .WithName("InventoryId must be Guid");

            RuleFor(x => x.ToolId)
              .NotEmpty().WithMessage("ToolId is required.")
              .Must(id => id is int)
              .WithName("ToolId must be int");

            RuleFor(x => x.TotalQuantity)
            .NotEmpty().WithMessage("TotalQuantity is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("TotalQuantity must be greater than or equal to 0.")
            .Must(id => id is int)
            .WithName("ToolId must be int");

            RuleFor(x => x.AmountBorrow)
           .NotEmpty().WithMessage("AmountBorrow is required.")
           .GreaterThanOrEqualTo(0)
           .WithMessage("AmountBorrow must be greater than or equal to 0.")
           .Must(id => id is int)
           .WithName("ToolId must be int");
        }
    }
}
