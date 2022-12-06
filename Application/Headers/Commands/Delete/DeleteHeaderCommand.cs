using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Events.HeaderEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Commands.Delete
{
    public record DeleteHeaderCommand(int Id) : IRequest;

    public class DeleteHeaderCommandHandler : IRequestHandler<DeleteHeaderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHeaderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteHeaderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Headers
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Headers), request.Id);
            }

            _context.Headers.Remove(entity);

            entity.RemoveDomainEvent(new HeaderDeletedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
