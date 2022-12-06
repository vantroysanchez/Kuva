using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.HeaderEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Commands.Create
{
    public record CreateHeaderCommand: IRequest<int>
    {
        public string? Description { get; set; }
    }

    public class CreateHeaderCommandHandler: IRequestHandler<CreateHeaderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateHeaderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHeaderCommand request, CancellationToken cancellationToken)
        {
            Random random = new Random();
            var code = string.Join("8",random.Next(100, 999), random.Next(10, 99));
            var entity = new Header();

            entity.Code = Convert.ToInt32(code);
            entity.Description = request.Description;
            entity.Date = DateTime.Now;
            entity.Status = true;

            entity.AddDomainEvent(new HeaderCreatedEvent(entity));

            _context.Headers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
