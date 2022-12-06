using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Commands.Update
{
    public record UpdateDetailCommand: IRequest
    {
        public int Id { get; init; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int HeaderId { get; set; }
    }

    public class UpdateDetailCommandhandler : IRequestHandler<UpdateDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDetailCommandhandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Details
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Detail), request.Id);
            }

            entity.Description = request.Description;
            entity.Quantity = request.Quantity;
            entity.Amount = request.Amount;
            entity.HeaderId = request.HeaderId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
