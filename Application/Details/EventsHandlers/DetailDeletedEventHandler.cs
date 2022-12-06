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
    public class DetailDeletedEventHandler : INotificationHandler<EntityDeletedEvent<Detail>>
    {
        private readonly ILogger<EntityDeletedEvent<Detail>> _logger;

        public DetailDeletedEventHandler(ILogger<EntityDeletedEvent<Detail>> logger)
        {
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Detail> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kuva Domain Event Deleted: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
