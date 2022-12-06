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
    public class HeaderCompletedEventHandler: INotificationHandler<HeaderCompletedEvent>
    {
        private readonly ILogger<HeaderCompletedEvent> _logger;

        public HeaderCompletedEventHandler(ILogger<HeaderCompletedEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(HeaderCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kuva Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
