using Domain.Entities;
using Domain.Events.Common;
using Domain.Events.HeaderEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.EventsHandlers
{
    public class DetailCreatedEventHandler : INotificationHandler<EntityCreatedEvent<Detail>>
    {
        private readonly ILogger<EntityCreatedEvent<Detail>> _logger;

        public DetailCreatedEventHandler(ILogger<EntityCreatedEvent<Detail>> logger)
        {
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Detail> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kuva Domain Event Created: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
