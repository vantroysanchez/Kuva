using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Commands.Create
{
    public class CreateHeaderCommandValidator: AbstractValidator<CreateHeaderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateHeaderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");            
        }
    }
}
