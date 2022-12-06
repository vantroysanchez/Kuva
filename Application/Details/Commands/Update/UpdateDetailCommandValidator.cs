using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Commands.Update
{
    public class UpdateDetailCommandValidator: AbstractValidator<UpdateDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDetailCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

            RuleFor(q => q.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required");

            RuleFor(q => q.Amount)
            .NotEmpty()
            .WithMessage("Amount is required and less than 1mm");

            RuleFor(q => q.HeaderId)
            .NotEmpty()
            .WithMessage("Header is required");
        }
    }
}
