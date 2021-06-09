using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using ShareRecipe.Services.Common.API.IntegrationEvents;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events;

namespace ShareRecipe.Services.ProfileService.API.Application.DomainEventHandlers.UserCreated
{
    public class CreatedUserDomainHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IBus _bus;

        public CreatedUserDomainHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            CreatedUserIntegrationEvent integrationEvent = new(notification.UserId, notification.DisplayName, notification.Description, notification.Image);
            await _bus.PubSub.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}