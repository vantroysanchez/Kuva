using Domain.Events.HeaderEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.EventHandlers
{
    public class HeaderCreatedEventHandler: INotificationHandler<HeaderCreatedEvent>
    {
        private readonly ILogger<HeaderCreatedEvent> _logger;

        public HeaderCreatedEventHandler(ILogger<HeaderCreatedEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(HeaderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kuva Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
