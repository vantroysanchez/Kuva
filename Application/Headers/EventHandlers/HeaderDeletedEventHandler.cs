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
    public class HeaderDeletedEventHandler : INotificationHandler<HeaderDeletedEvent>
    {
        private readonly ILogger<HeaderDeletedEvent> _logger;

        public HeaderDeletedEventHandler(ILogger<HeaderDeletedEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(HeaderDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kuva Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
