using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
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

        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            CreatedUserIntegrationEvent integrationEvent = new(notification.UserId, notification.DisplayName, notification.Description, notification.Image);
            _bus.PubSub.PublishAsync(integrationEvent, configuration => configuration.WithTopic("User.Created"), cancellationToken);
            return Task.CompletedTask;
        }
    }
}