using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Common;
using Domain.Events.HeaderEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Commands.Create
{
    public record CreateDetailCommand: IRequest<int>
    {
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int HeaderId { get; set; }
    }

    public class CreateDetailCommandHandler : IRequestHandler<CreateDetailCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = new Detail();

            entity.Description = request.Description;
            entity.Quantity = request.Quantity;
            entity.Amount = request.Amount;
            entity.HeaderId = request.HeaderId;

            entity.AddDomainEvent(new EntityCreatedEvent<Detail>(entity));

            _context.Details.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
