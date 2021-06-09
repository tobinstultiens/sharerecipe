using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using ShareRecipe.Services.Common.API.IntegrationEvents;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events;

namespace ShareRecipe.Services.ProfileService.API.Application.DomainEventHandlers.UserDisplayNameUpdated
{
    public class UpdatedUserDisplayNameDomainHandler : INotificationHandler<UserDisplayNameUpdatedDomainEvent>
    {
        private readonly IBus _bus;

        public UpdatedUserDisplayNameDomainHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(UserDisplayNameUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            UpdatedUserDisplayNameIntegrationEvent integrationEvent = new(notification.UserId, notification.DisplayName);
            _bus.PubSub.PublishAsync(integrationEvent, cancellationToken);
            return Task.CompletedTask;
        }
    }
}