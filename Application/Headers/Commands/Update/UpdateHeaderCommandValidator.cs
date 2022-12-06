using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Commands.Update
{
    public class UpdateHeaderCommandValidator: AbstractValidator<UpdateHeaderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateHeaderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(250).WithMessage("Title must not exceed 250 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateHeaderCommand model, string description, CancellationToken cancellationToken)
        {
            return await _context.Headers
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Description != description, cancellationToken);
        }
    }
}
