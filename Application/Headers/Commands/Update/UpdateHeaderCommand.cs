using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Commands.Update
{
    public record UpdateHeaderCommand: IRequest
    {
        public int Id { get; init; }
        public string? Description { get; init; }
    }

    public class UpdateHeaderCommandHandler: IRequestHandler<UpdateHeaderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateHeaderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateHeaderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Headers
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Header), request.Id);
            }

            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
