using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events;

namespace ShareRecipe.Services.ProfileService.API.Application.DomainEventHandlers.UserImageUpdated
{
    public class UpdatedUserImageDomainHandler : INotificationHandler<UserProfileImageUpdateDomainEvent>
    {
        private readonly IBus _bus;

        public UpdatedUserImageDomainHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(UserProfileImageUpdateDomainEvent notification, CancellationToken cancellationToken)
        {
            UpdatedUserImageIntegrationEvent integrationEvent = new(notification.UserId, notification.Image);
            _bus.PubSub.PublishAsync(integrationEvent, configuration => configuration.WithTopic("User.ImageUpdated"), cancellationToken);
            return Task.CompletedTask;
        }
    }
}