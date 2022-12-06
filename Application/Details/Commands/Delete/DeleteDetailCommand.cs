using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Common;
using Domain.Events.HeaderEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Commands.Delete
{
    public record DeleteDetailCommand(int id): IRequest;

    public class DeleteDetailCommandHandler : IRequestHandler<DeleteDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Details
            .Where(l => l.Id == request.id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Details), request.id);
            }

            entity.AddDomainEvent(new EntityDeletedEvent<Detail>(entity));

            _context.Details.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
